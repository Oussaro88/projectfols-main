using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AchievementShop : MonoBehaviour, IBaseMenu
{
    private GameManager _gameManager;

    [SerializeField] private GameObject _buttonNameText;
    [SerializeField] private GameObject _interactionButtonText;
    [SerializeField] private GameObject AchievementMenu;
    public Selectable DefaultButton;
    public GameObject defaultList;

    //private float Timer = 0;
    //private bool insideTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;
        //_interactionButtonText.SetActive(false);
       // AchievementMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (AchievementMenu.activeInHierarchy && !insideTrigger)
        //{
        //    Timer += Time.deltaTime;
        //}

        //if (Timer >= 10f && !insideTrigger)
        //{
        //    AchievementMenu.SetActive(false);
        //    Timer = 0;
        //}

        if (_gameManager.player.GetComponent<PlayerEntity>().isCanceling)
        {
            //_interactionButtonText.SetActive(false);
            AchievementMenu.SetActive(false);
            MenuOFF();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerEntity>())
        {
            _interactionButtonText.SetActive(false);
            AchievementMenu.SetActive(false);
            //insideTrigger = false;
            MenuOFF();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerEntity>())
        {
            //insideTrigger = true;
            _interactionButtonText.SetActive(true);

            switch (_gameManager.inputHub.GetCurrentScheme())
            {
                case "Keyboard&Mouse":
                    if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
                    {
                        _buttonNameText.GetComponent<TMPro.TextMeshProUGUI>().text = _gameManager.inputHub.myInputAction.Player.Interact.bindings[1].ToDisplayString().ToUpper();
                    }
                    else
                    {
                        _buttonNameText.GetComponent<TMPro.TextMeshProUGUI>().text = _gameManager.inputManager.myInputAction.Player.Interact.bindings[1].ToDisplayString().ToUpper();
                    }
                    break;

                case "Gamepad":
                    if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
                    {
                        _buttonNameText.GetComponent<TMPro.TextMeshProUGUI>().text = _gameManager.inputHub.myInputAction.Player.Interact.bindings[0].ToDisplayString().ToUpper();
                    }
                    else
                    {
                        _buttonNameText.GetComponent<TMPro.TextMeshProUGUI>().text = _gameManager.inputManager.myInputAction.Player.Interact.bindings[0].ToDisplayString().ToUpper();
                    }
                    break;
            }

            if (_gameManager.player.GetComponent<PlayerEntity>().isInteracting)
            {
                Debug.Log("activateMenu");
                AchievementMenu.SetActive(true);
                DefaultButton.Select();
                defaultList.SetActive(true);
                MenuON();

                switch (_gameManager.inputHub.GetCurrentScheme())
                {
                    case "Keyboard&Mouse":
                        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
                        {
                            _buttonNameText.GetComponent<TMPro.TextMeshProUGUI>().text = _gameManager.inputHub.myInputAction.Player.Cancel.bindings[0].ToDisplayString().ToUpper();
                        }
                        else
                        {
                            _buttonNameText.GetComponent<TMPro.TextMeshProUGUI>().text = _gameManager.inputManager.myInputAction.Player.Cancel.bindings[0].ToDisplayString().ToUpper();
                        }
                        break;

                    case "Gamepad":
                        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
                        {
                            _buttonNameText.GetComponent<TMPro.TextMeshProUGUI>().text = _gameManager.inputHub.myInputAction.Player.Cancel.bindings[1].ToDisplayString().ToUpper();
                        }
                        else
                        {
                            _buttonNameText.GetComponent<TMPro.TextMeshProUGUI>().text = _gameManager.inputManager.myInputAction.Player.Cancel.bindings[1].ToDisplayString().ToUpper();
                        }
                        break;
                }
            }

            //switch (_gameManager.inputManager.GetCurrentScheme())
            //{
            //    case "Keyboard&Mouse":
            //        _buttonNameText.GetComponent<TMPro.TextMeshProUGUI>().text = _gameManager.inputManager.myInputAction.Player.Interact.bindings[1].ToDisplayString().ToUpper();
            //        break;

            //    case "Gamepad":
            //        _buttonNameText.GetComponent<TMPro.TextMeshProUGUI>().text = _gameManager.inputManager.myInputAction.Player.Interact.bindings[0].ToDisplayString().ToUpper();
            //        break;
            //}
        }
    }

    public void Back()
    {
        AchievementMenu.SetActive(false);
        MenuOFF();
    }

    public void MenuON()
    {
        _gameManager.player.GetComponent<CharacterController>().enabled = false;
        _gameManager.player.GetComponent<Animator>().enabled = false;
        _gameManager.menuOpened = true;
    }

    public void MenuOFF()
    {
        _gameManager.player.GetComponent<CharacterController>().enabled = true;
        _gameManager.player.GetComponent<Animator>().enabled = true;
        _gameManager.menuOpened = false;
    }
}
