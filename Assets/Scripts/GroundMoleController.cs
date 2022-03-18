using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMoleController : Enemy
{
    public Sprite[] hurtAnimation;
    public Sprite idleFrame;
    public float hurtWaitTime;

    private SpriteRenderer sRenderer;
    private AudioSource myAudioSource;
    public float volume = 0.5f;
    public AudioClip groundHurtSound;

    void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        myAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

    }

    public override void Hurt()
    {
        myAudioSource.PlayOneShot(groundHurtSound, volume);
        StartCoroutine(hurtSequence(hurtWaitTime));
    }

    IEnumerator hurtSequence(float time)
    {
        sRenderer.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        Debug.Log("ground mole hit");
        GUIManager.EnemyCountdown();
        sRenderer.sprite = hurtAnimation[0];
        yield return new WaitForSeconds(time);
        sRenderer.sprite = hurtAnimation[1];
    }
}
