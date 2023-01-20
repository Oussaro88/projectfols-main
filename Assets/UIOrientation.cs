//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class UIOrientation : MonoBehaviour
//{
//    [SerializeField] private GameObject _canvas;
//    [SerializeField] private GameObject cam;

//    private GameManager _gameManager;
//    [SerializeField] private GameObject _buttonNameText;
//    [SerializeField] private GameObject _interactionButtonText;
//    [SerializeField] private GameObject _player;
//    [SerializeField] private GameObject _npc;
//    private Animator _animator;

//    bool _isTalking = false;
//    Vector3 _newPos;
//    Quaternion lookRot;
//    Quaternion _newRot;

//    // Start is called before the first frame update
//    void Start()
//    {
//        _gameManager = GameManager.Instance;
//        _interactionButtonText.SetActive(false);
//        _canvas.SetActive(false);
//        _animator = GetComponent<Animator>();
//    }

//    private void OnTriggerStay(Collider other)
//    {
//        if (other.gameObject.GetComponent<PlayerEntity>())
//        {
//            _interactionButtonText.SetActive(true);

//            if (_gameManager.player.GetComponent<PlayerEntity>().isInteracting)
//            {
//                _animator.SetBool("Talking", true);
//                _isTalking = true;
//                _canvas.SetActive(true);
//            }
//        }

//        switch (_gameManager.inputManager.GetCurrentScheme())
//        {
//            case "Keyboard&Mouse":
//                _buttonNameText.GetComponent<TMPro.TextMeshProUGUI>().text = _gameManager.inputManager.myInputAction.Player.Interact.bindings[1].ToDisplayString().ToUpper();
//                break;

//            case "Gamepad":
//                _buttonNameText.GetComponent<TMPro.TextMeshProUGUI>().text = _gameManager.inputManager.myInputAction.Player.Interact.bindings[0].ToDisplayString().ToUpper();
//                break;
//        }
//    }

//    private void OnTriggerExit(Collider other)
//    {
//        _canvas.SetActive(false);
//        _interactionButtonText.SetActive(false);
//        _animator.SetBool("Talking", false);
//        _isTalking = false;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        _canvas.transform.LookAt(cam.transform);

//        if (_isTalking)
//        {
//            this.transform.LookAt(_player.transform);
//            _newPos = (_player.transform.position - _npc.transform.position).normalized;
//            lookRot = Quaternion.LookRotation(_newPos);
//            _newRot = Quaternion.Lerp(this.transform.rotation, lookRot, 0.3f * Time.deltaTime);
//        }

//        if (_gameManager.player.GetComponent<PlayerEntity>().isCanceling)
//        {
//            _canvas.SetActive(false);
//            _interactionButtonText.SetActive(false);
//            _animator.SetBool("Talking", false);
//            _animator.SetBool("Talking", true);
//        }
//    }
//}
