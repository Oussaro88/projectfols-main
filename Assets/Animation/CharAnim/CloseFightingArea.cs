using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseFightingArea : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;
    [ContextMenu("Generate Guid for id")]
    private void GenerateGuid()
    {
        Id = System.Guid.NewGuid().ToString();
    }

    private GameManager manager;

    public GameObject spawnPointPlayer;

    public bool triggered;

    public string Id { get => id; set => id = value; }

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.instance;
        this.gameObject.GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.gameObject == manager.player)
        {
            StartCoroutine(ColliderOn());
        }
    }

    IEnumerator ColliderOn()
    {
        yield return new WaitForSeconds(0.5f);
        foreach (CloseFightingArea c in FindObjectsOfType<CloseFightingArea>())
        {
            if(c.Id.ToString() == this.Id.ToString())
            {
                c.gameObject.GetComponent<Collider>().isTrigger = false;
                c.triggered = true;
            }
        }
        this.gameObject.GetComponent<Collider>().isTrigger = false;
        manager.player.transform.position = spawnPointPlayer.transform.position;
        triggered = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadData(GameData data)
    {
        if (!DataPersistenceManager.instance.newSceneLoading)
        {
            data.collidersFight.TryGetValue(Id, out triggered);
            if (triggered)
            {
                this.gameObject.SetActive(false);
            }
        }

    }

    public void SaveData(GameData data)
    {
        if (data.collidersFight.ContainsKey(Id))
        {
            data.collidersFight.Remove(Id);
        }
        data.collidersFight.Add(Id, triggered);
    }
}
