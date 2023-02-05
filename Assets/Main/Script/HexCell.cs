using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCell : MonoBehaviour
{
    [HideInInspector] public Vector2 curPos;
    public bool canPutBranchIn ;
    public float weight;

    GameObject whiteblock;

    private void Start()
    {
        whiteblock = transform.Find("whiteBlock").gameObject;
    }

    // Start is called before the first frame update
    private void Update()
    {
        if (canPutBranchIn)
            whiteblock.SetActive(true);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EmptySpace"))
        {
            GameObject.Destroy(this.gameObject);
        }


        if (collision.CompareTag("BranchUI"))
        {


        }

        if (collision.CompareTag("Branch"))
        {
            Debug.Log(collision.gameObject.name);
            if (collision.GetComponent<RootColliderState>().canPutOnRock)
                canPutBranchIn = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.CompareTag("Branch"))
        {
            if (collision.GetComponent<RootColliderState>().canPutOnRock)
                canPutBranchIn = true;
        }
    }


}


