using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class FourDirectionMovementSystem : MonoBehaviour
{
    public float mouseSpeed;
    private Vector2 direction;
    private MetaMouse metaMouse;
}