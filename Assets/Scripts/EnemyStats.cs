using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{

    [SerializeField] private int health;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Projectile"))
        {
            health -= 5;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Stick"))
        {
            health -= 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = health;

        timer += Time.deltaTime;
        //if (Input.GetKeyDown(KeyCode.J))
        //{
            if (timer >= 2f)
            {
                GameObject gameObj = Instantiate(bullet, transform.position + transform.forward * 2, Quaternion.identity); //Instantiation du projectile
                gameObj.GetComponent<Rigidbody>().AddForce(transform.forward * 10, ForceMode.Impulse); //Application de la physique sur le projectile
                timer = 0f;
                Destroy(gameObj, 5f); //Destruction du projectile         
            }
        //}
    }
}
