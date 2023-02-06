using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    CameraController cameraController;

    [SerializeField] Camera mainCamara;
    Transform cameraTransform;
    bool cameraUp;

    public int energy;
    public bool hasHope;

    public int steps;
    public int hopeStartStep;

    public static GameManager instance;

    [Header("UI")]
    [SerializeField] GameObject success;
    [SerializeField] GameObject stage1;
    [SerializeField] GameObject stage2;
    [SerializeField] GameObject stage3;
    [SerializeField] GameObject stage4;

    bool stage2On;
    bool stage3On;
    bool stage4On;

    [Header("tree image")]
    [SerializeField] GameObject treeContainer;
    [SerializeField] Sprite tree2;
    [SerializeField] Sprite tree3;
    [SerializeField] Sprite tree4;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<EnergyManager>().InitEnergyCount();

        cameraController = FindObjectOfType<CameraController>();

        energy = 5;
        steps = 0;
        hasHope = false;
        hopeStartStep = -1;

        stage2On = stage3On = stage4On = true;
        cameraUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        HopeLastTime();

        if(energy == 0)
        {
            Debug.Log("game over");
        }

        if (cameraUp)
        {
            if (cameraTransform.position.y < 0)
            {
                cameraTransform.position = Vector3.Lerp(cameraTransform.position, new Vector3(cameraTransform.position.x, 0, cameraTransform.position.z), Time.deltaTime);
                //Debug.Log("camera up");
            }
            if (cameraTransform.position.y > -0.1f)
            {
                success.SetActive(true);
                //Time.timeScale = 0f;
            }
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
        switch (index)
        {
            case 0:
                if (stage2On == true)
                {
                    stage2.SetActive(true);
                    stage2On = false;
                }

                treeContainer.GetComponent<SpriteRenderer>().sprite = tree2;
                break;

            case 1:
                if (stage3On == true)
                {
                    stage3.SetActive(true);
                    stage3On = false;
                }

                treeContainer.GetComponent<SpriteRenderer>().sprite = tree3;
                break;

            case 2:
                if (stage4On == true)
                {
                    stage4.SetActive(true);
                    stage4On = false;
                }

                treeContainer.GetComponent<SpriteRenderer>().sprite = tree4;
                break;

            case 3:
                cameraController.toggle = false;

                cameraTransform = mainCamara.transform;
                cameraUp = true;
                index = -1;

                break;
        }

        if(index < 3 && index >= 0)
        {
            cameraController.yMinValue = -72f * (index + 2);
        }

    }
}
