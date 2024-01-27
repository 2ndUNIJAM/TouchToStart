using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderMaker : MonoBehaviour
{
    [SerializeField]
    private Vector2[] pointsArray;
    public PolygonCollider2D selfCollider;
    public SpriteRenderer spriteRenderer;

    public void getVertices() // sprite Renderer에서 sprite의 vertices 꼭지점들의 리스트를 pointsArray에 저장, makeCollider()에서 실행
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        pointsArray = spriteRenderer.sprite.vertices;
    }

    public Collider2D makeCollider() // getVertices() 실행 후, 오브젝트의 콜라이더를 주어진 꼭지점을 가진 콜라이더 모양으로 변형
    {
        getVertices();

        selfCollider.autoTiling = false; // spriteRenderer에 있는 sprite대로 collider가 설정_무효화
        selfCollider.points = pointsArray;

        // 작동하려면 인스펙터에서 selfCollider에 자신의 polygoncollider2d 컴포넌트를 연결해주어야 한다.
        return selfCollider;
    }

    private void Start()
    {
        makeCollider();
    }


}
