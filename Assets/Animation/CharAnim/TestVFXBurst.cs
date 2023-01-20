using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestVFXBurst : MonoBehaviour
{
    public bool vfxDestroyed;
    public GameObject fireBurst;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        //Destroy(gameObject);
        gameObject.SetActive(false);
        vfxDestroyed = true;
        if (vfxDestroyed)
        {
            Instantiate(fireBurst, transform.position, transform.rotation);
        }
    }
}
