using System;
using UnityEngine;

namespace TouchToStart
{
    public class FollowMouse : MonoBehaviour
    {
        public float Speed;
        private Vector3 previousPos;

        private void Start()
        {
            previousPos = Input.mousePosition;
        }

        private void Update()
        {
            MetaMouse.ZeroDepthMouse.MouseMovement(Input.mousePosition - previousPos, Speed);
            previousPos = Input.mousePosition;
        }
    }
}