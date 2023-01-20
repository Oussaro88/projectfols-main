using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChnageShaderTest : MonoBehaviour
{
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material transparentMaterial;

    // Start is called before the first frame update
    void Start()
    {
        defaultMaterial = this.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            this.GetComponent<MeshRenderer>().material.shader = Shader.Find("Shader Graphs/Transparent");
            //this.GetComponent<MeshRenderer>().material.shader.FindPropertyIndex("Shader Graphs/Transparent/Opacity");
            //transparentMaterial.SetFloat("Opacity", 0.5f); 
        }
        //if (Input.GetKeyDown(KeyCode.H))
        //{
        //    this.GetComponent<MeshRenderer>().material = defaultMaterial;
        //}
    }
}
