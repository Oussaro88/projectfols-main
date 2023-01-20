using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : MonoBehaviour
{
    private Transform posOrigin; 

    public void PickUpDrop() 
    {
        gameObject.SetActive(false); 
        gameObject.transform.position = posOrigin.position; 
    }

    // Start is called before the first frame update
    void Start()
    {
        posOrigin = transform; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
