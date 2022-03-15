using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampAnimation : MonoBehaviour
{
    public Sprite trampolineDown;
    public Sprite trampolineUp;
    public float animDuration;

    private SpriteRenderer sRenderer;

    void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" && col.rigidbody.velocity.y < 0.0f)
        {
            StartCoroutine(trampAnim(animDuration));
        }
    }

    IEnumerator trampAnim(float time)
    {
        sRenderer.sprite = trampolineDown;
        yield return new WaitForSeconds(time);
        sRenderer.sprite = trampolineUp;
    }
}
