using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKController : MonoBehaviour {

    private Animator anim;

    public Transform leftHandIKTarget;
    public Transform rightHandIKTarget;
    public Transform leftFootIKTarget;
    public Transform rightFootIKTarget;
    public Transform lookAtTarget;

    public float iKPositionWeight = 1f;
    public float iKRotationWeight = 1f;

    public float iKLookWeight = 1f;
    public float iKBodyWeight = 0.5f;
    public float iKHeadWeight = 1f;
    public float iKEyesWeight = 0.5f;
    public float iKClampWeight = 1f;

    void Start () {
        anim = GetComponentInChildren<Animator>();
    }
	
	void Update () {
		
	}

    void OnAnimatorIK(int layerIndex)
    {
        Debug.Log("Animate");
        if (leftHandIKTarget != null)
        {
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, iKPositionWeight);
            //anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, iKRotationWeight);
            anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandIKTarget.position);
        }

        if (rightHandIKTarget != null)
        {
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, iKPositionWeight);
            //anim.SetIKRotationWeight(AvatarIKGoal.RightHand, iKRotationWeight);
            anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandIKTarget.position);
        }

        if (leftFootIKTarget != null)
        {
            anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, iKPositionWeight);
            //anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, iKRotationWeight);
            anim.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootIKTarget.position);
        }

        if (rightFootIKTarget != null)
        {
            anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, iKPositionWeight);
            //anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, iKRotationWeight);
            anim.SetIKPosition(AvatarIKGoal.RightFoot, rightFootIKTarget.position);
        }

        if (lookAtTarget != null)
        {
            anim.SetLookAtWeight(iKLookWeight, iKBodyWeight, iKHeadWeight, iKEyesWeight, iKClampWeight);
            anim.SetLookAtPosition(lookAtTarget.position);
        }
    }

}
