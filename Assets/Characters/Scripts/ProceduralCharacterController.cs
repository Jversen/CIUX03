using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralCharacterController : MonoBehaviour
{

    public bool matchAnimation = false;

    public Animator animator;
    public GameObject ragdoll;
    private new Rigidbody rigidbody;

    private Transform leftFoot;
    private Transform rightFoot;

    public float jumpDistance = 25f;

    private float distToGround;

    private bool wasGrounded = true;
    private bool wasInAir = false;

    private Transform[] ragdollTransforms;
    private Transform[] animatorTransforms;

    void Start()
    {
        rigidbody = ragdoll.GetComponentInChildren<Rigidbody>();
        //distToGround = ragdoll.GetComponentInChildren<Collider>().bounds.extents.y;
        leftFoot = ragdoll.transform.Find("Hips/LeftUpLeg/LeftLeg/LeftFoot");
        rightFoot = ragdoll.transform.Find("Hips/RightUpLeg/RightLeg/RightFoot");

        ragdollTransforms = ragdoll.transform.Find("Hips").GetComponentsInChildren<Transform>();
        animatorTransforms = animator.transform.Find("Hips").GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        if (matchAnimation)
        {
            matchJoints();
        }

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

        bool fire1ButtonDown = Input.GetButtonDown("Fire1");

        if (fire1ButtonDown)
        {
            animator.SetTrigger("Punch");
        }

        wasInAir = !isGrounded;
        wasGrounded = isGrounded;
    }

    private void matchJoints()
    {
        for (int i = 0; i < ragdollTransforms.Length; i ++)
        {
            ragdollTransforms[i].rotation = animatorTransforms[i].rotation;
        }
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

}
