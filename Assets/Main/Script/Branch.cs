using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if ((collision.CompareTag("mud") ||
            collision.CompareTag("water2") ||
            collision.CompareTag("water3") ||
            collision.CompareTag("water4") ||
            collision.CompareTag("virus2") ||
            collision.CompareTag("virus3") ||
            collision.CompareTag("virus4") ||
            collision.CompareTag("rock") ||
            collision.CompareTag("hope")))
        {
               if((collision.transform.position - transform.position).magnitude < 1)
                GameObject.Destroy(collision.gameObject);
        }
    }
}
