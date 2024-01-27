using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseInteraction : MonoBehaviour
{
    public Vector2 currentMousePosition;
    public Collider2D selfCollider;

    public bool activate;
    
    public bool isTouched(Vector2 mousePosition, Collider2D collider) // 해당 콜라이더 안에 마우스의 포지션이 있는지 확인
    {
        return collider.OverlapPoint(mousePosition); // bool 값으로 마우스가 해당 콜라이더에 있는지 출력

    }

    private void Update()
    {
        activate = isTouched(currentMousePosition, selfCollider);
    }
}
