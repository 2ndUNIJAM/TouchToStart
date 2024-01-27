using System;
using TouchToStart.Utility;
using UnityEngine;

namespace TouchToStart
{
    public class Stage : Singleton<Stage>
    {
        public GameObject MetaMousePrefab;
        public GameObject GoalPrefab;
        public GameObject ObstaclePrefab;

        public GameObject WASDPrefab;
        public GameObject InverseWASDPrefab;
        public GameObject PadPrefab;
        public GameObject CartPrefab;

        public GameObject BoundaryPrefab;

        public StageScriptable StageData;
        public SubStage[] SubStages;

        private int _currentSubStage;
        
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

        private void Awake()
        {
            foreach (var subStage in SubStages)
            {
                subStage.Data = StageData;
            }
        }

        private void Start()
        {
            SubStages[0].Spawn();
        }

        public void ClearSubStage()
        {
            _currentSubStage++;
            SubStages[_currentSubStage].Spawn();
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