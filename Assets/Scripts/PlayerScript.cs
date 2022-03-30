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

    //The internal variable used to keep track of the speed without creating infinite loops.
    public new float _speed;
    //The property that overrides the variable speed in the entity class and defines how _speed is set and get
    public override float speed
    {
        //What happens when the value of speed is changed
        set
        {
            Debug.Log("Speed changed to " + value);
            _speed = value; //The value of the internal variable _speed is set to the new value
            agent.speed = _speed; //The speed of the agent is set to the current value of _speed
        }

        //Whe happens when the value of speed is retrieved
        get
        {
            return _speed; //We simply return the value of _speed. This is the default behavior.
        }
    }

    [Header("Death Sound")]
    public AudioSource audioSource;
    public AudioClip audioClip;

    private void Start()
    {
        agent.speed = speed;
    }

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
