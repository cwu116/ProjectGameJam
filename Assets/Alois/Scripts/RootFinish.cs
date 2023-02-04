using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RootFinish : MonoBehaviour
{
    public Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(CloseCanvas);
    }

    
    void CloseCanvas()
    {
        canvas.gameObject.SetActive(false);
    }
}
