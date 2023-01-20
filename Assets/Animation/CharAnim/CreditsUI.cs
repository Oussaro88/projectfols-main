using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsUI : MonoBehaviour, IBaseMenu
{
    private GameManager _gameManager;
    [SerializeField] private GameObject _buttonNameText;
    [SerializeField] private GameObject _interactionButtonText;
    public GameObject credits;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.instance;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerEntity>())
        {
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
                credits.SetActive(true);
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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerEntity>())
        {
            _interactionButtonText.SetActive(false);
            credits.SetActive(false);
            MenuOFF();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameManager.player.GetComponent<PlayerEntity>().isCanceling)
        {
            credits.SetActive(false);
            MenuOFF();
        }
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
