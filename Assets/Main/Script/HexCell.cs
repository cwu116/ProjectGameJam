using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCell : MonoBehaviour
{
    [HideInInspector] public Vector2 curPos;
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
            Debug.Log(1111);
        }
    }
}


