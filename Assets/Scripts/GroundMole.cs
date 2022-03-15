using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMole : Enemy
{
    public Sprite[] hurtAnimation;
    public Sprite idleFrame;
    public float hurtWaitTime;

    private SpriteRenderer sRenderer;
    //private int maxHitCount = 1;
    //private int hitCount = 0;

    void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

    }

    public override void Hurt()
    {
        Debug.Log("hurt");
        StartCoroutine(hurtSequence(hurtWaitTime));
    }

    IEnumerator hurtSequence(float time)
    {
        yield return new WaitForSeconds(0.5f);
        sRenderer.GetComponent<BoxCollider2D>().enabled = false;
        sRenderer.sprite = hurtAnimation[0];
        yield return new WaitForSeconds(time);
        sRenderer.sprite = hurtAnimation[1];
    }
}
