using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private GameManager gameManager;
    private bool lerpFieldOfView = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerEntity>())
        {
            lerpFieldOfView = true;
            gameManager.cam.m_Lens.FieldOfView = Mathf.Lerp(gameManager.cam.m_Lens.FieldOfView, 20f, 1f * Time.deltaTime);
            //gameManager.cam.m_Lens.NearClipPlane = 13f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerEntity>())
        {
            lerpFieldOfView= false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!lerpFieldOfView)
        {
            gameManager.cam.m_Lens.FieldOfView = Mathf.Lerp(gameManager.cam.m_Lens.FieldOfView, 40f, 1f * Time.deltaTime);
            //gameManager.cam.m_Lens.NearClipPlane = 0.01f;
        }
    }
}
