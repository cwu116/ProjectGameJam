using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RootFinish : MonoBehaviour
{
    public Canvas canvas;
    bool canPutOnRock;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(CloseCanvas);
        GetComponent<Button>().onClick.AddListener(CanPutOnRock);
    }

    
    void CloseCanvas()
    {
        canvas.gameObject.SetActive(false);
    }

    void CanPutOnRock()
    {
        canPutOnRock = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(canPutOnRock && (collision.CompareTag("mud") ||
            collision.CompareTag("water2") ||
            collision.CompareTag("water3") ||
            collision.CompareTag("water4") ||
            collision.CompareTag("virus2") ||
            collision.CompareTag("virus3") ||
            collision.CompareTag("virus4") ||
            collision.CompareTag("rock") ||
            collision.CompareTag("hope")))
        {
            collision.GetComponent<HexCell>().canPutBranchIn = true;
        }
    }
}
