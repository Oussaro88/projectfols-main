using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevation : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerEntity>())
        {
            other.gameObject.transform.SetParent(this.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerEntity>())
        {
            other.gameObject.transform.SetParent(null);
        }
    }

}
