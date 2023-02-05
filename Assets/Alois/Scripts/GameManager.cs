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

    [SerializeField] GameObject success;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<EnergyManager>().InitEnergyCount();

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
            Debug.Log("game over");
        }
    }

    void HopeLastTime()
    {
        if (hopeStartStep > 0 && hopeStartStep + 3 > steps && hasHope)
        {
            return;
        }
        if(hopeStartStep + 3 <= steps)
        {
            hasHope = false;
        }
    }

    public void ArriveLevel(int index)
    {
        if(index == 3)
        {
            success.SetActive(true);
        }
    }
}
