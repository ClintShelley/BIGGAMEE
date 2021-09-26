using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    Vector3 velocity;
    public float gravity = -9.8f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    public float jumpHeight = 3f;
    float timeSinceStep = 0;
    Vector3 move;
    [SerializeField] AudioClip footstep1;
    [SerializeField] AudioClip footstep2;
    [SerializeField] AudioClip footstep3;
    [SerializeField] AudioClip footstep4;

    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
         move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        ProcessFootsteps();
        timeSinceStep += Time.deltaTime;
    }
    private void ProcessFootsteps()
    {
        if (isMoving())
        {
                if (timeSinceStep > 0.6)
                {
                int randomSound = Random.Range(1, 4);
                switch (randomSound)
                {
                    case 1:
                        // print("playedsound1");
                        audioSource.PlayOneShot(footstep1);
                        break;
                    case 2:
                        //print("playedsound2");
                        audioSource.PlayOneShot(footstep2);
                        break;
                    case 3:
                        // print("playedsound3");
                        audioSource.PlayOneShot(footstep3);
                        break;
                    case 4:
                        //  print("playedsound4");
                        audioSource.PlayOneShot(footstep4);
                        break;
                }
                timeSinceStep = 0;
                }
        }
    }
    public bool isMoving()
    {
        if (move != Vector3.zero)
        {
            return true;
        }
        return false;
    }
}
