using System;
using System.Collections.Generic;
using UnityEngine;

namespace TouchToStart
{
    public class SubStage : MonoBehaviour
    {
        public StageScriptable Data;

        public Transform ScalableParent;
        
        public MetaMouse MetaMouse;
        public Controller Controller;
        public Transform GoalParent;
        public Transform ObstacleParent;

        private int _layer;
        private int _startButtonNum;
        private List<GameObject> _clearObjects = new List<GameObject>();
        private List<GameObject> _buttonGos = new List<GameObject>();
        
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

            GameObject go;
            
            // go = Instantiate(Stage.instance.BoundaryPrefab, ScalableParent);
            // go.SetLayerWithChildren(_layer);
            // _clearObjects.Add(go);
            
            // go = Instantiate(Stage.instance.MetaMousePrefab, Data.MouseStartPosition, Quaternion.identity, transform);
            // go.SetLayerWithChildren(_layer);
            // _clearObjects.Add(go);
            // MetaMouse = go.GetComponent<MetaMouse>();
            MetaMouse.StartPosition = Data.MouseStartPosition;
            MetaMouse.MouseReset();

            foreach (var position in Data.GoalPositions.Values)
            {
                go = Instantiate(Stage.instance.StartButtonPrefab, position, Quaternion.identity, GoalParent);
                go.SetLayerWithChildren(_layer);
                _buttonGos.Add(go);
                go.GetComponentInChildren<StartButton>().OnButtonClicked += StartButtonClick;
                _startButtonNum++;
            }
            
            foreach (var position in Data.ObstaclePositions.Values)
            {
                go = Instantiate(Stage.instance.DeleteButtonPrefab, position, Quaternion.identity, ObstacleParent);
                go.SetLayerWithChildren(_layer);
                _buttonGos.Add(go);
                go.GetComponentInChildren<DeleteButton>().OnButtonClicked += DeleteButtonClick;
            }
        }

        public void ClearButtons()
        {
            foreach (var go in _buttonGos)
            {
                Destroy(go);
            }
            _buttonGos.Clear();
        }

        public void ClearRemains()
        {
            ClearButtons();

            foreach (var go in _clearObjects)
            {
                Destroy(go);
            }
            _clearObjects.Clear();

            _startButtonNum = 0;
        }

        void StartButtonClick()
        {
            _startButtonNum--;
            if (_startButtonNum <= 0)
            {
                ClearButtons();
                
                if (Stage.instance.CurrentSubStage + 1 <= Data.MaxDepth)
                {
                    GameObject go = null;
                    switch (Data.StageType)
                    {
                        case StageType.WASD:
                            go = Instantiate(Stage.instance.WASDPrefab, transform);
                            break;
                        case StageType.INVERSE_WASD:
                            go = Instantiate(Stage.instance.InverseWASDPrefab, transform);
                            break;
                        case StageType.PAD:
                            go = Instantiate(Stage.instance.PadPrefab, transform);
                            break;
                        case StageType.CART:
                            go = Instantiate(Stage.instance.CartPrefab, transform);
                            break;
                    }

                    go.SetLayerWithChildren(_layer);
                    _clearObjects.Add(go);
                    Controller = go.GetComponent<Controller>();
                    Controller.SameDepthMouse = MetaMouse;
                }

                Stage.instance.ClearSubStage();
            }
        }

        void DeleteButtonClick()
        {
            Stage.instance.Fail();
        }
    }
}