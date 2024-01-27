using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class spriteInspector : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Vector2[] spriteVertices;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteVertices = spriteRenderer.sprite.vertices;
    }
}
