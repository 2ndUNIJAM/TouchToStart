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
            float xScale = (_targetCam.pixelWidth / (float)_targetCam.pixelHeight) / CameraSplit.DEFAULT_SCREEN_RATIO;

            if (float.IsNaN(xScale) || float.IsInfinity(xScale)) xScale = 0;
            
            float yScale = _targetCam.orthographicSize / CameraSplit.DEFAULT_CAMERA_ORTHOGONAL_SIZE;
            
            if (float.IsNaN(yScale) || float.IsInfinity(yScale)) yScale = 0;

            Vector3 targetScale = new Vector3(xScale * yScale, yScale);
            transform.localScale = targetScale;
        }
    }
}