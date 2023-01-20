using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;

    public float timer;
    public GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.instance;
        target = manager.player.transform;
        timer = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == manager.player)
        {
            manager.inventoryscript.CoinPickup(); 
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 3f)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y + 1.5f, target.position.z), 20 * Time.deltaTime);
        }
        timer += Time.deltaTime;
    }

}
