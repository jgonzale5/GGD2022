using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : PickUpClass
{
    [SerializeField]
    private float speedMultiplier;
    [SerializeField]
    private float boostDuration;

    public override void Execute(EntityClass target)
    {
        StartCoroutine(Boost(target));
    }

    IEnumerator Boost(EntityClass entity)
    {
        float originalSpeed = entity.speed;
        entity.speed *= speedMultiplier;
        yield return new WaitForSeconds(boostDuration);
        entity.speed = originalSpeed;
    }
}
