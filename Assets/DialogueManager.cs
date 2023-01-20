using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;
    public static DialogueManager Instance { get => instance; set => instance = value; }

    private Queue<string> _sentences;

    public string _sentenceToDisplay;

    public GameObject _dialogueText;

    public bool _conversationIsOver = false;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _sentences = new Queue<string>();
    }

    public void StartDialogue(NpcTrigger _npcTrigger)
    {
        _conversationIsOver = false;
        Debug.Log("Starting conversation with" + _npcTrigger._npcName[0]);

        _sentences.Clear();

        foreach(string sentence in _npcTrigger._sentences)
        {
            _sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(_sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        //string sentence = sentences.Dequeue();
        _sentenceToDisplay = _sentences.Dequeue();
        //_dialogueText.GetComponent<TMPro.TextMeshProUGUI>().text = sentenceToDisplay;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(_sentenceToDisplay));

        Debug.Log(_sentenceToDisplay);
    }

    public IEnumerator TypeSentence(string sentence)
    {
        _dialogueText.GetComponent<TMPro.TextMeshProUGUI>().text = "";
        foreach(char _letter in sentence.ToCharArray())
        {
            _dialogueText.GetComponent<TMPro.TextMeshProUGUI>().text += _letter;
            yield return new WaitForSeconds(0);
        }
    }

    public void EndDialogue()
    {
        Debug.Log("End of conversation.");
        _conversationIsOver = true;
    }

    public IEnumerator ResetConversation()
    {
        _conversationIsOver = false;
        yield return new WaitForSeconds(1f);
    }


}
