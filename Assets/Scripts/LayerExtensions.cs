using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TouchToStart
{
    public static class LayerExtensions
    {
        public static void SetLayerWithChildren(this GameObject go, int layer)
        {
            go.transform.SetLayerWithChildren(layer);
        }
        
        public static void SetLayerWithChildren(this Transform tr, int layer)
        {
            Queue<Transform> trs = new Queue<Transform>();

            trs.Enqueue(tr);

            while (trs.Count > 0)
            {
                var temp = trs.Dequeue();
                temp.gameObject.layer = layer;
                
                foreach (Transform transform in temp)
                {
                    trs.Enqueue(transform);
                }
            }
        }
    }
}