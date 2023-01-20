using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinLoot : MonoBehaviour
{
    private Vector3 velocity = Vector3.up;
    private Rigidbody rb;
    private Vector3 startPos;

    public Animator animatorCoin;

    public AudioClip coins;
    public AudioSource audioCoin;

    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.position;
        velocity *= Random.Range(5f, 7f);
        velocity += new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));

        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
        animatorCoin.enabled = false;
        this.GetComponent<Collider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        rb.position += velocity * Time.deltaTime;

        Quaternion deltaR = Quaternion.Euler(new Vector3(0, 0, Random.Range(-180f, 180f)));
        rb.MoveRotation(rb.rotation * deltaR);

        if (velocity.y < -5f)
        {
            velocity.y = -5f;
        }
        else
        {
            velocity -= Vector3.up * 5 * Time.deltaTime;
        }

        if(Mathf.Abs(rb.position.y - startPos.y) < 0.25f && velocity.y < 0)
        {
            rb.useGravity = true;
            rb.isKinematic = false;

            //rb.velocity = velocity;
            this.enabled = false;
        }

        if (!enabled)
        {
            this.GetComponent<Collider>().enabled = true;
            animatorCoin.enabled = true;
            audioCoin.PlayOneShot(coins);
            Destroy(this.GetComponent<Rigidbody>());
        }
    }
}   
