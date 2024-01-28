using System;
using TouchToStart.Utility;
using UnityEngine;
using UnityEngine.InputSystem;


namespace TouchToStart
{
    public class FollowMouse : Singleton<FollowMouse>
    {
        public float Speed;

        private float bezel = 50;

        private void Update()
        {
            MetaMouse.ZeroDepthMouse.MouseMovement(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")), Speed);
            Mouse.current.WarpCursorPosition(new Vector2(Screen.width/2, Screen.height/2));
        }

        private void OnEnable()
        {
            Cursor.visible = false;
        }

        private void OnDisable()
        {
            Cursor.visible = true;
        }
    }
}