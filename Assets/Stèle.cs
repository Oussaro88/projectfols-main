using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stèle : MonoBehaviour, IBaseMenu
{
    private GameManager _gameManager;
    [SerializeField] private GameObject _vfx;
    [SerializeField] private GameObject _buttonNameText;
    [SerializeField] private GameObject _interactionButtonText;
    [SerializeField] private GameObject _tipsPanel;
    //private int index;
    //private float tipTimer= 0;
    //private bool insideTrigger = false;



    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerEntity>())
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

            //insideTrigger = true;
            _interactionButtonText.SetActive(true);

            if(_gameManager.player.GetComponent<PlayerEntity>().isInteracting)
            {
                _tipsPanel.SetActive(true);
                //index = 1;

                MenuON();

                switch (_gameManager.inputManager.GetCurrentScheme())
                {
                    case "Keyboard&Mouse":
                        _buttonNameText.GetComponent<TMPro.TextMeshProUGUI>().text = _gameManager.inputManager.myInputAction.Player.Cancel.bindings[0].ToDisplayString().ToUpper();
                        break;

                    case "Gamepad":
                        _buttonNameText.GetComponent<TMPro.TextMeshProUGUI>().text = _gameManager.inputManager.myInputAction.Player.Cancel.bindings[1].ToDisplayString().ToUpper();
                        break;
                }
            }

            //else if (_gameManager.player.GetComponent<PlayerEntity>().isCanceling)
            //{
            //    _interactionButtonText.SetActive(false);
            //    _tipsPanel.SetActive(false);
            //    //index = 0;
            //}

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerEntity>())
        {
            _interactionButtonText.SetActive(false);
            //_tipsPanel.SetActive(false);
            //index = 0;
            //insideTrigger = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;
        _interactionButtonText.SetActive(false);
        _tipsPanel.SetActive(false);
        //index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //if(_tipsPanel.activeInHierarchy && !insideTrigger)
        //{
        //    tipTimer += Time.deltaTime;
        //}

        //if (_gameManager.player.GetComponent<PlayerEntity>().isInteracting && _tipsPanel.activeInHierarchy || tipTimer >= 10f && !insideTrigger)
        //{
        //    _tipsPanel.SetActive(false);
        //    //index = 0;
        //    tipTimer = 0;
        //}

        if (_gameManager.player.GetComponent<PlayerEntity>().isCanceling)
        {
            _interactionButtonText.SetActive(false);
            _tipsPanel.SetActive(false);
            //index = 0;
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
