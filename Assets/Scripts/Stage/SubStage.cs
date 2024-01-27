using System;
using UnityEngine;

namespace TouchToStart
{
    public class SubStage : MonoBehaviour
    {
        public StageScriptable Data;

        public Transform ScalableParent;
        
        public MetaMouse MetaMouse;
        public Transform GoalParent;
        public Transform ObstacleParent;

        private int _layer; 
        
        public void SetData(StageScriptable data)
        {
            Data = data;
        }

        public void Clear()
        {
            Stage.instance.ClearSubStage();
        }

        private void Awake()
        {
            _layer = gameObject.layer;
        }

        public void Spawn()
        {
            ClearRemains();

            Instantiate(Stage.instance.BoundaryPrefab, ScalableParent).SetLayerWithChildren(_layer);
            
            switch (Data.StageType)
            {
                case StageType.WASD:
                    Instantiate(Stage.instance.WASDPrefab, transform).SetLayerWithChildren(_layer);
                    break;
                case StageType.INVERSE_WASD:
                    Instantiate(Stage.instance.InverseWASDPrefab, transform).SetLayerWithChildren(_layer);
                    break;
                case StageType.PAD:
                    Instantiate(Stage.instance.PadPrefab, transform).SetLayerWithChildren(_layer);
                    break;
                case StageType.CART:
                    Instantiate(Stage.instance.CartPrefab, transform).SetLayerWithChildren(_layer);
                    break;
            }
            
            Instantiate(Stage.instance.MetaMousePrefab, Data.MouseStartPosition, Quaternion.identity, transform).SetLayerWithChildren(_layer);

            foreach (var position in Data.GoalPositions.Values)
            {
                Instantiate(Stage.instance.GoalPrefab, position, Quaternion.identity, GoalParent).SetLayerWithChildren(_layer);
            }
            
            foreach (var position in Data.ObstaclePositions.Values)
            {
                Instantiate(Stage.instance.ObstaclePrefab, position, Quaternion.identity, ObstacleParent).SetLayerWithChildren(_layer);
            }
        }

        public void ClearRemains()
        {
            for (int i = 0; i < GoalParent.childCount; i++)
            {
                Destroy(GoalParent.GetChild(0));
            }

            for (int i = 0; i < ObstacleParent.childCount; i++)
            {
                Destroy(ObstacleParent.GetChild(0));
            }
            
            if (MetaMouse)
                Destroy(MetaMouse.gameObject);
        }
    }
}