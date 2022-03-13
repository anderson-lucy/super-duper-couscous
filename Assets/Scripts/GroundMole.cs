using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMole : MonoBehaviour
{
    public Sprite[] hurtAnimation;
    public Sprite idleFrame;
    public float hurtWaitTime;

    private SpriteRenderer sRenderer;
    private int maxHitCount = 1;
    private int hitCount = 0;

    void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.attachedRigidbody.velocity.y < 0.0f && hitCount < maxHitCount)
        {
            StartCoroutine(hurtSequence(hurtWaitTime));
        }
    }

    IEnumerator hurtSequence(float time)
    {
        sRenderer.GetComponent<BoxCollider2D>().enabled = false;
        hitCount++;
        sRenderer.sprite = hurtAnimation[0];
        yield return new WaitForSeconds(time);
        sRenderer.sprite = hurtAnimation[1];
    }
}
