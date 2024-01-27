using System;
using TouchToStart.Sound;
using UnityEngine;

namespace TouchToStart
{
    public class Controller : MonoBehaviour
    {
        public MetaMouse TargetMouse;
        public MetaMouse SameDepthMouse;

        public static float Speed = 5;
        protected Vector2 Output;

        private bool lastHovered = false;
        
        // 중심은 (0, 0)으로 잡고 하세요
        
        private void Update()
        {
            bool thisHovered = IsHovering();

            if (thisHovered && !lastHovered)
            {
                AudioEvents.instance.PlaySound(SoundType.press);
            } else if (!thisHovered && lastHovered)
            {
                AudioEvents.instance.PlaySound(SoundType.release);
            }
            
            if (thisHovered)
            {
                Output = CalculateOutput();
                TargetMouse.MouseMovement(Output, Speed);
            }

            lastHovered = IsHovering();
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