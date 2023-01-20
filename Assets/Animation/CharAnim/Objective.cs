using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Objective : MonoBehaviour, IDataPersistence
{

    private GameManager manager;

    private DataPersistenceManager dpManager;

    [SerializeField] private string id;
    [ContextMenu("Generate Guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public GameObject objUI;
    public bool objActive;
    public TextMeshProUGUI objectiveText;
    private bool collision;
    public string mission;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.Instance;
        dpManager = DataPersistenceManager.instance;
        objActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == manager.player && !collision)
        {
            objUI.SetActive(true);
            objActive = true;
            collision = true;
            
            objectiveText.text = mission;

            //foreach(CloseFightingArea c in FindObjectsOfType<CloseFightingArea>())
            //{
            //    c.GetComponent<Collider>().isTrigger = true;
            //}

            StartCoroutine(HideObjective());
            dpManager.SaveGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.gameObject.activeInHierarchy)
        {
            objUI.SetActive(false);
        }
    }

    IEnumerator HideObjective()
    {
        yield return new WaitForSeconds(5f);
        objUI.SetActive(false);
        if(SceneManager.GetActiveScene().buildIndex != 1)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void LoadData(GameData data)
    {
        if (!DataPersistenceManager.instance.newSceneLoading)
        {
            data.objectiveTriggered.TryGetValue(id, out collision);
            if (collision)
            {
                if (SceneManager.GetActiveScene().buildIndex == 1)
                {
                    collision = false;
                }

                StartCoroutine(HideObjective());
                //this.gameObject.SetActive(false);
            }

            objectiveText.text = data.objectiveMission;
        }
        
    }

    public void SaveData(GameData data)
    {
        if (data.objectiveTriggered.ContainsKey(id))
        {
            data.objectiveTriggered.Remove(id);
        }
        data.objectiveTriggered.Add(id, collision);

        data.objectiveMission = objectiveText.text;

        data.sceneIndex = SceneManager.GetActiveScene().buildIndex;
        
    }
}
