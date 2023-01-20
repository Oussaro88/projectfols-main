using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelsManager : MonoBehaviour
{
    [SerializeField] private GameObject[] panels = null;
    [SerializeField] private Selectable[] defaultBtn = null;

    public bool isPaused;

    // private InputManager InputManagerInstance;

    public void PanelToggle(int position)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(position == i);
            if (position == i)
            {
                defaultBtn[i].Select();
                //StartCoroutine(Wait(0.1f, i));
            }
        }
    }

    //IEnumerator Wait(float seconds, int index)
    //{
    //    yield return new WaitForSeconds(seconds);
    //    defaultBtn[index].Select();
    //}

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        panels[0].SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void PauseGame()
    {
        PanelToggle(0);
        UnlockCursor();
        Time.timeScale = 0.001f;
        isPaused = true;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
