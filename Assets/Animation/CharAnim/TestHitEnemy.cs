using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHitEnemy : MonoBehaviour
{

    public CharMovementTest weapon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy" && weapon.isAttacking)
        {
            Debug.Log(other.name);
            other.GetComponent<Animator>().SetTrigger("Hit");
        }
    }
}
