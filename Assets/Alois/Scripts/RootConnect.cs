using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RootConnect : MonoBehaviour
{
    [SerializeField] GameObject finishButton;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("can connect");
        finishButton.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("can not connect");
        finishButton.SetActive(false);
    }
}
