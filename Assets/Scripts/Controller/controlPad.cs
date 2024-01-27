using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class controlPad : MonoBehaviour
{
    [SerializeField]
    private float c = 3.0f; // controller 속도

    // 크기 조정하는 스크립트 필요
    // 원은 1/3 지름

    private Vector2 mousePosition;
    private Vector2 mouseVector;

    [SerializeField]
    private GameObject mouse; // 현재 단계의 가상 마우스 지정

    [SerializeField]
    private GameObject upArea;
    [SerializeField]
    private GameObject downArea;
    [SerializeField]
    private GameObject leftArea;
    [SerializeField]
    private GameObject rightArea;
    [SerializeField]
    private GameObject centerArea;

    private PolygonCollider2D upCollider;
    private PolygonCollider2D downCollider;
    private PolygonCollider2D leftCollider;
    private PolygonCollider2D rightCollider;
    private CircleCollider2D centerCollider;

    // areaIndex
    private int center = 0;
    private int up = 1;
    private int right = 2;
    private int down = 3;
    private int left = 4;

    private int areaIndex;


    // Start is called before the first frame update
    void Start()
    {
        upCollider = upArea.GetComponent<PolygonCollider2D>();
        downCollider = downArea.GetComponent<PolygonCollider2D>();
        leftCollider = leftArea.GetComponent<PolygonCollider2D>();
        rightCollider = rightArea.GetComponent<PolygonCollider2D>();
        centerCollider = centerArea.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        // mousePosition = mouse.position;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        getIndex(mousePosition);
        mouseVector = giveVector(areaIndex);

        Debug.Log(nextVector);
        */
    }

    /*
    public bool isTouched(Vector2 mousePosition, Collider2D collider) // 해당 콜라이더 안에 마우스의 포지션이 있는지 확인
    {
        return collider.OverlapPoint(mousePosition);
    }
    */

    private void getIndex(Vector2 mousePosition)
    {
        if (centerCollider.OverlapPoint(mousePosition)) { areaIndex = 0; }
        else
        {
            if (upCollider.OverlapPoint(mousePosition)) { areaIndex = 1; }
            else if (rightCollider.OverlapPoint(mousePosition)) { areaIndex = 2; }
            else if (downCollider.OverlapPoint(mousePosition)) { areaIndex = 3; }
            else if (leftCollider.OverlapPoint(mousePosition)) { areaIndex = 4; }
            else { areaIndex = 0; }
        }
    }

    public Vector2 giveVector(int i)
    {
        if (i == 0) { return Vector2.zero; }
        else if (i == 1) {  return new Vector2 (0f, c); }
        else if (i == 2) {  return new Vector2 (c, 0f); }
        else if (i == 3) {  return new Vector2 (0f, -c); }
        else if (i == 4) {  return new Vector2 (-c, 0f); }
        else { return Vector2.zero; }
    }
    
}
