using System;
using UnityEngine;

namespace TouchToStart
{
    public class StageScaler : MonoBehaviour
    {
        public int TargetDepth;
        [SerializeField]
        private Camera _targetCam;

        private void Start()
        {
            _targetCam = CameraSplit.instance.Cams[TargetDepth].Cam;
        }

        private void Update()
        {
            float xScale;
            if (_targetCam.pixelHeight == 0)
            {
                xScale = 0;
            }
            else
            {
                xScale = (_targetCam.pixelWidth / (float)_targetCam.pixelHeight) / CameraSplit.DEFAULT_SCREEN_RATIO;
            }
            
            float yScale;
            if (float.IsNaN(_targetCam.orthographicSize))
            {
                yScale = 0;
            }
            else
            {
                yScale = _targetCam.orthographicSize / CameraSplit.DEFAULT_CAMERA_ORTHOGONAL_SIZE;
            }
            Vector3 targetScale = new Vector3(xScale * yScale, yScale);
            transform.localScale = targetScale;
        }
    }
}