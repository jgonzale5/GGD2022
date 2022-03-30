using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityClass : MonoBehaviour
{
    public int HP = 1;
    public Transform gitsEffect;
    //The internal variable used to keep the value of speed
    protected float _speed;
    //This is the property that modifies the value of _speed
    public virtual float speed
    {
        set
        {
            _speed = value;
        }

        get
        {
            return _speed;
        }
    }

    public virtual void Kill()
    {
        HP = 0; //Sets HP to 0
        Instantiate(gitsEffect, transform.position, transform.rotation); //Spawns the gits with the position and rotation of the entity
        Destroy(this.gameObject); //Destroys the object
    }
}
