using UnityEngine;

namespace TouchToStart
{
    [CreateAssetMenu(fileName = "Stages", menuName = "Data/Stages", order = 0)]
    public class StageListScriptable : ScriptableObject
    {
        public StageScriptable[] Stages;
    }
}