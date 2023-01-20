using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vfxtestscript : MonoBehaviour
{
    private bool isVfxDestroyed;

    private GameManager gameManager;
    public bool instatiateOnce = false;


    // Start is called before the first frame update
    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void OnParticleCollision(GameObject other)
    {
        instatiateOnce = true;
        if (instatiateOnce)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                //Debug.Log("isColling");
                GameObject obj = Instantiate(gameManager.explosionBlue, gameManager.explosionBlueTransform.position, transform.rotation);
                Destroy(obj, 2);
                instatiateOnce = false;
            }
    }
}



}
