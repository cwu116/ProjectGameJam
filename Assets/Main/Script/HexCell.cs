using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCell : MonoBehaviour
{
    [HideInInspector] public Vector2 curPos;
    public bool canPutBranchIn ;
    public float weight;



    // Start is called before the first frame update
    void Start()
    {
        
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


