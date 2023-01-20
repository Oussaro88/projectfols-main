using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



//[RequireComponent (typeof(Dropdown))]

public class SetQuality : MonoBehaviour
{

    public GameObject dropdown;
    private string[] qualities;

    void Start()
    {
        qualities = QualitySettings.names;
        List<string> dropOptions = new List<string>();
        foreach(string s in qualities)
        {
            dropOptions.Add(s);
        }
        dropdown.GetComponent<TMPro.TMP_Dropdown>().ClearOptions();
        dropdown.GetComponent<TMPro.TMP_Dropdown>().AddOptions(dropOptions);
        dropdown.GetComponent<TMPro.TMP_Dropdown>().value = QualitySettings.GetQualityLevel();
    }


    public void SetGFX()
    {
        QualitySettings.SetQualityLevel(dropdown.GetComponent<TMPro.TMP_Dropdown>().value, true);
    }
}
