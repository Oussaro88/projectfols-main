using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameManager gameManager;

    private static UIManager instance;
    //public GameObject AchievementMenu;
    //public GameObject ArmoryMenu;

    [SerializeField] private Slider hpBar;
    [SerializeField] private GameObject hpText;
    [SerializeField] private Slider manaBar;
    [SerializeField] private GameObject manaText;
    [SerializeField] private Slider staminaBar;
    [SerializeField] private GameObject staminaText;
    [SerializeField] private GameObject swordImage;
    [SerializeField] private GameObject shieldImage;

    public static UIManager Instance { get => instance; set => instance = value; }
    public Slider HpBar { get => hpBar; set => hpBar = value; }
    public GameObject HpText { get => hpText; set => hpText = value; }
    public Slider ManaBar { get => manaBar; set => manaBar = value; }
    public GameObject ManaText { get => manaText; set => manaText = value; }
    public Slider StaminaBar { get => staminaBar; set => staminaBar = value; }
    public GameObject StaminaText { get => staminaText; set => staminaText = value; }
    public GameObject SwordImage { get => swordImage; set => swordImage = value; }
    public GameObject ShieldImage { get => shieldImage; set => shieldImage = value; }

    [SerializeField] private GameObject[] panels = null;
    [SerializeField] private Selectable[] defaultBtn = null;

    public GameObject[] uiObjects = null;

    [SerializeField] private AudioMixer audioM = null;

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

        foreach(GameObject o in uiObjects)
        {
            o.SetActive(true);
        }
    }
  
    public void PanelToggle(int position)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(position == i);
            if (position == i)
            {
                StartCoroutine(Wait(0, i));
            }
        }
    }

    IEnumerator Wait(float seconds, int index)
    {
        yield return new WaitForSeconds(seconds);
        defaultBtn[index].Select();
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        gameManager = GameManager.instance;

        //Deactivate panels
        panels[0].SetActive(false);
        panels[1].SetActive(false);

        //Recuperate Audio
       
        float v = PlayerPrefs.GetFloat("VolMaster", 0);
        audioM.SetFloat("VolMaster", v);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape) && !gameManager.menuOpened)
        if (gameManager.isPausingGame && !gameManager.menuOpened)
        {
            if (gameManager.Paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        //if (AchievementMenu.activeInHierarchy || ArmoryMenu.activeInHierarchy)
        //{
        //    gameManager.player.GetComponent<PlayerEntity>().Speed = 0;
        //}
        //else
        //{
        //    gameManager.player.GetComponent<PlayerEntity>().Speed = 6;
        //}
    }
    public void ResumeGame()
    {
        panels[0].SetActive(false);
        gameManager.Paused = false;
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        PanelToggle(0);
        gameManager.Paused = true;
        Time.timeScale = 0f;
    }
}
