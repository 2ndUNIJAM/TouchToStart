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
            if (mx > Screen.width)
            {
                newPos = new Vector2(0, my);
            }
            else if (mx < 0)
            {
                newPos = new Vector2(Screen.width, my);
            }
            else if (my > Screen.height)
            {
                newPos = new Vector2(mx, 0);
            }
            else if (my < 0)
            {
                newPos = new Vector2(mx, Screen.height);
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