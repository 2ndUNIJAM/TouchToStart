using System;
using TouchToStart.Utility;
using UnityEngine;
using UnityEngine.InputSystem;


namespace TouchToStart
{
    public class FollowMouse : Singleton<FollowMouse>
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
            float mx = Input.mousePosition.x, my = Input.mousePosition.y;
            Vector2 newPos;
            if (mx > Screen.width - 9)
            {
                newPos = new Vector2(10, my);
            }
            else if (mx < 1)
            {
                newPos = new Vector2(Screen.width - 1.5f, my);
            }
            else if (my > Screen.height - 9)
            {
                newPos = new Vector2(mx, 10);
            }
            else if (my < 1)
            {
                newPos = new Vector2(mx, Screen.height - 1.5f);
            }
            else
            {
                newPos = new Vector2(mx, my);
            }

            Mouse.current.WarpCursorPosition(newPos);
            previousPos = Input.mousePosition;
        }
    }
}