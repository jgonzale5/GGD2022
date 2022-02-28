using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        //If the object that entered this trigger has the component entity class
        if (other.TryGetComponent<EntityClass>(out EntityClass entity))
        {
            //The out keyword determines that entity can and will be modified inside of trygetcomponent
            //Entity is the component retrieved from the object
            entity.Kill(); //The entity is told to die
        }
    }
}
