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

        // Hover
        protected int keyIndex = 0;
        [SerializeField] private SpriteRenderer wKey;
        [SerializeField] private SpriteRenderer aKey;
        [SerializeField] private SpriteRenderer sKey;
        [SerializeField] private SpriteRenderer dKey;

        public void keyHover(int index)
        {
            wKey.enabled = true; aKey.enabled = true; sKey.enabled = true; dKey.enabled = true;

            if (index == 1) { wKey.enabled = false; }
            else if (index == 2) { dKey.enabled = false; }
            else if (index == 3) { sKey.enabled = false; }
            else if (index == 4) { aKey.enabled = false; }
            // else if(index == 0) { 가운데버튼 이펙트 } 
            else { return; }
        }

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
            else
            {
                keyIndex = 0;
            }

            lastHovered = IsHovering();

            keyHover(keyIndex);
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