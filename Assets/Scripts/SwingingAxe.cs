using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingAxe : MonoBehaviour
{

    public bool rightSide;
    public float motionCountDown;
    public float delay;

    public Animator animator;

    public AudioClip axe;
    public AudioSource audioAxe;

    // Start is called before the first frame update
    void Start()
    {
        motionCountDown = delay;
        audioAxe = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        motionCountDown -= Time.deltaTime;

        if (motionCountDown <= 0)
        {
            if (rightSide == false)
            {
                animator.SetBool("RightSwing", false);
                animator.SetBool("LeftSwing", true);

                rightSide = true;
                audioAxe.PlayOneShot(axe);
            }
            else if (rightSide == true)
            {
                animator.SetBool("LeftSwing", false);
                animator.SetBool("RightSwing", true);
              
                rightSide = false;
                audioAxe.PlayOneShot(axe);
            }

            motionCountDown = delay;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerEntity>())
        {
            //Vector3 hitDirection = (other.transform.position - transform.position).normalized * 10f;
            other.gameObject.GetComponent<PlayerEntity>().knockDirection = (other.transform.position - transform.position).normalized * 5f;
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
