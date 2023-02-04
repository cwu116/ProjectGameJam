using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int energy;
    public bool hasHope;
    public int steps;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        energy = 5;
        steps = 0;
        hasHope = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
