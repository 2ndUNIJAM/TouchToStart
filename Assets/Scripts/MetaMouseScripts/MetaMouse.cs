using System.Collections.Generic;
using UnityEngine;

public class MetaMouse : MonoBehaviour
{
    public static MetaMouse ZeroDepthMouse => MouseList[ZeroDepthMouseIndex];
    public static int ZeroDepthMouseIndex;
    public static List<MetaMouse> MouseList;
    public float Speed;

    void Awake()
    {
        MouseList = new List<MetaMouse>();
        MouseList.Add(this);
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

}