using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Animator animator;
    public CharacterController characterController;
    public float speed = 1f;
    public float turnspeed = 1f;
    public float gravity = -9.8f;
    public float jumpSpeed = 15f;

    [Header("Run")]
    public KeyCode keyRun = KeyCode.LeftShift;
    public float speedRun = 1.5f;

    private float vSpeed = 0f;
    private void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnspeed * Time.deltaTime, 0);

        var inputAxisVertical = Input.GetAxis("Vertical");
        var speedVector = transform.forward * inputAxisVertical * speed;

        if (characterController.isGrounded)
        {
            vSpeed = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                vSpeed = jumpSpeed;
            }

        }
       var isWalking = inputAxisVertical != 0;
       if (isWalking)
       {
           if (Input.GetKey(keyRun))
           {
                speedVector *= speedRun;
                animator.speed = speedRun;
           }
            else
            {
                animator.speed = 1;
            }
           //
       }
       

        vSpeed -= gravity * Time.deltaTime;
        speedVector.y = vSpeed;

        characterController.Move(speedVector * Time.deltaTime);

        animator.SetBool("Run", isWalking);

    }
}
