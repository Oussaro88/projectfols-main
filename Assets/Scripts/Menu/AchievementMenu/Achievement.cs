using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement 
{

    private string name;
    private string description;

    private bool unlocked;

    private int points;
    private int spriteIndex;

    private GameObject achievementRef;

    public Achievement(string name, string description, int points, int spriteIndex, GameObject achievementRef)
    {
        this.Name = name;
        this.Description = description;
        this.unlocked = false;
        this.Points = points;
        this.SpriteIndex = spriteIndex;
        this.achievementRef = achievementRef;
        LoadAchievement();
    }

    public string Name { get => name; set => name = value; }
    public string Description { get => description; set => description = value; }
    public int Points { get => points; set => points = value; }
    public int SpriteIndex { get => spriteIndex; set => spriteIndex = value; }

    public bool EarnAchievement()
    {
        if (!unlocked)
        {
            
            achievementRef.GetComponent<Image>().sprite = AchievementManager.Instance.unlockedSprite;
            //unlocked = true;
            SaveAchievement(true);
            return true;
        }
        return false;
    }

    public void SaveAchievement(bool value)
    {
        unlocked = value;

        int tmpPoints = PlayerPrefs.GetInt("Points");

        PlayerPrefs.SetInt("Points", tmpPoints += points);

        PlayerPrefs.SetInt(name, value ? 1 : 0);

        PlayerPrefs.Save();
    }

    public void LoadAchievement()
    {
        unlocked = PlayerPrefs.GetInt(name) == 1 ? true : false;

        if (unlocked)
        {
            AchievementManager.Instance.textPoints.text = "Points: " + PlayerPrefs.GetInt("Points");
            achievementRef.GetComponent<Image>().sprite = AchievementManager.Instance.unlockedSprite;
        }
    }
}
