using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitVFX : MonoBehaviour
{
    public ParticleSystem hitVFX;
    public ParticleSystem hitVFX2;

    public bool startOnce = true;
    public bool start = false;
    public float timer = 0f;

    public Vector3 posOrigin;
    public bool isPooling;

    private void Awake()
    {
        if (isPooling)
        {
            posOrigin = gameObject.transform.position;
        }
    }

    public void ReturnOrigin()
    {
        if (isPooling)
        {
            gameObject.transform.position = posOrigin;
        }
        gameObject.SetActive(false);
    }


    public void StartVFX()
    {
        start = true;
        timer = 0f;
        hitVFX.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (startOnce)
        {
            posOrigin = gameObject.transform.position;
            startOnce = false;
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            timer += Time.deltaTime;

            if (timer >= 2f)
            {
                timer = 0;
                start = false;
                ReturnOrigin();
                //this.gameObject.SetActive(false);
            }
        }
    }
}
