using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI energyText;
    [SerializeField] TextMeshProUGUI depthText;

    int lastFrameEnergy;
    int lastFrameStep;

    // Start is called before the first frame update
    void Start()
    {
        lastFrameEnergy = GameManager.instance.energy;
        lastFrameStep = GameManager.instance.steps;

        energyText.text = "Energy: " + GameManager.instance.energy;
        depthText.text = "Depth: " + GameManager.instance.energy * 10;
    }

    // Update is called once per frame
    void Update()
    {
        //if (lastFrameEnergy != GameManager.instance.energy || lastFrameStep != GameManager.instance.steps)
        //{
            //energyText.text = "Energy: " + GameManager.instance.energy;
            //depthText.text = "Depth: " + GameManager.instance.energy * 10;
            //UpdateParameters();
        //}
    }

    void UpdateParameters()
    {
        lastFrameEnergy = GameManager.instance.energy;
        lastFrameStep = GameManager.instance.steps;
    }
}
