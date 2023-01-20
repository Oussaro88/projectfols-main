using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ShieldType { Shield1, Shield2, Shield3 };

public class ShieldAttributes : MonoBehaviour
{
    //Variables 
    private float deflectionSpeed; //variable pour la vitesse de déviation
    private float deflectionAngle; //variable pour l'angle de déviation
    [SerializeField] private ShieldType shieldType;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody>())
        {
            if (shieldType == ShieldType.Shield1)
            {
                Vector3 newPos = new Vector3(Random.Range(transform.position.x + 5, transform.position.x - 5), Random.Range(transform.position.y + 5, transform.position.y - 5), Random.Range(transform.position.z + 5, transform.position.z - 5)); //crée une position aléatoir

                deflectionAngle = Mathf.Atan2(transform.position.y, transform.position.x + 10) * Mathf.Rad2Deg;
                //deflectionAngle = transform.eulerAngles.z;

                //collision.gameObject.GetComponent<Rigidbody>().velocity = Quaternion.Euler(0, Random.Range(transform.eulerAngles.x - 20, transform.eulerAngles.x +20), 0) * newPos * deflectionSpeed; //change la direction du projectile selon l'axe y du weapon
                collision.gameObject.GetComponent<Rigidbody>().velocity = Quaternion.Euler(0, Random.Range(deflectionAngle - 20, deflectionAngle + 20), 0) * newPos * deflectionSpeed; //change la direction du projectile selon l'axe y du weapon
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        deflectionSpeed = Random.Range(1, 3);
    }
}
