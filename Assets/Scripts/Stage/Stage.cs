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
            foreach (var subStage in SubStages)
            {
                subStage.Data = StageData;
            }
            
            this[0].Spawn();
        }

        public void ClearStage()
        {
            CurrentStage++;
            StageData = StageListData.Stages[CurrentStage];
            
            int targetZoomIndex = (StageData.MaxDepth + 1) / 2 * 2;
            CameraSplit.instance.ZoomIn(targetZoomIndex, () =>
            {
                for (int i = 0; i < SubStages.Length; i++)
                {
                    SubStages[i].ClearRemains();
                }
                
                MetaMouse.MouseList.Clear();
                Start();
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
                SubStages[CurrentSubStage].Spawn();
                SubStages[CurrentSubStage - 1].Controller.TargetMouse = SubStages[CurrentSubStage].MetaMouse;
            }
        }

        public void Fail()
        {
            for (int i = 0; i < SubStages.Length; i++)
            {
                SubStages[i].ClearRemains();
            }
            
            SubStages[0].Spawn();
        }
    }
}