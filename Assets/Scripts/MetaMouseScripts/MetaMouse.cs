using System;
using System.Collections.Generic;
using TouchToStart;
using TouchToStart.Sound;
using UnityEngine;

public class MetaMouse : MonoBehaviour
{
    public static MetaMouse ZeroDepthMouse => MouseList[CameraSplit.instance.CurrentFirstCamIndex];
    public static List<MetaMouse> MouseList = new List<MetaMouse>();
    public int Number;

    [HideInInspector] public Vector2 StartPosition;

    void Awake()
    {
        MouseList.Add(this);
    }

    private void Start()
    {
        MouseList.Sort((aMouse, bMouse) => aMouse.Number < bMouse.Number ? -1 : aMouse.Number > bMouse.Number ? 1 : 0);
    }

    public void MouseMovement(Vector3 direction, float speed)
    {
        direction = direction.normalized;
        transform.position += Time.deltaTime * speed * direction;
    }

    public void MouseMovement(Vector3 velocity)
    {
        transform.position += Time.deltaTime * velocity;
    }

    public void MouseMovement(Vector2 direction, float speed)
    {
        direction = direction.normalized;
        transform.position += Time.deltaTime * speed * Vector2to3(direction);
    }

    public void MouseMovement(Vector2 velocity)
    {
        transform.position += Time.deltaTime * Vector2to3(velocity);
    }


//이 아래 함수들은 다른 클래스로 옮겨도 괜찮을 거 같음.
    public Vector3 Vector2to3(Vector2 vector2)
    {
        Vector3 vector3;
        vector3 = new Vector3(vector2.x, vector2.y, 0);
        return vector3;
    }

    public Vector2 Vector3to2(Vector3 vector3)
    {
        Vector2 vector2;
        vector2 = new Vector2(vector3.x,vector3.y);
        return vector2;
    }
    
    public void MouseReset()
    {
        transform.position = StartPosition;
    }

    public void MouseToOrigin()
    {
        transform.position = Vector2.zero;
    }
}