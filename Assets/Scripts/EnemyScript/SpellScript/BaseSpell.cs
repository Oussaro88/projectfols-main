using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpell : MonoBehaviour
{
    public GameObject[] warningZone;
    public GameObject[] spellZone;

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

    protected virtual void Start()
    {
        if (startOnce)
        {
            posOrigin = gameObject.transform.position;
            startOnce = false;
            gameObject.SetActive(false);
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

    public abstract void StartSpell();
    public abstract void InitializeSpell();
    public abstract void ShowSpellEffect(GameObject[] list, GameObject zone);
    
}
