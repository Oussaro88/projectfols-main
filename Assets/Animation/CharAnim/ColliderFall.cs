using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderFall : MonoBehaviour
{
    public GameObject spawnPointPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == GameManager.instance.player)
        {
            GameManager.instance.player.transform.position = spawnPointPlayer.transform.position;
            GameManager.instance.player.GetComponent<PlayerEntity>().OnHurt(Random.Range(5, 10));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == GameManager.instance.player)
        {
            GameManager.instance.player.transform.position = spawnPointPlayer.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
