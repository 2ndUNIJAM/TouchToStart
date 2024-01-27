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
            float xScale = _targetCam.aspect / CameraSplit.DEFAULT_SCREEN_RATIO;
            float yScale = _targetCam.orthographicSize / CameraSplit.DEFAULT_CAMERA_ORTHOGONAL_SIZE;
            Vector3 targetScale = new Vector3(xScale * yScale, yScale);
            transform.localScale = targetScale;
        }
    }
}