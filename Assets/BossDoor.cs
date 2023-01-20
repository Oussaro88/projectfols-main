using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    
    public GameObject animator;
    public bool IsOpen = false;

    GameManager gamemanager;

    private void Start()
    {
        gamemanager = GameManager.instance;        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerEntity>() && IsOpen == false)
        {
            Debug.Log("open");
            animator.GetComponent<Animator>().enabled = true;
            PoolingManager.instance.bossSpawner.GetComponent<BossSpawner>().SpawnBoss();
        }
    }

    public void EndAnimation()
    {
        animator.GetComponent<Animator>().enabled = false;
        IsOpen = true;
    }
}
