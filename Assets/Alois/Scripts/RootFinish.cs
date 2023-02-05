using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RootFinish : MonoBehaviour
{
    public Canvas canvas;
    public bool canPutOnRock;
    public Button button;
    public List<RootColliderState> colliderstateList;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(CloseCanvas);
    }

    
    public void CloseCanvas()
    {
        canvas.gameObject.SetActive(false);
        CanPutOnRock();
    }

    void CanPutOnRock()
    {
        canPutOnRock = true;
        for(int i = 0; i< colliderstateList.Count; i++)
        {
            colliderstateList[i].canPutOnRock = true;
        }
    }
}
