using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "mud")
        {
            GameManager.instance.energy--;
        }

        if (other.tag == "water2")
        {
            GameManager.instance.energy += 2;
        }

        if (other.tag == "water3")
        {
            GameManager.instance.energy += 3;
        }

        if (other.tag == "water4")
        {
            GameManager.instance.energy += 4;
        }

        if (other.tag == "virus2")
        {
            GameManager.instance.energy -= 2;
        }

        if (other.tag == "virus3")
        {
            GameManager.instance.energy -= 3;
        }

        if (other.tag == "virus4")
        {
            GameManager.instance.energy -= 4;
        }

        if (other.tag == "rock")
        {
            if (GameManager.instance.hasHope)
            {
                Debug.Log("break rock");
            }
            else
            {
                Debug.Log("can't break rock");
            }
        }

        if (other.tag == "hope")
        {
            GameManager.instance.hasHope = true;
        }
    }
}
