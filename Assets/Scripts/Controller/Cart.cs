using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering.LookDev;

namespace TouchToStart
{
    public class Cart : Controller
    {
        protected override Vector2 CalculateOutput()
        {
            if (upCollider.OverlapPoint(mousePosition)) { keyIndex = 1; return new Vector2(-TargetMouse.transform.right.y, TargetMouse.transform.right.x); }
            else if (downCollider.OverlapPoint(mousePosition)) { keyIndex = 3; return -new Vector2(-TargetMouse.transform.right.y, TargetMouse.transform.right.x); }
            else if (leftCollider.OverlapPoint(mousePosition))
            {
                keyIndex = 4;
                angle = TargetMouse.transform.eulerAngles.z;
                TargetMouse.transform.eulerAngles = new Vector3(0, 0, angle + 0.5f);
                //TargetMouse.transform.eulerAngles += new Vector3(0, 0, angularSpeed * Time.deltaTime);
                return new Vector2(-TargetMouse.transform.right.y, TargetMouse.transform.right.x)*0.5f;
            }
            else if (rightCollider.OverlapPoint(mousePosition))
            {
                keyIndex = 2;
                angle = TargetMouse.transform.eulerAngles.z;
                TargetMouse.transform.eulerAngles = new Vector3(0, 0, angle - 0.5f);
                return new Vector2(-TargetMouse.transform.right.y, TargetMouse.transform.right.x)*0.5f;
                //TargetMouse.transform.eulerAngles -= new Vector3(0, 0, angularSpeed * Time.deltaTime);
            }
            else { keyIndex = 0; return Vector2.zero;  }

            //return base.CalculateOutput();
        }

        public float angularSpeed;

        protected override bool IsHovering()
        {
            mousePosition = SameDepthMouse.transform.position;
            return !centerCollider.OverlapPoint(mousePosition);

            // return base.IsHovering();
        }

        private Vector2 mousePosition;

        [SerializeField]
        private PolygonCollider2D upCollider;
        [SerializeField]
        private PolygonCollider2D downCollider;
        [SerializeField]
        private PolygonCollider2D leftCollider;
        [SerializeField]
        private PolygonCollider2D rightCollider;

        [SerializeField]
        private CircleCollider2D centerCollider;

        private float angle;
    }
}