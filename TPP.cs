using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPP : MonoBehaviour
{

    public CharacterController controller;
    public Transform Cam; // ref for camera
    public Transform GroundCheck;

    public float gravity = -9.81f;
    public float JumpHeight = 3f;
    public float groundDistance = 0.4f;
    public LayerMask groundMask; // LayerMask is a function
    public float speed = 6f;
    public float smoothtime = 0.1f;
    float smoothvelocity; // purpose is to create a ref in float angle

    bool isGrounded;
    Vector3 velocity;


    private void FixedUpdate()// to apply constant gravity
    {
        isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {

            velocity.y = -2f;// is more workable than 0
        }

    }


    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if(direction.magnitude>= 0.1f)
        {
            float TargetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, TargetAngle, ref smoothvelocity, smoothtime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, TargetAngle, 0f) * Vector3.forward; 

            controller.Move(moveDir.normalized * speed * Time.deltaTime);   
        }

        if(Input.GetButtonDown("Jump")&& isGrounded)
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);



    }
}
