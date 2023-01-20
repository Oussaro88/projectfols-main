using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{

    public bool trapTriggered = false;
    public float motionCountDown;
    public float delay;

    public Animator animator;


    public AudioClip spike;
    public AudioSource audioSpike;

    // Start is called before the first frame update
    void Start()
    {
        motionCountDown = delay;
        audioSpike = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        motionCountDown -= Time.deltaTime;

        if (motionCountDown <= 0)
        {
            if (trapTriggered == false)
            {
                animator.SetBool("Down", false);
                animator.SetBool("Up", true);

                trapTriggered = true;
                audioSpike.PlayOneShot(spike);
            }
            else if (trapTriggered == true)
            {
                animator.SetBool("Up", false);
                animator.SetBool("Down", true);

                trapTriggered = false;
                motionCountDown = delay;
            }

            
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerEntity>())
        {
            other.gameObject.GetComponent<PlayerEntity>().knockDirection = (other.transform.position - transform.position).normalized;
            other.gameObject.GetComponent<PlayerEntity>().knockknock = true;


            //Play knockback animation
            if (!other.gameObject.GetComponent<PlayerEntity>().IsUsingShield)
            {
                if (other.gameObject.GetComponent<PlayerEntity>().isInvincible)
                {
                    other.gameObject.GetComponent<PlayerEntity>().Animator.SetBool("Knocked", false);
                    other.gameObject.GetComponent<PlayerEntity>().OnHurt(0);
                }
                else
                {
                    other.gameObject.GetComponent<PlayerEntity>().isKnocked = true;
                    other.gameObject.GetComponent<PlayerEntity>().Animator.SetBool("Knocked", true);
                    //other.gameObject.GetComponent<PlayerEntity>().KnockBack(hitDirection);
                    other.gameObject.GetComponent<PlayerEntity>().OnHurt(20);

                }
            }
        }
    }
}
