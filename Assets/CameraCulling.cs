using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCulling : MonoBehaviour
{
    RaycastHit hit;
    [SerializeField] LayerMask _mask;
    [SerializeField] float _maxDistance;
    [SerializeField] GameObject _mainCamera;
    [SerializeField] GameObject _cineCam;
    [SerializeField] Transform _lookAt;

    [SerializeField] private bool wallHit;
    [SerializeField] private List<GameObject> _walls2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        wallHit = Physics.Raycast(_cineCam.transform.position, _lookAt.transform.position, out hit, _maxDistance, _mask);

        if(wallHit)
        {
           
            if (!_walls2.Contains(hit.transform.gameObject))
            {
                _walls2.Add(hit.transform.gameObject);
            }
           

            foreach (GameObject obj in _walls2)
            {
                obj.transform.gameObject.GetComponent<MeshRenderer>().material.shader = Shader.Find("Shader Graphs/PhysicalTransparent");
            }
        }
       

        if(!wallHit)
        {
            
            
            foreach (GameObject obj in _walls2)
            {
                obj.transform.gameObject.GetComponent<MeshRenderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                
                if(obj.transform.gameObject.GetComponent<MeshRenderer>().material.shader == Shader.Find("Universal Render Pipeline/Lit"))
                {
                    _walls2.Remove(obj);
                }
            }
        }
    }
}
