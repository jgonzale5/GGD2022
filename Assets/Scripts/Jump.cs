using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public Rigidbody2D reggiebody;
    public float jumpForce;
    bool isGrounded = false;
    public float groundRayLength;
    public LayerMask layers;
    
    void Update()
    {
        isGrounded = Physics2D.Raycast(transform.position, -transform.up, groundRayLength, layers);
        Debug.DrawRay(transform.position, -transform.up * groundRayLength, Color.red);
       
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Debug.Log("Jump is pressed");
            reggiebody.AddForce(Vector2.up * jumpForce);
        }
    }
}
