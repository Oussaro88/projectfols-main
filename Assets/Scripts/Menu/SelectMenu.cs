using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SelectMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] panels = null;
    [SerializeField] private Selectable[] defaultBtn = null;

    public bool isPaused;

   // private InputManager InputManagerInstance;

    public void PanelToggle (int position)
    {
            for (int i = 0; i < panels.Length; i++)
            {
                panels[i].SetActive(position == i);
            if (position == i)
                {
                    StartCoroutine(Wait(0.1f,i));       
                }
            }
    }

    public void Awake()
    {
        //InputManagerInstance = InputManager.Instance;
    }


    IEnumerator Start()
    {
        yield return new WaitForFixedUpdate();
        Time.timeScale = 1f;
        PanelToggle(0);
        UnlockCursor();
    }

    IEnumerator Wait(float seconds, int index)
    {
        yield return new WaitForSeconds(seconds);
        defaultBtn[index].Select();
    }

    //public void PlayGame()
    //{
    //    StartCoroutine(IEPlayGame());
    //}

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    //IEnumerator IEPlayGame()
    //{
    //    yield return new WaitForSeconds(0.25f);
    //    Debug.Log("Play");
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //}

    public void ResumeGame()
    {
        
    }

    void Update()
    {
        
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
