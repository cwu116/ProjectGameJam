using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RootRotate : MonoBehaviour
{
    public Transform root;
    public Button button;



    // Start is called before the first frame update
    void Start()
    {

        button.onClick.AddListener(RotateSixty);
        

    }

    void RotateSixty()
    {
        root.rotation *= Quaternion.Euler(0, 0, -60);
    }
}
