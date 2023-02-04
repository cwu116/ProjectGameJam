using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    
    public void Restart(string sceneName)
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

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
    
}
