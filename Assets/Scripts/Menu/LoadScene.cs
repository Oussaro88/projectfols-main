using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{

    public Animator animator;

    private AsyncOperation async;

    private GameManager gameManager;

    public string  sceneName;

    [SerializeField] private GameObject loaderCanvas;
    [SerializeField] private Image progressBar;
    private float target;

    private bool allowScene;

    //public void BtnLoadScene(int i) //pas de parametres = charge la scene suivante.
    //{
    //    if (async != null) return;
    //    animator.SetTrigger("FadeOut");
    //    StartCoroutine(IELoadSceneInt(i));
    //}

    public async void BtnLoadScene(string s) // s = nom de la scene
    {
        if(SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(0))
        {
            if (gameManager.Paused)
            {
                this.transform.parent.gameObject.SetActive(false);
            }
        }

       // progressBar.fillAmount = 0;

        if (async != null) return;
       // animator.SetTrigger("FadeOut");

        Debug.Log("It's ASYNC...");
        var scene = SceneManager.LoadSceneAsync(s);
        scene.allowSceneActivation = false;

        loaderCanvas.SetActive(true);
        progressBar.fillAmount = 0;
        do
        {
            Time.timeScale = 1; //Make sure it doesn't mess with the pause in other scenes
            await Task.Delay(100);
            progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, 1f, 7f * Time.deltaTime);
        } while (progressBar.fillAmount != 1f);
        animator.SetTrigger("FadeOut");
        await Task.Delay(1500);

        loaderCanvas.SetActive(false);
        scene.allowSceneActivation = true;
        //if (progressBar.fillAmount == 1f)
        //{
        //    //animator.SetTrigger("FadeOut");
        //    loaderCanvas.SetActive(false);
        //    scene.allowSceneActivation = true;

        //}
        //scene.allowSceneActivation = true;
        //loaderCanvas.SetActive(false);
        //StartCoroutine(IELoadSceneString(s));
    }

    public async void BtnLoadSceneI(int indexs) 
    {
        //progressBar.fillAmount = 0;

        Debug.Log("It's BtnLoadSceneI calling...");

        if (async != null) return;
        //animator.SetTrigger("FadeOut");

        Debug.Log("It's ASYNC I...");

        var scene = SceneManager.LoadSceneAsync(indexs);
        scene.allowSceneActivation = false;

        loaderCanvas.SetActive(true);
        progressBar.fillAmount = 0;
        do
        {
            Time.timeScale = 1; //Make sure it doesn't mess with the pause in other scenes
            await Task.Delay(100);
            progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, 1f, 5f * Time.deltaTime);
        } while (progressBar.fillAmount != 1f); //scene.progress < 0.9f && 
        animator.SetTrigger("FadeOut");
        await Task.Delay(1500);

        loaderCanvas.SetActive(false);
        scene.allowSceneActivation = true;
        //if(progressBar.fillAmount == 1f)
        //{
        //    //animator.SetTrigger("FadeOut");
        //    loaderCanvas.SetActive(false);
        //    scene.allowSceneActivation = true;

        //}
        //scene.allowSceneActivation = true;
        //loaderCanvas.SetActive(false);
        //StartCoroutine(IELoadSceneString(s));
    }

    //IEnumerator IELoadSceneInt(int i)
    //{
    //    yield return new WaitForSeconds(1f);
    //    Debug.Log("Play");
    //    async = SceneManager.LoadSceneAsync(i);
    //}

    //IEnumerator IELoadSceneString(string s)
    //{
    //    yield return new WaitForSeconds(1f);
        
    //    async = SceneManager.LoadSceneAsync(s);
    //    async.allowSceneActivation = false;
    //    loaderCanvas.SetActive(true);

    //    do
    //    {
            
    //    } while (async.progress < 0.9f);
    //}

    public void OnLoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/data.game"))
        {
            DataPersistenceManager.instance.LoadGame();
            Debug.Log("Loading Existing Game..." + DataPersistenceManager.instance.GameData.sceneIndex); //In the MainMenu, the data doesn't exist yet

            //Find methods that loads next scene from another scene except MainMenu
            //DataPersistenceManager.instance.SaveGame();
            DataPersistenceManager.instance.newSceneLoading = false;
            BtnLoadSceneI(DataPersistenceManager.instance.GameData.sceneIndex);
        }
        else if (!File.Exists(Application.persistentDataPath + "/data.game"))
        {
            BtnLoadScene("RealHub");
        }
    }

    private void Start()
    {
        gameManager = GameManager.instance;
        //Time.timeScale = 1;
    }

    void Update()
    {
       //progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, target, 3 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject == gameManager.player)
        {
            DataPersistenceManager.instance.newSceneLoading = true;
            BtnLoadScene(sceneName);
        }
    }

    //public void OnFadeCompleteString(string s)
    //{
    //    Debug.Log(sceneToLoad_string);
    //    // async = 
    //    SceneManager.LoadScene(s);
    //}

    //public void OnFadeCompleteInt()
    //{
    //    // async = 
    //    Debug.Log(sceneToLoad_int);
    //    SceneManager.LoadScene(sceneToLoad_int);
    //}
}


