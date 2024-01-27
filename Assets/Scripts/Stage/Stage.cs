using System;
using TouchToStart.Utility;
using UnityEngine;
using UnityEngine.Serialization;

namespace TouchToStart
{
    public class Stage : Singleton<Stage>
    {
        public GameObject MetaMousePrefab;
        [FormerlySerializedAs("GoalPrefab")] public GameObject StartButtonPrefab;
        [FormerlySerializedAs("ObstaclePrefab")] public GameObject DeleteButtonPrefab;
        public GameObject GameClearRestartButtonPrefab;
        public GameObject CreditPrefab;

        public GameObject WASDPrefab;
        public GameObject InverseWASDPrefab;
        public GameObject PadPrefab;
        public GameObject CartPrefab;

        public GameObject BoundaryPrefab;

        public StageListScriptable StageListData;
        public StageScriptable StageData;
        public SubStage[] SubStages;

        public int CurrentStage;
        public int CurrentSubStage;
        
        public SubStage this[int index]
        {
            get
            {
                int currentIndex = CameraSplit.instance.CurrentFirstCamIndex;

                int targetIndex = currentIndex + index;
                targetIndex = targetIndex % CameraSplit.instance.MaxDepth;

                return SubStages[targetIndex];
            }
        }

        private void Start()
        {
            CurrentStage = PlayerPrefs.GetInt("PlayingStage", 0);

            StageData = StageListData.Stages[CurrentStage];
            
            foreach (var subStage in SubStages)
            {
                subStage.Data = StageData;
            }
            
            this[0].Spawn();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                if (CurrentStage <= 0)
                {
                    return;
                }
                
                for (int i = 0; i < SubStages.Length; i++)
                {
                    SubStages[i].ClearRemains();
                }

                foreach (var mouse in MetaMouse.MouseList)
                {
                    mouse.MouseReset();
                }
                CurrentSubStage = 0;

                CurrentStage--;

                StageData = StageListData.Stages[CurrentStage];
                
                foreach (var subStage in SubStages)
                {
                    subStage.Data = StageData;
                }
            
                this[0].Spawn();
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                if (CurrentStage >= StageListData.Stages.Length)
                {
                    return;
                }
                
                for (int i = 0; i < SubStages.Length; i++)
                {
                    SubStages[i].ClearRemains();
                }
            
                foreach (var mouse in MetaMouse.MouseList)
                {
                    mouse.MouseReset();
                }
                CurrentSubStage = 0;
                
                CurrentStage++;

                if (CurrentStage >= StageListData.Stages.Length)
                {
                    GameClear();
                    return;
                }

                StageData = StageListData.Stages[CurrentStage];
                
                foreach (var subStage in SubStages)
                {
                    subStage.Data = StageData;
                }
            
                this[0].Spawn();
            }
        }

        public void ClearStage()
        {
            CurrentStage++;

            if (CurrentStage >= StageListData.Stages.Length)
            {
                GameClear();
                return;
            }
            StageData = StageListData.Stages[CurrentStage];
            
            PlayerPrefs.SetInt("PlayingStage", CurrentStage);

            FollowMouse.instance.enabled = false;
            
            int targetZoomIndex = (StageData.MaxDepth + 1) / 2 * 2;
            CameraSplit.instance.ZoomIn(targetZoomIndex, () =>
            {
                for (int i = 0; i < SubStages.Length; i++)
                {
                    SubStages[i].ClearRemains();
                }
                
                foreach (var mouse in MetaMouse.MouseList)
                {
                    mouse.MouseReset();
                }
                Start();
                FollowMouse.instance.enabled = true;
            });
        }

        public void ClearSubStage()
        {
            CurrentSubStage++;
            if (CurrentSubStage > StageData.MaxDepth)
            {
                ClearStage();
                CurrentSubStage = 0;
            }
            else
            {
                this[CurrentSubStage].Spawn();
                this[CurrentSubStage - 1].Controller.TargetMouse = this[CurrentSubStage].MetaMouse;
            }
            
            MetaMouse.ZeroDepthMouse.MouseToOrigin();
        }
        
        private void GameClear()
        {
            for (int i = 0; i < SubStages.Length; i++)
            {
                SubStages[i].ClearRemains();
            }
            
            foreach (var mouse in MetaMouse.MouseList)
            {
                Destroy(mouse.gameObject);
            }
            MetaMouse.MouseList.Clear();
            Instantiate(MetaMousePrefab).SetLayerWithChildren(CameraSplit.instance.Cams[0].Cam.gameObject.layer);
            
            PlayerPrefs.SetInt("PlayingStage", 0);

            Instantiate(GameClearRestartButtonPrefab, Vector3.up * 3, Quaternion.identity).SetLayerWithChildren(CameraSplit.instance.Cams[0].Cam.gameObject.layer);
            
            CameraSplit.instance.Cams[1].ScreenRect.SetDivision(0);
            Instantiate(CreditPrefab).SetLayerWithChildren(CameraSplit.instance.Cams[1].Cam.gameObject.layer);
        }

        public void Fail()
        {
            for (int i = 0; i < SubStages.Length; i++)
            {
                SubStages[i].ClearRemains();
            }
            
            foreach (var mouse in MetaMouse.MouseList)
            {
                mouse.MouseReset();
            }
            CurrentSubStage = 0;
            
            this[0].Spawn();
        }
    }
}