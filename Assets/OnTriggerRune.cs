using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerRune : MonoBehaviour
{
    private GameManager gameManager;
    public float timer = 0f;
    public bool runeActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerEntity>())
        {
            this.gameObject.GetComponent<MeshRenderer>().material = gameManager.runeActivatedMaterial;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerEntity>())
        {
            runeActivated = true;
            gameManager.runesList.Add(this.gameObject);
            if(gameManager.runesList.Contains(this.gameObject))
            {
                if(!gameManager.runesListIndex.Contains(this.gameObject))
                {
                gameManager.runesListIndex.Add(this.gameObject);
                }
                else
                {
                    return;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if(runeActivated)
        //{
        //    timer += Time.deltaTime;
        //    if (timer >= 20f)
        //    {
        //        this.gameObject.GetComponent<MeshRenderer>().material = gameManager.runeDefaultMaterial;
        //        timer = 0f;
        //        runeActivated = false;
        //        gameManager.runesList.Remove(this.gameObject);
        //        gameManager.runesListIndex.Remove(this.gameObject);
        //    }
        //}

        //if (runeActivated)
        //{
        //    timer += Time.deltaTime;
        //    if (timer >= 20f)
        //    {
        //        this.gameObject.GetComponent<MeshRenderer>().material = gameManager.runeDefaultMaterial;
        //        timer = 0f;
        //        runeActivated = false;
        //        gameManager.runesList.Remove(this.gameObject);
        //        gameManager.runesListIndex.Remove(this.gameObject);
        //    }
        //}

        switch (gameManager.runesList.Count)
        {
            //case 1:
            //    if (gameManager.runesListIndex.IndexOf(gameManager.rune2) != 0 && gameManager.runesListIndex.IndexOf(gameManager.rune1) != 1)
            //    {
            //        foreach (GameObject rune in gameManager.runesList)
            //        {
            //            rune.GetComponent<MeshRenderer>().material = gameManager.runeDefaultMaterial;
            //            rune.GetComponent<OnTriggerRune>().runeActivated = false;
            //        }
            //        gameManager.runesList.Clear();
            //        gameManager.runesListIndex.Clear();
            //    }
            //    break;

            //case 2:
            //    if (gameManager.runesListIndex.IndexOf(gameManager.rune2) != 0 && gameManager.runesListIndex.IndexOf(gameManager.rune1) != 1 && gameManager.runesListIndex.IndexOf(gameManager.rune3) != 2)
            //    {
            //        foreach (GameObject rune in gameManager.runesList)
            //        {
            //            rune.GetComponent<MeshRenderer>().material = gameManager.runeDefaultMaterial;
            //            rune.GetComponent<OnTriggerRune>().runeActivated = false;
            //        }
            //        gameManager.runesList.Clear();
            //        gameManager.runesListIndex.Clear();
            //    }
            //    break;

            //case 3:
            //    if (gameManager.runesListIndex.IndexOf(gameManager.rune2) != 0 && gameManager.runesListIndex.IndexOf(gameManager.rune1) != 1 && gameManager.runesListIndex.IndexOf(gameManager.rune3) != 2 && gameManager.runesListIndex.IndexOf(gameManager.rune4) != 3)
            //    {
            //        foreach (GameObject rune in gameManager.runesList)
            //        {
            //            rune.GetComponent<MeshRenderer>().material = gameManager.runeDefaultMaterial;
            //            rune.GetComponent<OnTriggerRune>().runeActivated = false;
            //        }
            //        gameManager.runesList.Clear();
            //        gameManager.runesListIndex.Clear();
            //    }
            //    break;

            //case 5:
            //    if (gameManager.runesListIndex.IndexOf(gameManager.rune2) != 0 && gameManager.runesListIndex.IndexOf(gameManager.rune1) != 1 && gameManager.runesListIndex.IndexOf(gameManager.rune3) != 2 && gameManager.runesListIndex.IndexOf(gameManager.rune4) != 3 && gameManager.runesListIndex.IndexOf(gameManager.rune5) != 4)
            //    {
            //        foreach (GameObject rune in gameManager.runesList)
            //        {
            //            rune.GetComponent<MeshRenderer>().material = gameManager.runeDefaultMaterial;
            //            rune.GetComponent<OnTriggerRune>().runeActivated = false;
            //        }
            //        gameManager.runesList.Clear();
            //        gameManager.runesListIndex.Clear();
            //    }
            //    break;
        }

    }
}
