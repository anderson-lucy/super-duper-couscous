using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Enemy
{
    public Sprite[] chugAnimation;
    public Sprite[] explosionAnimation;
    public float explosionWaitTime;

    public float speed;
    public bool grounded;
    public bool isHurt = false;
    public LayerMask groundLayer;
    public float groundRayLength = .1f;
    public float groundRaySpread = .1f;
    public float ledgeTestLeft;
    public float ledgeTestRight;

    private int chugCount;
    private Rigidbody2D rb;
    private SpriteRenderer mySpriteRenderer;
    private float changeTimer;
    private float animationtimer = .2f;

    private int direction = 1;

    private AudioSource myAudioSource;
    public float volume = 0.5f;
    public AudioClip trainHurtSound;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myAudioSource = GetComponent<AudioSource>();
        chugCount = 0;
        changeTimer = animationtimer;
        
    }

    void Update()
    {
        updateGrounding();
        updateDirection();

        Vector2 vel = rb.velocity;
        vel.x = direction * speed;

        if (isHurt)
        {
            vel.x = 0;
        }

        rb.velocity = vel;

        if (!isHurt)
            animate();
    }

    public override void Hurt()
    {
        myAudioSource.PlayOneShot(trainHurtSound, volume);
        StartCoroutine(hurtSequence(explosionWaitTime));
    }

    void animate()
    {
        changeTimer -= Time.deltaTime;
        if (direction > 0)
        {
            mySpriteRenderer.flipX = false;
            if (changeTimer <= 0)
            {
                chugCount++;
                if (chugCount >= chugAnimation.Length)
                    chugCount = 0;
                changeTimer = animationtimer;
            }
        }
        if (direction < 0)
        {
            mySpriteRenderer.flipX = true;
            if (changeTimer <= 0)
            {
                chugCount++;
                if (chugCount >= chugAnimation.Length)
                    chugCount = 0;
                changeTimer = animationtimer;
            }

        }
        mySpriteRenderer.sprite = chugAnimation[chugCount];
    }

    int updateDirection()
    {

        if (!grounded)
        {
            direction = 0;
            return 0;
        }
        if(direction == 0)
        {
            direction = 1;
        }

        Vector3 legdgeRayStartLeft = transform.position + Vector3.up * groundRayLength + Vector3.left * ledgeTestLeft;
        Vector3 legdgeRayStartRight = transform.position + Vector3.up * groundRayLength + Vector3.right * ledgeTestRight;

        Debug.DrawLine(legdgeRayStartLeft, legdgeRayStartLeft + Vector3.down * groundRayLength * 2, Color.red);
        Debug.DrawLine(legdgeRayStartRight, legdgeRayStartRight + Vector3.down * groundRayLength * 2, Color.red);

        RaycastHit2D hitLeft = Physics2D.Raycast(legdgeRayStartLeft, Vector2.down, groundRayLength * 2, groundLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(legdgeRayStartRight, Vector2.down, groundRayLength * 2, groundLayer);

        if(hitLeft.collider == null)
        {
            direction = 1;
        }
        if(hitRight.collider == null)
        {
            direction = -1;
        }
        return direction;
    }

    bool updateGrounding()
    {
        Vector3 rayStart = transform.position + Vector3.up * groundRayLength;
        Vector3 rayStartLeft = transform.position + Vector3.up * groundRayLength + Vector3.left * groundRaySpread;
        Vector3 rayStartRight = transform.position + Vector3.up * groundRayLength + Vector3.right * groundRaySpread;

        RaycastHit2D hit = Physics2D.Raycast(rayStart, Vector2.down, groundRayLength * 2, groundLayer);
        RaycastHit2D hitLeft = Physics2D.Raycast(rayStartLeft, Vector2.down, groundRayLength * 2, groundLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(rayStartRight, Vector2.down, groundRayLength * 2, groundLayer);
        
        if (hit.collider != null || hitLeft.collider != null || hitRight.collider != null)
        {
            grounded = true;
            return true;
        }
        grounded = false;
        return false;
    }

    IEnumerator hurtSequence(float time)
    {
        mySpriteRenderer.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        Debug.Log("train mole hit");
        GUIManager.EnemyCountdown();
        isHurt = true;
        
        foreach (Sprite explode in explosionAnimation)
        {
            mySpriteRenderer.sprite = explode;
            yield return new WaitForSeconds(time);
        }

        Destroy(gameObject);
    }
}
