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

    //sprites
    public Sprite hammer1;
    public Sprite hammer2;
    public Sprite hammer3;

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
        //float movementVertical = 0;
        Vector2 vel = rb.velocity;
        vel.x = Input.GetAxis("Horizontal") * speed;

        spr.sprite = right;
        //maybe make only a set jump amount
        UpdateGrounding();

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) && grounded) //also implement double jumping
        {
            Debug.Log("JUMP");
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //hit the thing
            Debug.Log("HIT");
            //changing the animations -- look up the video on this
            spr.sprite = hammer1;
            spr.sprite = hammer2;
            spr.sprite = hammer3;
            //also detect a collision with an enemy using tags
            //implement animations
        }

        //check tag for bar -- if out of bounds then reload the scene (or if they run into a mole or obstacle)
        //logic for if it hits a trampoline then single/double/triple jump automatically
        //logic for running into a mole -- starting over
        //add in camera stuff
        //variable for amount of times it takes to kill the enemy in the enemy script
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (col.gameObject.CompareTag("Trampoline"))
        {
            Vector2 vel = rb.velocity;
            Debug.Log("TRAMPOLINE JUMP");
            Debug.Log(jumpforce);
            vel.x = Input.GetAxis("Horizontal") * speed;
            vel.y = jumpforce * 2;
            rb.velocity = vel;
        }
    }

    void UpdateGrounding()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down);
        //if we have collided with a surface
        if (hit.collider != null)
        {
            grounded = true;
        }
        grounded = false;
    }

}