using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChange : MonoBehaviour
{
    private Color color;
    [SerializeField] private Slider HpBar;

    // Start is called before the first frame update
    void Start()
    {
        HpBar = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        color = Color.Lerp(Color.red, Color.green, HpBar.value / HpBar.maxValue);

        HpBar.GetComponentInChildren<Image>().color = color;
    }
}
