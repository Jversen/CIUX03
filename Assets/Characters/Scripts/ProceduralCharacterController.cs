using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralCharacterController : MonoBehaviour
{

    public Animator animator;
    public GameObject ragdoll;
    private Animator ragDollAnim;
    private new Rigidbody rigidbody;

    private Transform leftFoot;
    private Transform rightFoot;

    public float jumpDistance = 25f;

    private float distToGround;

    private bool wasGrounded = true;
    private bool wasInAir = false;

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
        //distToGround = ragdoll.GetComponentInChildren<Collider>().bounds.extents.y;
        ragDollAnim = ragdoll.GetComponentInChildren<Animator>();
        leftFoot = ragdoll.transform.Find("Hips/LeftUpLeg/LeftLeg/LeftFoot");
        rightFoot = ragdoll.transform.Find("Hips/RightUpLeg/RightLeg/RightFoot");
    }

    void Update()
    {
        bool isGrounded = IsGrounded();
        //Debug.Log(isGrounded);

        if (!wasGrounded && isGrounded && wasInAir)
        {
            animator.SetBool("Grounded", true);
        }

        bool jumpButtonDown = Input.GetButtonDown("Jump");

        if (jumpButtonDown && isGrounded)
        {
            // Start jump animation
            animator.SetTrigger("Jump");
            animator.SetBool("Grounded", false);

            // Add jump force
            rigidbody.velocity = rigidbody.velocity + new Vector3(0, jumpDistance, 0);
        }

        wasInAir = !isGrounded;
        wasGrounded = isGrounded;
    }

    private bool IsGrounded()
    {
        Vector3 start = rigidbody.transform.position;
        Vector3 direction = -Vector3.up;
        //float distance = distToGround + 0.2f;
        float distance = 0.2f;

        bool leftFootGrounded = Physics.Raycast(leftFoot.transform.position, direction, distance);
        Debug.DrawLine(leftFoot.transform.position, leftFoot.transform.position + direction * distance, Color.red);

        bool rightFootGrounded = Physics.Raycast(rightFoot.transform.position, direction, distance);
        Debug.DrawLine(rightFoot.transform.position, rightFoot.transform.position + direction * distance, Color.red);

        return leftFootGrounded || rightFootGrounded;
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
