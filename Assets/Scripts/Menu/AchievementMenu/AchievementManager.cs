using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class AchievementManager : MonoBehaviour
{
    private GameManager gameManager;

    public GameObject achievementPrefab;

    public Sprite[] sprites;

    public GameObject[] CategoryList;

    public GameObject AchievementMenu;
   
    private AchievementButton activeButton;
    private GameObject activeList;
    public Selectable defaultBtn;

    public ScrollRect scrollRect;

    public GameObject visualAchievement;

    public Dictionary<string, Achievement> achievements = new Dictionary<string, Achievement>();

    public Sprite unlockedSprite;

    public TMP_Text textPoints;

    public Selectable backBtn;

    public int meleeGoblinsKilled;
    public int rangedGoblinsKilled;
    public int bossKilled;

   


    private static AchievementManager instance;

    public static AchievementManager Instance 
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<AchievementManager>();
            }
            return AchievementManager.instance;
        }   
    }

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();

        gameManager = GameManager.instance;

        activeButton = GameObject.Find("GeneralBtn").GetComponent<AchievementButton>();

        CreateAchievement("GeneralCategory", "Die Goblins!", "Kill 5 Melee Goblins", 10, 0);
        CreateAchievement("GeneralCategory", "Die Goblins! 2", "Kill 10 Melee Goblins", 20, 0);

        CreateAchievement("GeneralCategory", "Legolas? Die!", "Kill 5 Ranged Goblins", 10, 0);
        CreateAchievement("GeneralCategory", "Legolas? Die! 2", "Kill 10 Ranged Goblins", 20, 0);

        CreateAchievement("Other", "Too easy!", "Kill Boss Warrior", 30, 0);

        foreach (GameObject achievementList in CategoryList)
        {
            Debug.Log(achievementList);
            achievementList.SetActive(false);
        }

        //activeButton.GetComponent<Image>().sprite = sprites[3];
        //defaultBtn.Select();
        activeList = activeButton.achievementList;
        activeList.SetActive(true);

        AchievementMenu.SetActive(false);
       
    }

    // Update is called once per frame
    void Update()
    {
        meleeGoblinsKilled = PlayerPrefs.GetInt("MeleeGoblinsKilled");
        rangedGoblinsKilled = PlayerPrefs.GetInt("RangedGoblinsKilled");
        //PlayerPrefs.SetInt("GoblinsKilled", goblinsKilled);
        //goblinsKilled = PlayerPrefs.GetInt("GoblinsKilled");
        //activeList.SetActive(true);

        if (Input.GetKeyDown(KeyCode.I))
        {
            AchievementMenu.SetActive(!AchievementMenu.activeSelf);
        }
        if (meleeGoblinsKilled == 5)
        {
            EarnAchievement("Die Goblins!");
        }
        if (meleeGoblinsKilled == 10)
        {
            EarnAchievement("Die Goblins! 2");
        }
        if(rangedGoblinsKilled == 5)
        {
            EarnAchievement("Legolas? Die!");
        }
        if (rangedGoblinsKilled == 10)
        {
            EarnAchievement("Legolas? Die! 2");
        }       
    }

    public void EarnAchievement(string title)
    {
        if (achievements[title].EarnAchievement()) 
        {
            GameObject achievement = (GameObject)Instantiate(visualAchievement);
            SetAchievementInfo("EarnCanvas", achievement, title);
            textPoints.text = "Points: " + PlayerPrefs.GetInt("Points");
            StartCoroutine(HideAchievement(achievement));
        }
    }

    public IEnumerator HideAchievement(GameObject achievement)
    {
        yield return new WaitForSeconds(3);
        Destroy(achievement);
    }


    public void CreateAchievement(string parent, string title, string description,  int points, int spriteIndex)
    {
        GameObject achievement = (GameObject)Instantiate(achievementPrefab);

        Achievement newAchievement = new Achievement(title, description, points, spriteIndex, achievement);

        achievements.Add(title, newAchievement);

        SetAchievementInfo(parent, achievement, title);
    }

    public void SetAchievementInfo(string parent, GameObject achievement, string title)
    {
        achievement.transform.SetParent(GameObject.Find(parent).transform);
        achievement.transform.localScale = new Vector3(1, 1, 1);
        achievement.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = title;
        achievement.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = achievements[title].Description;
        achievement.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = achievements[title].Points.ToString();
        achievement.transform.GetChild(3).GetComponent<Image>().sprite = sprites[achievements[title].SpriteIndex];
    }

    public void ChangeCategory(GameObject button)
    {
        AchievementButton achievementButton = button.GetComponent<AchievementButton>();
        scrollRect.content = achievementButton.achievementList.GetComponent<RectTransform>();
        activeList.SetActive(false);
        activeList = achievementButton.achievementList;
        activeList.SetActive(true);

        //achievementButton.Click();
        //activeButton.Click();
        //activeButton = achievementButton;
    }

    public void Diselect(GameObject btn)
    {
        btn.GetComponent<Image>().sprite = sprites[2];
    }

    public void OnSelect(GameObject btn)
    {
        Debug.Log("Onselecccct");
        btn.GetComponent<Image>().sprite = sprites[3];
    }

    public void GoblinMeleeKilled()
    {
        meleeGoblinsKilled += 1;
        PlayerPrefs.SetInt("MeleeGoblinsKilled", meleeGoblinsKilled);
        meleeGoblinsKilled = PlayerPrefs.GetInt("MeleeGoblinsKilled");
        PlayerPrefs.Save();
    }

    public void GoblinRangedKilled()
    {
        rangedGoblinsKilled += 1;
        PlayerPrefs.SetInt("RangedGoblinsKilled", rangedGoblinsKilled);
        rangedGoblinsKilled = PlayerPrefs.GetInt("RangedGoblinsKilled");
        PlayerPrefs.Save();
    }
}
