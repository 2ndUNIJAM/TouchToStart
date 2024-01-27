using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouseMovementSystem : MonoBehaviour
{
    public float mouseSpeed;
    private Vector2 currentMousePosition;
    private Vector2 prevMousePosition;
    private Vector2 mouseVelocity;
    private MetaMouse metaMouse;


    // void Start()
    // {
    //     metaMouse = MetaMouse.MouseList[0];
    //     prevMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    // }
    
    
    // void Update()
    // {
    //     FollowMouseMovement(mouseSpeed);

    //     metaMouse.MouseMovement(mouseVelocity);
    // }
    

    Vector2 CalculateVelocity(Vector2 initialPosition, Vector2 finalPosition, float mouseSpeed)
    {
        Vector2 Velocity;
        Velocity = mouseSpeed * (finalPosition - initialPosition) / Time.deltaTime;     //1frame 당 마우스의 이동거리(속도)

        return Velocity;
    }

    Vector2 FollowMouseMovement(float mouseSpeed)
    {
        currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseVelocity = CalculateVelocity(prevMousePosition, currentMousePosition, mouseSpeed);
        prevMousePosition = currentMousePosition;

        return mouseVelocity;
    }
}