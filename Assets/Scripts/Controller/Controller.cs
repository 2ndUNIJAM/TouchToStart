using System;
using UnityEngine;

namespace TouchToStart
{
    public class Controller : MonoBehaviour
    {
        public MetaMouse TargetMouse;
        public MetaMouse SameDepthMouse;

        public static float Speed = 5;
        protected Vector2 Output;
        
        // 중심은 (0, 0)으로 잡고 하세요
        
        private void Update()
        {
            if (IsHovering())
            {
                Output = CalculateOutput();
                TargetMouse.MouseMovement(Output, Speed);
            }
        }

        protected virtual Vector2 CalculateOutput()
        {
            return Vector2.zero;
        }

        protected virtual bool IsHovering()
        {
            return false;
        }
    }
}