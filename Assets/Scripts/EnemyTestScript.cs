using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTestScript : MonoBehaviour
{
    WaveSpawner waveSpawnerScript;
    public bool Alive = true;

    public GameObject coin;

    // Start is called before the first frame update
    void Start()
    {
        waveSpawnerScript = FindObjectOfType<WaveSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Alive == false)
        {
            waveSpawnerScript.EnemyCount(-1);
            CoinDrop();
            gameObject.SetActive(false);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Stick")
        {
            waveSpawnerScript.EnemyCount(-1);
            gameObject.SetActive(false);
        }
    }

    public void CoinDrop()
    {
        int ranNum = Random.Range(0,99);

        if (ranNum <= 24)
        {
            Instantiate(coin, this.transform.position, this.transform.rotation);
        }
    }
}
