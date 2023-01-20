using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IDataPersistence
{
    public Animator animator;

    [SerializeField] private string id;
    [ContextMenu("Generate Guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private bool hasOpened;

    // Start is called before the first frame update
    void Start()
    {
          
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    
    {
        if (GameManager.instance.inventoryscript.keys >= 1)
        {
            animator.SetBool("IsOpen", true);
            GameManager.instance.inventoryscript.DoorOpened();
            hasOpened = true;
        }
        else return;
    }

    public void LoadData(GameData data)
    {
        if (!DataPersistenceManager.instance.newSceneLoading)
        {
            data.doorsTriggered.TryGetValue(id, out hasOpened);
            if (hasOpened)
            {
                animator.SetBool("IsOpen", true);
            }
        }
    }

    public void SaveData(GameData data)
    {
        if (data.doorsTriggered.ContainsKey(id))
        {
            data.doorsTriggered.Remove(id);
        }
        data.doorsTriggered.Add(id, hasOpened);
    }
}
