using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioMenuBGM;

    void Start()
    {
        //BGM
        audioSource.PlayOneShot(audioMenuBGM);
    }   

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(Delay(sceneName));
    }

    IEnumerator Delay(string sceneName)
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);
    }

    public void EndGame()
    {
        Debug.Log("End");
        Application.Quit();
    }
    
}
