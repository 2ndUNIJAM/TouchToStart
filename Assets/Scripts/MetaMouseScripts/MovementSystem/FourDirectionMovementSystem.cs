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

    // void Start()
    // {
    //     metaMouse = MetaMouse.MouseList[0];
    // }

    // void Update()
    // {
    //     FourDirectionMovement(metaMouse, direction, mouseSpeed);
    // }

    // void FourDirectionMovement(MetaMouse metaMouse, Vector2 direction, float mouseSpeed)
    // {
    //     metaMouse.MouseMovement(direction, mouseSpeed);
    // }           //이거 안 해도 될 거 같음
}