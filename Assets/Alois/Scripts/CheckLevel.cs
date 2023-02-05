using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLevel : MonoBehaviour
{
    public int levelIndex;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Branch"))
        {
            GameManager.instance.ArriveLevel(levelIndex);
            Debug.LogError("arrive leve " + levelIndex);
        }
    }
}
