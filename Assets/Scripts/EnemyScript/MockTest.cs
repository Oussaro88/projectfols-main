using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MockTest : MonoBehaviour
{
    public int hp = 100;
    public int hpmax = 100;

    public Slider slider;
    public void DisplayHealthBar()
    {
        slider.value = hp * 100 / hpmax;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DisplayHealthBar();
    }
}
