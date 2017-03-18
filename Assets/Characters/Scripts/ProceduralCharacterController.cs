using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralCharacterController : MonoBehaviour
{

    public Animator animator;
    public GameObject ragdoll;
    private Animator ragDollAnim;
    private new Rigidbody rigidbody;

    public float jumpDistance = 25f;

    private float distToGround;

    // IK
    public Transform leftHandIKTarget;
    public Transform rightHandIKTarget;
    public Transform leftFootIKTarget;
    public Transform rightFootIKTarget;
    public Transform lookAtTarget;

    public float iKPositionWeight = 1f;
    public float iKRotationWeight = 1f;

    public float iKLookWeight = 1f;
    public float iKBodyWeight = 0f;
    public float iKHeadWeight = 1f;
    public float iKEyesWeight = 0f;
    public float iKClampWeight = 1f;

    void Start()
    {
        rigidbody = ragdoll.GetComponentInChildren<Rigidbody>();
        distToGround = ragdoll.GetComponentInChildren<Collider>().bounds.extents.y;
        ragDollAnim = ragdoll.GetComponentInChildren<Animator>();
        Debug.Log(ragDollAnim);
    }

    void Update()
    {
        bool isGrounded = IsGrounded();

        bool jumpButtonDown = Input.GetButtonDown("Jump");

        if (jumpButtonDown && isGrounded)
        {
            // Start jump animation
            animator.SetTrigger("Jump");

            // Add jump force
            rigidbody.velocity = rigidbody.velocity + new Vector3(0, jumpDistance, 0);
        }
    }

    private bool IsGrounded()
    {
        Vector3 start = rigidbody.transform.position;
        Vector3 direction = -Vector3.up;
        float distance = distToGround + 0.2f;

        Debug.DrawLine(rigidbody.transform.position, rigidbody.transform.position + direction * distance, Color.red);
        return Physics.Raycast(rigidbody.transform.position, direction, distance);
    }

    void OnAnimatorIK(int layerIndex)
    {
        if (leftHandIKTarget != null)
        {
            ragDollAnim.SetIKPositionWeight(AvatarIKGoal.LeftHand, iKPositionWeight);
            //anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, iKRotationWeight);
            ragDollAnim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandIKTarget.position);
        }

        if (rightHandIKTarget != null)
        {
            ragDollAnim.SetIKPositionWeight(AvatarIKGoal.RightHand, iKPositionWeight);
            //anim.SetIKRotationWeight(AvatarIKGoal.RightHand, iKRotationWeight);
            ragDollAnim.SetIKPosition(AvatarIKGoal.RightHand, rightHandIKTarget.position);
        }

        if (leftFootIKTarget != null)
        {
            ragDollAnim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, iKPositionWeight);
            //anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, iKRotationWeight);
            ragDollAnim.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootIKTarget.position);
        }

        if (rightFootIKTarget != null)
        {
            ragDollAnim.SetIKPositionWeight(AvatarIKGoal.RightFoot, iKPositionWeight);
            //anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, iKRotationWeight);
            ragDollAnim.SetIKPosition(AvatarIKGoal.RightFoot, rightFootIKTarget.position);
        }

        if (lookAtTarget != null)
        {
            ragDollAnim.SetLookAtWeight(iKLookWeight, iKBodyWeight, iKHeadWeight, iKEyesWeight, iKClampWeight);
            ragDollAnim.SetLookAtPosition(lookAtTarget.position);
        }
    }

}
