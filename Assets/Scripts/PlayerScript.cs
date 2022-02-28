using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class PlayerScript : EntityClass
{
    public string Tag;
    public Rigidbody reggiebody;
    public float knockbackForce;
    public float screenShakeIntensity;
    public UnityEngine.Events.UnityEvent OnKnockback;

    [Header("Navigation")]
    public NavMeshAgent agent;

    [Header("Death Sound")]
    public AudioSource audioSource;
    public AudioClip audioClip;

    private void FixedUpdate()
    {
        //InputGetButtonDown("Fire1") returns true when the left click is pressed
        //Then we are calling the function we defined below with the position of the mouse
        if (Input.GetButtonDown("Fire1"))
        {
            MovePlayer(Input.mousePosition);
        }
    }

    public void MovePlayer(Vector2 mousePos)
    {
        //Worldpos is the position hit by the ray, casted in the direction we're clicking in
        //Screentoray produces a ray along the direction of a point on the screen (mousePos)
        //Physics.raycast casts that ray using the physics engine. 
        //Out Racasthit Hitinfo gives us the information of the raycasting (for example, the first point it hits)
        //Mathf.infinity is simply the maximum distance. We could remove it to be honest, as a ray is assumed to be infinite unless
        //otherwise specified.
        //agent.SetDestination tells the player to move to that location.
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
        {
            Vector3 worldPos = hitInfo.point;
            agent.SetDestination(worldPos);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag))
        {
            Vector3 touchDirection = this.transform.position - other.transform.position;

            reggiebody.AddForce(touchDirection.normalized * knockbackForce, ForceMode.Impulse);

            OnKnockback.Invoke();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.name);
        if (collision.gameObject.CompareTag(Tag))
        {
            Vector3 touchDirection = this.transform.position - collision.transform.position;

            reggiebody.AddForce(touchDirection.normalized * knockbackForce, ForceMode.Impulse);

            OnKnockback.Invoke();
        }
    }

    public override void Kill()
    {
        audioSource.PlayOneShot(audioClip);
        Instantiate(gitsEffect, transform.position, transform.rotation); //Spawns the gits with the position and rotation of the entity
        this.gameObject.SetActive(false);
        //Destroy(this.gameObject); //Destroys the object
    }
}
