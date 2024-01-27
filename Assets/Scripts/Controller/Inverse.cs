using UnityEngine;

namespace TouchToStart
{
    public class Inverse : Controller
    {
        protected override Vector2 CalculateOutput()
        {
            if (upCollider.OverlapPoint(mousePosition)) { return new Vector2(0, -1f); }
            else if (downCollider.OverlapPoint(mousePosition)) { return new Vector2(0, 1f); }
            else if (leftCollider.OverlapPoint(mousePosition)) { return new Vector2(1f, 0); }
            else if (rightCollider.OverlapPoint(mousePosition)) { return new Vector2(-1f, 0); }
            else { return new Vector2(0, 0); }

            // return base.CalculateOutput();
        }

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