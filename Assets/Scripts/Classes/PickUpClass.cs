using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUpClass : MonoBehaviour
{

    protected void OnTriggerEnter(Collider other)
    {
        //If an entity touches this object
        if (other.TryGetComponent<EntityClass>(out EntityClass entity))
        {
            //We should perform the pickup action with that entity
            Execute(entity);
        }
    }

    public abstract void Execute(EntityClass target);
}
