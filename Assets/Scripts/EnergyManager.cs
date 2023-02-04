using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour
{
    public Text energyText;
    public int energyDefault;
    public int energyCount;
    
    public GameObject gamerOverMenu;
    public GameObject settingButton;

    //bool
    public bool energyLost;
    public bool energyAdd;
    
    public void InitEnergyCount()
    {
        energyCount = energyDefault;
        energyText.text = energyCount.ToString();
    }
    
    public void LoseEnergy()
    {
        if (energyCount <= 0)
            return;

        energyCount--;
        energyText.text = energyCount.ToString();

        EndGameByEnergyNumber();
    }
    
    public void AddEnergy()
    {
        energyCount++;
        energyText.text = energyCount.ToString();

        EndGameByEnergyNumber();
    }

    void EndGameByEnergyNumber()
    {
        if (energyCount <= 0)
        {
            Debug.Log("You Lost");
            gamerOverMenu.SetActive(true);
            settingButton.SetActive(false);
            //Reset the game
        }
    }
    
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
