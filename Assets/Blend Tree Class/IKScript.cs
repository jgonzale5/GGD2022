using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKScript : MonoBehaviour
{

    Animator animator;

    [Header("IK")]
    public float maxRaycastDistance;
    public LayerMask layerMask;
    public Vector3 raycastOriginOffset;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnAnimatorIK(int layerIndex)
    {

        //The weights of the IKs are set
        animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);
        animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);
        animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1f);

        //RIGHT FOOT
        //A ray is casted from the right foot, pointing down from the player
        Ray ray = new Ray(animator.GetIKPosition(AvatarIKGoal.RightFoot) + raycastOriginOffset, -transform.up);
        Debug.DrawRay(ray.origin, ray.direction * maxRaycastDistance);
        RaycastHit hit;

        //If the ray hits something in the layermask and distance we defined
        if (Physics.Raycast(ray, out hit, maxRaycastDistance, layerMask))
        {
            //We set the position of the foot to be the point of contact
            animator.SetIKPosition(AvatarIKGoal.RightFoot, hit.point - raycastOriginOffset);
            animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(transform.forward, hit.normal));
        }

        //LEFT FOOT
        ray = new Ray(animator.GetIKPosition(AvatarIKGoal.LeftFoot) + raycastOriginOffset, -transform.up);
        Debug.DrawRay(ray.origin, ray.direction * maxRaycastDistance);

        //If the ray hits something in the layermask and distance we defined
        if (Physics.Raycast(ray, out hit, maxRaycastDistance, layerMask))
        {
            //We set the position of the foot to be the point of contact
            animator.SetIKPosition(AvatarIKGoal.LeftFoot, hit.point - raycastOriginOffset);
            animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(transform.forward, hit.normal));
        }
    }
}
