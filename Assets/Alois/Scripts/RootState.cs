using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RootState : MonoBehaviour
{
    [SerializeField] Sprite root1;
    [SerializeField] Sprite root2;
    //[SerializeField] GameObject rootContainer;

    // Start is called before the first frame update
    void Start()
    {
        SetSprite();
        SetRotation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool SetSprite()
    {
        float i = Random.Range(0f, 1f);
        if (i < 0.6f)
        {
            GetComponent<Image>().sprite = root1;
            return true;
        }
        else
        {
            GetComponent<Image>().sprite = root2;
            return false;
        }
    }

    int SetRotation()
    {
        int i = Random.Range(0, 6);
        transform.rotation = Quaternion.Euler(0, 0, -60 * (i + 1));
        return i;

    }
}
