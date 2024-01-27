using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseInteraction : MonoBehaviour
{
    public Vector2 currentMousePosition;
    public Collider2D selfCollider;

    public bool activate;
    
    public bool isTouched(Vector2 mousePosition, Collider2D collider) // �ش� �ݶ��̴� �ȿ� ���콺�� �������� �ִ��� Ȯ��
    {
        return collider.OverlapPoint(mousePosition); // bool ������ ���콺�� �ش� �ݶ��̴��� �ִ��� ���

    }

    private void Update()
    {
        activate = isTouched(currentMousePosition, selfCollider);
    }
}
