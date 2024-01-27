using UnityEngine;

namespace TouchToStart
{
    [CreateAssetMenu(fileName = "Positions", menuName = "Data/Positions", order = 0)]
    public class PositionScriptable : ScriptableObject
    {
        public Vector2[] Values;
    }
}