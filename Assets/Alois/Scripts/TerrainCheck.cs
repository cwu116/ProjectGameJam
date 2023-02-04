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
            GameManager.instance.steps++;
        }

        if (other.tag == "water2")
        {
            GameManager.instance.energy += 2;
            GameManager.instance.steps++;
        }

        if (other.tag == "water3")
        {
            GameManager.instance.energy += 3;
            GameManager.instance.steps++;
        }

        if (other.tag == "water4")
        {
            GameManager.instance.energy += 4;
            GameManager.instance.steps++;
        }

        if (other.tag == "virus2")
        {
            GameManager.instance.energy -= 2;
            GameManager.instance.steps++;
        }

        if (other.tag == "virus3")
        {
            GameManager.instance.energy -= 3;
            GameManager.instance.steps++;
        }

        if (other.tag == "virus4")
        {
            GameManager.instance.energy -= 4;
            GameManager.instance.steps++;
        }

        if (other.tag == "rock")
        {
            if (GameManager.instance.hasHope)
            {
                Debug.Log("break rock");
                GameManager.instance.steps++;
            }
            else
            {
                Debug.Log("can't break rock");
            }
        }

        if (other.tag == "hope")
        {
            GameManager.instance.hasHope = true;
            GameManager.instance.steps++;
            GameManager.instance.hopeStartStep = GameManager.instance.steps;
        }
    }
}
