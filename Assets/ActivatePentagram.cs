using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActivatePentagram : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;
    [ContextMenu("Generate Guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private GameManager _gameManager;

    [SerializeField] GameObject pentagram;
    [SerializeField] private Slider _activationProgressBar;
    [SerializeField] private Material _pentagramActivatedMaterial;
    [SerializeField] private GameObject progressBar;
    [SerializeField] private GameObject _spiralVfx;
    //[SerializeField] private GameObject circleImage;
    [SerializeField] private GameObject _buttonNameText;
    [SerializeField] private GameObject _interactionButtonText;

    private bool pentagramTriggered;
    public GameObject objUI;
    public bool objActive;
    public TextMeshProUGUI objectiveText;
    public string mission;


    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;
        _activationProgressBar.value = 0;
        if (pentagramTriggered)
        {
            _gameManager.pentagramActivatedindex += 1;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerEntity>() && _gameManager.inventoryscript.keys >= 1)
        {
            _activationProgressBar.gameObject.SetActive(true);
            _interactionButtonText.SetActive(true);

            if (_gameManager.player.GetComponent<PlayerEntity>().isInteracting)
            {
                _spiralVfx.gameObject.SetActive(true);
                _interactionButtonText.SetActive(false);

                _activationProgressBar.value = Mathf.MoveTowards(_activationProgressBar.value, _activationProgressBar.maxValue, 30f * Time.deltaTime);

                if (_activationProgressBar.value == _activationProgressBar.maxValue && !pentagramTriggered)
                {
                    pentagramTriggered = true;
                    pentagram.GetComponent<MeshRenderer>().material = _pentagramActivatedMaterial;
                    progressBar.GetComponent<Image>().color = Color.green;
                    _gameManager.pentagramActivatedindex += 1;
                    _activationProgressBar.gameObject.SetActive(false);
                    _spiralVfx.gameObject.SetActive(false);
                    Destroy(pentagram.GetComponent<SphereCollider>());
                    _gameManager.inventoryscript.DoorOpened();

                    foreach (CloseFightingArea c in FindObjectsOfType<CloseFightingArea>())
                    {
                        c.GetComponent<Collider>().isTrigger = true;
                    }

                    objUI.SetActive(true);
                    objActive = true;
                    objectiveText.text = mission;
                    StartCoroutine(HideObjective());
                    DataPersistenceManager.instance.SaveGame();
                }
            }
            else
            {
                _spiralVfx.gameObject.SetActive(false);
            }


            switch (_gameManager.inputManager.GetCurrentScheme())
            {
                case "Keyboard&Mouse":
                    _buttonNameText.GetComponent<TMPro.TextMeshProUGUI>().text = _gameManager.inputManager.myInputAction.Player.Interact.bindings[1].ToDisplayString().ToUpper();
                    break;

                case "Gamepad":
                    _buttonNameText.GetComponent<TMPro.TextMeshProUGUI>().text = _gameManager.inputManager.myInputAction.Player.Interact.bindings[0].ToDisplayString().ToUpper();
                    break;
            }
        }
    }

    IEnumerator HideObjective()
    {
        yield return new WaitForSeconds(5f);
        objUI.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerEntity>())
        {
            _activationProgressBar.gameObject.SetActive(false);
            _spiralVfx.gameObject.SetActive(false);
            _interactionButtonText.SetActive(false);
            //circleImage.gameObject.SetActive(false);
        }
    }

    private void Update()
    {

    }

    public void LoadData(GameData data)
    {
        if (!DataPersistenceManager.instance.newSceneLoading)
        {
            data.pentagramActivated.TryGetValue(id, out pentagramTriggered);
            if (pentagramTriggered)
            {
                pentagram.GetComponent<MeshRenderer>().material = _pentagramActivatedMaterial;
                progressBar.GetComponent<Image>().color = Color.green;
                _activationProgressBar.gameObject.SetActive(false);
                _spiralVfx.gameObject.SetActive(false);
                Destroy(pentagram.GetComponent<SphereCollider>());
                StartCoroutine(HideObjective());
            }

            objectiveText.text = data.objectiveMission;
        }

    }

    public void SaveData(GameData data)
    {
        if (data.pentagramActivated.ContainsKey(id))
        {
            data.pentagramActivated.Remove(id);
        }
        data.pentagramActivated.Add(id, pentagramTriggered);

        data.objectiveMission = objectiveText.text;
    }
}
