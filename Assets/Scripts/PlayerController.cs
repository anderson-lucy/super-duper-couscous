using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public float jumpforce = 0;
    public float trampJumpForce = 0;
    Rigidbody2D rb;

    
    public bool tramp = false;
    public bool hammer = false;
    public float hammerWaitTime;
    public float trampAnimTime;

    [Header("Attacking")]
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;


    [Header("Grounding")]
    public bool grounded = false;
    public LayerMask groundMask;
    public float groundRayLength = 0.1f;
    public float groundRaySpread = 0.1f;

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //spr = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        Vector2 vel = rb.velocity;
        vel.x = Input.GetAxis("Horizontal") * speed;

        UpdateGrounding();

        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded /*&& !isJumping*/) //also implement double jumping
        {
            vel.y = jumpforce;

        }
        if (Input.GetKeyDown(KeyCode.W) && grounded /*&& !isJumping*/)
        {
            vel.y = jumpforce;

        }
        //come down from jump faster if down key is pressed
        if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && !grounded)
        {
            vel.y = -1 * jumpforce;
        }

        Vector3 attackPos = attackPoint.transform.localPosition;
        if (vel.x < 0.0f)
        {
            attackPos.x = -1;
        }
        if (vel.x > 0.0f)
        {
            attackPos.x = 1;
        }
        attackPoint.transform.localPosition = attackPos;

        rb.velocity = vel;

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            StartCoroutine(HammerHit(hammerWaitTime));
            Collider2D[] enemyHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

            foreach(Collider2D enemy in enemyHit)
            {
                enemy.GetComponent<Enemy>().Hurt();
            }
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (col.gameObject.CompareTag("Trampoline") && rb.velocity.y < 0.0f)
        {
            Vector2 vel = rb.velocity;
            vel.x = Input.GetAxis("Horizontal") * speed;
            vel.y = trampJumpForce;
            rb.velocity = vel;
            StartCoroutine(TrampHit(trampAnimTime));
        } 
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.CompareTag("Bottom"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
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

    IEnumerator TrampHit(float time)
    {
        tramp = true;
        yield return new WaitForSeconds(time);
        tramp = false;
    }
    //need a way to tell if all moles are hit
    //and then share this info with flower that turns into a door
}