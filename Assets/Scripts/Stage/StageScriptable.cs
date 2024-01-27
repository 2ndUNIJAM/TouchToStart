using UnityEngine;

namespace TouchToStart
{
    public enum StageType
    {
        WASD,
        INVERSE_WASD,
        PAD,
        CART,
    }
    
    [CreateAssetMenu(fileName = "Stage", menuName = "Data/Stage", order = 0)]
    public class StageScriptable : ScriptableObject
    {
        public Vector2 MouseStartPosition;

        public StageType StageType;
        
        public PositionScriptable GoalPositions;
        public PositionScriptable ObstaclePositions;
    }
}