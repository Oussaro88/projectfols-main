using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementButton : MonoBehaviour
{

    public GameObject achievementList;

    public Sprite neutral, highlight;

    private Image sprite;


    private void Awake()
    {
        sprite = GetComponent<Image>();
    }

    public void Click()
    {
        if(sprite.sprite == highlight)
        {
            sprite.sprite = neutral;
            achievementList.SetActive(true);
        }
        else
        {
            sprite.sprite = highlight;
            achievementList.SetActive(false);
        }
    }
}
