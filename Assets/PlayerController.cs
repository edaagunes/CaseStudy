using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Blended;

public class PlayerController : MonoSingleton<PlayerController>
{

    Rigidbody rb;
    Animator anim;
    Vector3 mouseDown;
    public DynamicJoystick js;
    public float moveSpeed;
    enum AnimationState
    {
        idle,
        walk,
        death
    }
    AnimationState animState;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Movement();
    }
    void Movement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDown = Input.mousePosition;
        }
        if (Input.mousePosition != mouseDown)
        {
            rb.velocity = new Vector3(js.Horizontal * moveSpeed, rb.velocity.y, js.Vertical * moveSpeed);
            if (js.Horizontal != 0 || js.Vertical != 0)
            {
                if (rb.velocity != Vector3.zero)
                {

                    transform.rotation = Quaternion.LookRotation(rb.velocity);
                    if (animState != AnimationState.walk)
                    {
                        animState = AnimationState.walk;
                        anim.SetTrigger("isRun");
                    }
                }



            }
            if (js.Horizontal == 0 && js.Vertical == 0)
            {
                if (animState != AnimationState.idle)
                {
                    animState = AnimationState.idle;
                    anim.SetTrigger("isIdle");
                }
            }
        }
    }
    public void Death()
    {
        anim.SetTrigger("isDeath");
        rb.isKinematic = true;
      //GameManager.instance.GameOver();
        GetComponent<PlayerController>().enabled = false;
      //  GetComponent<PlayerAttack>().enabled = false;
    }
}

