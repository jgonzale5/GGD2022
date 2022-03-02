using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineScript : MonoBehaviour
{
    public bool defused = false;
    public Renderer rend;
    public Color offColor;
    public float blinkTime;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        //If the object that entered this trigger has the component entity class
        if (!defused && other.TryGetComponent<EntityClass>(out EntityClass entity))
        {
            //The out keyword determines that entity can and will be modified inside of trygetcomponent
            //Entity is the component retrieved from the object
            entity.Kill(); //The entity is told to die


            StartCoroutine(BlinkColor());
        }
    }

    IEnumerator BlinkColor()
    {
        Color origColor = rend.material.color;

        rend.material.color = offColor;
        defused = true;

        yield return new WaitForSeconds(blinkTime);

        rend.material.color = origColor;
        defused = false;
    }
}
