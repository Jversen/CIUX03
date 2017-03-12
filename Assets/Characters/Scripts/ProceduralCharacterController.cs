using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralCharacterController : MonoBehaviour
{

    public Animator animator;

    void Start()
    {

    }

    void Update()
    {
        bool jumpButtonDown = Input.GetButtonDown("Jump");

        if (jumpButtonDown)
        {
            animator.SetTrigger("Jump");
        }
    }

}
