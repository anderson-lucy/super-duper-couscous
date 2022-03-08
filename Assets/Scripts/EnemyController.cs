using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Sprite[] chugAnimation;
    public float speed; 
    private int direction = 1;
    public int chugCount;
    private Rigidbody2D rb;
    private SpriteRenderer mySpriteRenderer;
    public float changeTimer;
    private float animationtimer = .9f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        chugCount = 0;
        changeTimer = animationtimer;
    }

    void Update()
    {
        changeTimer -= Time.deltaTime;
        Vector2 velocity = rb.velocity;
        if(direction > 0)
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
        if(direction < 0)
        {
            mySpriteRenderer.flipX = true;
            if(changeTimer <=0)
            {
                chugCount++;
                if (chugCount >= chugAnimation.Length)
                    chugCount = 0;
                changeTimer = animationtimer;
            }

        }
        mySpriteRenderer.sprite = chugAnimation[chugCount];

        RaycastHit2D lookDown = Physics2D.Raycast(transform.position, Vector2.down, 1);
        RaycastHit2D lookLeft = Physics2D.Raycast(transform.position, Vector2.left, 1);
        RaycastHit2D lookRight = Physics2D.Raycast(transform.position, Vector2.right, 1);
        if(lookDown == false || lookLeft || lookRight)
        {
            //direction *=-1;
        }
        if (lookLeft)
            Debug.Log("left and switch");
        if (lookRight)
            Debug.Log("right and switch");

        velocity.x = direction * speed;
        rb.velocity = velocity;
    }
}
