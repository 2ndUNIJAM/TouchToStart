using JetBrains.Annotations;
using UnityEngine;

namespace TouchToStart
{
    public class Cart : Controller
    {
        protected override Vector2 CalculateOutput()
        {
            if (upCollider.OverlapPoint(mousePosition)) { return new Vector2(TargetMouse.transform.right.x, TargetMouse.transform.right.y); }
            else if (downCollider.OverlapPoint(mousePosition)) { return -new Vector2(TargetMouse.transform.right.x, TargetMouse.transform.right.y); }
            else if (leftCollider.OverlapPoint(mousePosition))
            {
                TargetMouse.transform.eulerAngles += new Vector3(0, 0, angularSpeed * Time.deltaTime);
                return Vector2.zero;
            }
            else if (rightCollider.OverlapPoint(mousePosition))
            {
                TargetMouse.transform.eulerAngles -= new Vector3(0, 0, angularSpeed * Time.deltaTime);
                return Vector2.zero;
            }
            else { return Vector2.zero;  }

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
    }
}