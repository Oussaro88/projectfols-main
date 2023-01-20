using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flameDamage : MonoBehaviour
{
    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.GetComponent<PlayerEntity>())
    //    {
    //        Debug.Log("Colliding");
    //        other.gameObject.GetComponent<PlayerEntity>().OnHurt(1);
    //    }
    //}

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.GetComponent<PlayerEntity>())
        {
            Debug.Log("Colliding");
            other.gameObject.GetComponent<PlayerEntity>().OnHurt(1);
        }
    }
}
