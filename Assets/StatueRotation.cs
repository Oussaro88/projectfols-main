using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueRotation : MonoBehaviour
{
    //private GameManager gameManager;
    //[SerializeField] GameObject flames;
    //Vector3 _newPos;
    //Quaternion _newRot;
    //public bool canShootEnemy = false;
    //float fireRate = 0f;


    //private void Awake()
    //{
    //    //flames.SetActive(false);
    //    canShootEnemy = false;
    //}

    //private void Start()
    //{
    //    gameManager = GameManager.Instance;
    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.GetComponent<PlayerEntity>())
    //    {
    //        _newPos = (gameManager.player.transform.position - this.transform.position).normalized;
    //        Quaternion lookRot = Quaternion.LookRotation(_newPos);
    //        _newRot = Quaternion.Lerp(this.transform.rotation, lookRot, 0.2f * Time.deltaTime);
    //        this.transform.rotation = _newRot;
    //        canShootEnemy = true;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    //flames.SetActive(false);
    //    if(other.gameObject.GetComponent<PlayerEntity>())
    //    {
    //        canShootEnemy = false;
    //    }
    //}

    //private void Update()
    //{
    //    if(canShootEnemy)
    //    {
    //        fireRate += Time.deltaTime;
    //        if(fireRate >= 0.05f)
    //        {
    //            GameObject gameObj = Instantiate(flames, (new Vector3(this.transform.position.x, this.transform.position.y + 1.5f, this.transform.position.z) + this.transform.forward * 1.5f), Quaternion.identity);
    //            gameObj.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 5, ForceMode.Impulse);
    //            fireRate = 0f;
    //            Destroy(gameObj, 0.8f);
    //        }
    //    }
    //}


    [SerializeField] private GameObject flames;
    private GameManager gameManager;
    Vector3 _newPos;
    Quaternion _newRot;

    private void Awake()
    {
        flames.SetActive(false);
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void OnTriggerStay(Collider other)
    {
        flames.SetActive(true);
        _newPos = (gameManager.player.transform.position - this.transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(_newPos);
        _newRot = Quaternion.Lerp(this.transform.rotation, lookRot, 0.2f * Time.deltaTime);
        this.transform.rotation = _newRot;
    }

    private void OnTriggerExit(Collider other)
    {
        flames.SetActive(false);
    }

}
