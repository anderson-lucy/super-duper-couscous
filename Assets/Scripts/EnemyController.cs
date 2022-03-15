using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Sprite[] chugAnimation;
    //public Sprite sprite;
    public int chugCount;
    private Rigidbody2D rb;
    private SpriteRenderer mySpriteRenderer;
    public float changeTimer;
    private float animationtimer = .9f;

    public float speed;
    public bool grounded;
    public LayerMask groundLayer;
    public float groundRayLength = .1f;
    public float groundRaySpread = .1f;

    private int direction = 1;
    public float ledgeTestLeft;
    public float ledgeTestRight;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        chugCount = 0;
        changeTimer = animationtimer;
        
    }

    void Update()
    {
        updateGrounding();
        updateDirection();

        Vector2 vel = rb.velocity;
        vel.x = direction * speed;
        rb.velocity = vel;

        animate();


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
        
        if(hit.collider != null || hitLeft.collider != null || hitRight.collider != null)
        {
            grounded = true;
            return true;
        }
        grounded = false;
        return false;
    }
}
