using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{
    public Vector3 posOrigin;
    public bool isPooling;
    public bool start = false;
    public float timer = 0f;
    public bool startOnce = true;

    private void Awake()
    {
        if (isPooling)
        {
            posOrigin = this.gameObject.transform.position;
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

            if(timer>= 4f)
            {
                start = false;
                timer = 0;
                ReturnOrigin();
            }
        }
    }
}
