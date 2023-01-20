using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLoot : MonoBehaviour
{
    public List<GameObject> loot = new List<GameObject>();
    public int minRange = 5;
    public int maxRange = 10;

    private Transform spawnPos;
    //private bool collected;
    public bool spawned;

    private GameManager _gameManager;
    [SerializeField] private GameObject _buttonNameText;
    [SerializeField] private GameObject _interactionButtonText;

    private bool chestOpened;

    private void OnValidate()
    {
        if(minRange > maxRange)
        {
            maxRange = minRange + 1;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnPos = this.gameObject.transform;
        _gameManager = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {

        if(spawned /*&& !collected*/)
        {
            spawned = false;
            TakeLoot();
        }
    }

    public void TakeLoot()
    {
        //collected = true;
        int num = Random.Range(minRange, maxRange);
        StartCoroutine(LootSpawning(num));
        if(this.GetComponent<LootBox>() != null)
        {
            chestOpened = true;
            StartCoroutine(GemSpawning());
        }
    }

    IEnumerator LootSpawning(int number)
    {
        yield return new WaitForSeconds(0.5f);

        for(int i = 0; i< number; i++)
        {
            GameObject tempLoot = Instantiate(loot[0]);
            _gameManager.inventoryscript.lootCollected += 1;
            tempLoot.transform.position = this.spawnPos.position;
            yield return new WaitForSeconds(0.1f);
        }

    }

    IEnumerator GemSpawning()
    {
        yield return new WaitForSeconds(0.5f);

        GameObject tempLoot = Instantiate(loot[1]);
        _gameManager.inventoryscript.lootCollected += 1;
        tempLoot.transform.position = this.spawnPos.position;
        _interactionButtonText.SetActive(false);
        chestOpened = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerEntity>() && !chestOpened)
        {
            switch (_gameManager.inputManager.GetCurrentScheme())
            {
                case "Keyboard&Mouse":
                    _buttonNameText.GetComponent<TMPro.TextMeshProUGUI>().text = _gameManager.inputManager.myInputAction.Player.Interact.bindings[1].ToDisplayString().ToUpper();
                    break;

                case "Gamepad":
                    _buttonNameText.GetComponent<TMPro.TextMeshProUGUI>().text = _gameManager.inputManager.myInputAction.Player.Interact.bindings[0].ToDisplayString().ToUpper();
                    break;
            }

            _interactionButtonText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerEntity>())
        {
            _interactionButtonText.SetActive(false);
        }
    }
}
