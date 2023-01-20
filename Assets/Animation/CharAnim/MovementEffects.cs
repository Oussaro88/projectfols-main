using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementEffects : MonoBehaviour
{
    public GameObject leftFoot;
    public GameObject rightFoot;
    public GameObject dustObject;
    private ParticleSystem dust;

    public AudioClip footstepLeft;
    public AudioClip footstepRight;
    public AudioClip diving;
    public AudioSource audioMove;

    public GameObject meleeParticle;
    public ParticleSystem meleevfx;
    public AudioClip swordMelee;
    public AudioClip swordSlash;
    public AudioSource audioSword;

    //public GameObject fireTrail;
    public AudioClip fireExploding;
    public AudioSource audioSpell;

    public AudioClip shield;
    public AudioSource audioShield;


    private Vector3 old_pos;
    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        dust = dustObject.GetComponent<ParticleSystem>();
        meleevfx = meleeParticle.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(old_pos == transform.position)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }

        old_pos = transform.position;
        
    }

    public void MoveLeft()
    {
        if(isMoving)
        {
            dustObject.transform.position = leftFoot.transform.position;
            dust.Play();
            audioMove.PlayOneShot(footstepLeft);
            //Debug.Log("Left");
        }
        
    }

    public void MoveRight()
    {
        if (isMoving)
        {
            dustObject.transform.position = rightFoot.transform.position;
            dust.Play();
            audioMove.PlayOneShot(footstepRight);
            //Debug.Log("Right");
        }
        
    }

    public void SwordMelee()
    {
        meleevfx.Play();
        audioSword.PlayOneShot(swordMelee);
    }

    public void SwordSlash()
    {
        audioSword.PlayOneShot(swordSlash);
    }

    public void FireSpell()
    {
        audioSpell.PlayOneShot(fireExploding);
        //GameObject gameObj = Instantiate(fireTrail, new Vector3(transform.position.x, transform.position.y + 1.25f, transform.position.z) + transform.forward * 1.5f, Quaternion.identity); //Instantiation du projectile
        //gameObj.GetComponent<Rigidbody>().AddForce(transform.forward * 15, ForceMode.Impulse); //Application de la physique sur le projectile
        //Destroy(gameObj, 5f);
    }

    public void Dive()
    {
        audioMove.PlayOneShot(diving);
    }

    public void Shield()
    {
        audioShield.PlayOneShot(shield);
    }

   
}
