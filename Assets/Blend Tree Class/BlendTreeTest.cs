using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendTreeTest : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;
    public float walkSpeed;
    public float runSpeed;

    Vector3 velocity = Vector3.zero;
    public float gravity = 9.8f;

    void Update()
    {
        // apply gravity 
        velocity.y -= gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);


        float speed = walkSpeed * Time.deltaTime;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            z *= 2;
            speed = runSpeed * Time.deltaTime;
        }

        animator.SetFloat("x", x);
        animator.SetFloat("z", z);

        controller.Move(new Vector3(x * speed, 0, z * speed));


        if (Input.GetMouseButton(0))
        {
            //animator.SetTrigger("Shoot");
            animator.SetBool("Shooting", true);
        }
        else
        {
            animator.SetBool("Shooting", false);
        }
    }
}
