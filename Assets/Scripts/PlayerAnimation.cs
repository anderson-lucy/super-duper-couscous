using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public enum AnimationState
    {
        Idle,
        Walk,
        Jump,
        Crouch,
        Hammer
    }

    public float animationFPS;
    public Sprite[] idleAnimation;
    public Sprite[] walkAnimation;
    public Sprite[] jumpAnimation;
    public Sprite[] crouchAnimation;
    public Sprite[] hammerAnimation;

    private Rigidbody2D rb2d;
    private SpriteRenderer sRenderer;
    //private PlayerController controller;
    private TestController controller;

    private float frameTimer = 0;
    private int frameIndex = 0;
    private AnimationState state = AnimationState.Idle;
    private Dictionary<AnimationState,Sprite[]> animationAtlas;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sRenderer = GetComponent<SpriteRenderer>();
        //controller = GetComponent<PlayerController>();
        controller = GetComponent<TestController>();

        animationAtlas = new Dictionary<AnimationState, Sprite[]>();
        animationAtlas.Add(AnimationState.Idle, idleAnimation);
        animationAtlas.Add(AnimationState.Walk, walkAnimation);
        animationAtlas.Add(AnimationState.Jump, jumpAnimation);
        animationAtlas.Add(AnimationState.Crouch, crouchAnimation);
        animationAtlas.Add(AnimationState.Hammer, hammerAnimation);
    }

    void Update()
    {
        AnimationState newState = GetAnimationState();
        if (state != newState)
        {
            TransistionToState(newState);
        }

        frameTimer -= Time.deltaTime;
        if (frameTimer <= 0.0f)
        {
            frameTimer = 1 / animationFPS;
            Sprite[] anim = animationAtlas[state];
            frameIndex %= anim.Length;
            sRenderer.sprite = anim[frameIndex];
            frameIndex++;
        }

        if (rb2d.velocity.x < -0.1f)
        {
            sRenderer.flipX = true;
        }
        if (rb2d.velocity.x > 0.1f)
        {
            sRenderer.flipX = false;
        }

    }

    void TransistionToState(AnimationState newState)
    {
        frameTimer = 0.0f;
        frameIndex = 0;
        state = newState;
    }

    AnimationState GetAnimationState()
    {
        if (!controller.grounded && state != AnimationState.Crouch)
        {
            Debug.Log("Crouch Up");
            return AnimationState.Crouch;
        }
        if (!controller.grounded && state == AnimationState.Crouch)
        {
            Debug.Log("Jumping");
            return AnimationState.Jump;
        }
        if (controller.grounded && state == AnimationState.Jump)
        {
            Debug.Log("Crouch Down");
            return AnimationState.Crouch;
        }
        if (Mathf.Abs(rb2d.velocity.x) > 0.1f)
        {
            Debug.Log("Walking");
            return AnimationState.Walk;
        }
        if (controller.hammer)
        {
            Debug.Log("Hammer");
            return AnimationState.Hammer;
        }
        return AnimationState.Idle;
    }
}
