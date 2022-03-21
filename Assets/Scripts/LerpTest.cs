using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LerpTest : MonoBehaviour
{
    public Transform APosition;
    public Transform BPosition;

    public Slider slider;

    private void Start()
    {
        MoveObject(slider.normalizedValue);
    }

    public void MoveObject()
    {
        float value = slider.normalizedValue;

        this.transform.position = Vector3.Lerp(APosition.position, BPosition.position, value);
    }

    public void MoveObject(float value)
    {
        this.transform.position = Vector3.Lerp(APosition.position, BPosition.position, value);
    }
}
