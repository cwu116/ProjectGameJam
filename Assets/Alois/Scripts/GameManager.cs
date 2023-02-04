using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int energy;
    public bool hasHope;

    public int steps;
    public int hopeStartStep;

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
        hopeStartStep = -1;
    }

    // Update is called once per frame
    void Update()
    {
        HopeLastTime();

        if(energy == 0)
        {

        }
    }

    void HopeLastTime()
    {
        if (hopeStartStep > 0 && hopeStartStep < steps && hasHope)
        {
            hopeStartStep++;
        }
        if(hopeStartStep >= steps)
        {
            hasHope = false;
        }
    }
}
