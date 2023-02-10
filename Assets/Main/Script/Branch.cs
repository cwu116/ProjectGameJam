using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    RootFinish rootfinish;
    RootRevert rootRevert;
    private void Start()
    {
        rootRevert = GetComponent<RootRevert>();
        rootfinish = GetComponent<RootFinish>();
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
            if ((collision.transform.position - transform.position).magnitude < 1)
            {
                //GameObject.Destroy(collision.gameObject);
                collision.gameObject.SetActive(false);
                if(rootfinish)
                    rootfinish.rock = collision.gameObject;
                if (rootRevert)
                    rootRevert.rock = collision.gameObject;

            }
        }
    }
}
