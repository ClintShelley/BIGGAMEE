using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimationHandler : MonoBehaviour
{
    // Start is called before the first frame update
    Animation animations;
    FiniteStateMachine fsm;
    string currentStateName;
    PlayerHealth player;
    EnemyHealth health;
    Rigidbody rb;
    NavMeshAgent agent;
    AudioSource audioSource;
    [SerializeField]AudioClip attackSound;
    [SerializeField] AudioClip footstep1;
    [SerializeField] AudioClip footstep2;
    [SerializeField] AudioClip footstep3;
    [SerializeField] AudioClip footstep4;
    [SerializeField] float damage;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        health = GetComponent<EnemyHealth>();
        player = FindObjectOfType<PlayerHealth>();
        animations = GetComponent<Animation>();
        fsm = GetComponent<FiniteStateMachine>();
        agent = GetComponent<NavMeshAgent>();
    }
    public void PlayFootStep()
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
    }
    public void DoDamge()
    {
        audioSource.PlayOneShot(attackSound);
        FindObjectOfType<PlayerHealth>().SubtractHealth(damage);
    }
    // Update is called once per frame
    void Update()
    {
        var vel = rb.velocity;
        var speed = vel.magnitude;
        if (Vector3.Distance(this.transform.position, player.transform.position) < 6f && health.ReturnHealth() > 0)
        {
           // print("attackanim");
            animations.Play("Attack3");
        }
        else if (Vector3.Distance(this.transform.position, player.transform.position) > 6f && health.ReturnHealth() > 0)
        {
            //print("walkanim");
            animations.Play("Walk2");
        }
        else if (health.ReturnHealth() <= 0)
        {
            agent.speed = 0;
            animations.Play("Death");
        }
    }
}
