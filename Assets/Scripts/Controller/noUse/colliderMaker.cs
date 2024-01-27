using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderMaker : MonoBehaviour
{
    [SerializeField]
    private Vector2[] pointsArray;
    public PolygonCollider2D selfCollider;
    public SpriteRenderer spriteRenderer;

    public void getVertices() // sprite Renderer���� sprite�� vertices ���������� ����Ʈ�� pointsArray�� ����, makeCollider()���� ����
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        pointsArray = spriteRenderer.sprite.vertices;
    }

    public Collider2D makeCollider() // getVertices() ���� ��, ������Ʈ�� �ݶ��̴��� �־��� �������� ���� �ݶ��̴� ������� ����
    {
        getVertices();

        selfCollider.autoTiling = false; // spriteRenderer�� �ִ� sprite��� collider�� ����_��ȿȭ
        selfCollider.points = pointsArray;

        // �۵��Ϸ��� �ν����Ϳ��� selfCollider�� �ڽ��� polygoncollider2d ������Ʈ�� �������־�� �Ѵ�.
        return selfCollider;
    }

    private void Start()
    {
        makeCollider();
    }


}
