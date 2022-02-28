using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityClass : MonoBehaviour
{
    public int HP = 1;
    public Transform gitsEffect;

    public virtual void Kill()
    {
        HP = 0; //Sets HP to 0
        Instantiate(gitsEffect, transform.position, transform.rotation); //Spawns the gits with the position and rotation of the entity
        Destroy(this.gameObject); //Destroys the object
    }
}
