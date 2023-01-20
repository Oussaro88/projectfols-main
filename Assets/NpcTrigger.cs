using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcTrigger : MonoBehaviour
{
    private GameManager _gameManager;

    public string[] _npcName;

    [TextArea(3, 6)]
    public string[] _sentences;

    public GameObject _dialogueBoxUI;
    [SerializeField] private GameObject _nameText;

    [SerializeField] private GameObject _buttonNameText;
    [SerializeField] private GameObject _interactionButtonText;

    public GameObject _signUI;
    public Transform _chatBackGround;
    public Transform _npc;
    private Vector3 _camPos;

    private float _tipTimer = 0;
    private bool _insideTrigger = false;

    private Animator _animator;
    private bool _isTalking = false;
    private bool _isInRange = false;
    Vector3 _newPos;
    Quaternion lookRot;
    Quaternion _newRot;

    public Canvas signUI;
    public Canvas chatBoxUI;


    private void Start()
    {
        _gameManager = GameManager.Instance;
        _npc = this.transform.gameObject.transform;
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        //_camPos = Camera.main.WorldToScreenPoint(_npc.position);
        //_camPos.y += 200;
        //_camPos.x -= 80;
        //_chatBackGround.transform.position = _camPos;
        //_signUI.transform.position = new Vector3 (_camPos.x + 80, _camPos.y - 70, _camPos.z);

        if (_dialogueBoxUI.activeInHierarchy && !_insideTrigger)
        {
            _tipTimer += Time.deltaTime;
        }

        if (_tipTimer >= 1f && !_insideTrigger)
        {
            _dialogueBoxUI.SetActive(false);
            _tipTimer = 0;
        }

        if (_isInRange)
        {
            _interactionButtonText.SetActive(true);
            _animator.SetBool("Talking", true);
            //this.transform.LookAt(_gameManager.player.transform);
            //_newPos = (_gameManager.player.transform.position - _npc.transform.position).normalized;
            //lookRot = Quaternion.LookRotation(_newPos);
            //_newRot = Quaternion.Lerp(this.transform.rotation, lookRot, 0.3f * Time.deltaTime);
        }

        //if(_isTalking && _isInRange)
        //{
        //    _interactionButtonText.SetActive(false);
        //}

        signUI.transform.LookAt(_gameManager.cameraMain.transform);
        chatBoxUI.transform.LookAt(_gameManager.cameraMain.transform);
    }

    public void TriggerDialogue()
    {
        _dialogueBoxUI.SetActive(true);
        FindObjectOfType<DialogueManager>().StartDialogue(this);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerEntity>())
        {
            if (!_isTalking)
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
            }
                

            _isInRange = true;
            _insideTrigger = true;
            _interactionButtonText.SetActive(true);
            _nameText.GetComponent<TMPro.TextMeshProUGUI>().text = _npcName[0];


            if (_gameManager.player.GetComponent<PlayerEntity>().isInteracting)
            {
                _signUI.SetActive(false);
                _isTalking = true;
                TriggerDialogue();

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

            if (_gameManager.player.GetComponent<PlayerEntity>().isCanceling && _dialogueBoxUI.activeInHierarchy)
            {
                FindObjectOfType<DialogueManager>().DisplayNextSentence();
            }

            if(_gameManager._dialogueManager._conversationIsOver)
            {
                _dialogueBoxUI.SetActive(false);
                _gameManager._dialogueManager.ResetConversation();
                _isTalking = false;
                _signUI.SetActive(true);
            }
        }

        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerEntity>())
        {
            _insideTrigger = true;
            _dialogueBoxUI.SetActive(false);
            _interactionButtonText.SetActive(false);
            _gameManager._dialogueManager._conversationIsOver = false;
            _animator.SetBool("Talking", false);
            _isTalking = false;
            _isInRange = false;
            _signUI.SetActive(true);
        }
    }
}
