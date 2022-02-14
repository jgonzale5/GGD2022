using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerScript : MonoBehaviour
{
    public string Tag;
    public Rigidbody reggiebody;
    public float knockbackForce;
    public float screenShakeIntensity;
    public UnityEngine.Events.UnityEvent OnKnockback;

    //private void Update()
    //{
    //    reggiebody.AddForce(this.transform.forward * knockbackForce * Time.deltaTime);
    //}

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
}
