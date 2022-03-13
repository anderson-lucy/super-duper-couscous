using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public float jumpforce = 0;
    Rigidbody2D rb;
    private SpriteRenderer spr;
    public bool grounded = false;
    public bool tramp = false;
    public bool hammer = false;
    public float hammerWaitTime;


    [Header("Grounding")]
    public LayerMask groundMask;
    public float groundRayLength = 0.1f;
    public float groundRaySpread = 0.1f;

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    //public Sprite left;

    public Sprite right;


    void Update()
    {
        float movementHorizontal = 0;
        Vector2 vel = rb.velocity;
        vel.x = Input.GetAxis("Horizontal") * speed;

        spr.sprite = right;
        //maybe make only a set jump amount

        UpdateGrounding();

        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded /*&& !isJumping*/) //also implement double jumping
        {
            Debug.Log("JUMP");
            vel.y = jumpforce;

        }
        if (Input.GetKeyDown(KeyCode.W) && grounded /*&& !isJumping*/)
        {
            Debug.Log("JUMP with W");
            vel.y = jumpforce;

        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            movementHorizontal = speed;

            spr.flipX = false;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            movementHorizontal = -speed;

            spr.flipX = true;
        }
        rb.velocity = vel;

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            //hit the thing
            Debug.Log("HIT");
            StartCoroutine(HammerHit(hammerWaitTime));
        }
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (col.gameObject.CompareTag("Trampoline"))
        {
            Vector2 vel = rb.velocity;
            Debug.Log(jumpforce);
            vel.x = Input.GetAxis("Horizontal") * speed;
            vel.y = jumpforce * 2;
            rb.velocity = vel;

        } 
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
       // if (col.gameObject.CompareTag("Bottom")
       // {
            Debug.Log("BOTTOMMMMM");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // }

        // makes you bounce when you hit an enemy
        Vector2 vel = rb.velocity;
        if (col.tag == "Enemy" && vel.y < 0.0f)
        {
            Debug.Log("collide with enemy");
            vel.y = jumpforce / 2;
        }
        rb.velocity = vel;
    }


    void UpdateGrounding()
    {
        Vector3 rayStart = transform.position + Vector3.up * groundRayLength;
        Vector3 rayStartLeft = transform.position + Vector3.left * groundRaySpread;
        Vector3 rayStartRight = transform.position + Vector3.right * groundRaySpread;

        RaycastHit2D hit = Physics2D.Raycast(rayStart, Vector3.down, groundRayLength * 2, groundMask);
        RaycastHit2D hitLeft = Physics2D.Raycast(rayStartLeft, Vector3.down, groundRayLength * 2, groundMask);
        RaycastHit2D hitRight = Physics2D.Raycast(rayStartRight, Vector3.down, groundRayLength * 2, groundMask);

        if (hit.collider != null || hitLeft.collider != null || hitRight.collider != null)
        {
            Debug.Log("HIIII");
            grounded = true;
        } else
        {
            grounded = false;
        }
       
        
    }

    IEnumerator HammerHit(float time)
    {
        hammer = true;
        yield return new WaitForSeconds(time);
        hammer = false;
    }
}