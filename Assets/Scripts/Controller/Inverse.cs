using UnityEngine;

namespace TouchToStart
{
    public class Inverse : Controller
    {
        protected override Vector2 CalculateOutput()
        {
            if (upCollider.OverlapPoint(mousePosition)) { keyIndex = 1; return new Vector2(0, -1f); }
            else if (downCollider.OverlapPoint(mousePosition)) { keyIndex = 3; return new Vector2(0, 1f); }
            else if (leftCollider.OverlapPoint(mousePosition)) { keyIndex = 4; return new Vector2(1f, 0); }
            else if (rightCollider.OverlapPoint(mousePosition)) { keyIndex = 2; return new Vector2(-1f, 0); }
            else { keyIndex = 0; return new Vector2(0, 0); }

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