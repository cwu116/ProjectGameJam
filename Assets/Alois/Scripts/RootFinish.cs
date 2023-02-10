using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RootFinish : MonoBehaviour
{
    public Canvas canvas;
    public bool canPutOnRock;
    public Button button;
    public List<RootColliderState> colliderstateList;

    [HideInInspector]public GameObject branchUI;
    [HideInInspector]public int branchIndex;
    [HideInInspector] public GameObject rock;
    [HideInInspector] public string rockTag;

    AudioSource[] m_ArrayMusic;
    GameObject audioManager;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(CloseCanvas);
        audioManager = GameObject.Find("Audio Manager");
        m_ArrayMusic = audioManager.GetComponents<AudioSource>();
    }

    
    public void CloseCanvas()
    {
        canvas.gameObject.SetActive(false);
        CanPutOnRock();
        BranchManager.instance.CreateNewBranch(branchIndex);
    }

    public void CloseCanvas1()
    {
        canvas.gameObject.SetActive(false);
        CanPutOnRock();
    }


    void CanPutOnRock()
    {
        canPutOnRock = true;
        for(int i = 0; i< colliderstateList.Count; i++)
        {
            colliderstateList[i].canPutOnRock = true;
        }

        if(rock)
        TerrainCheck(rock);

        Destroy(branchUI);
        Destroy(rock);
    }

    void TerrainCheck(GameObject other)
    {
        if (other.tag == "mud")
        {
            GameManager.instance.energy--;
            GameManager.instance.steps++;

            m_ArrayMusic[0].Play();
        }

        if (other.tag == "water2")
        {
            GameManager.instance.energy += 2;
            GameManager.instance.steps++;

            m_ArrayMusic[1].Play();
        }

        if (other.tag == "water3")
        {
            GameManager.instance.energy += 3;
            GameManager.instance.steps++;

            m_ArrayMusic[1].Play();
        }

        if (other.tag == "water4")
        {
            GameManager.instance.energy += 4;
            GameManager.instance.steps++;

            m_ArrayMusic[1].Play();
        }

        if (other.tag == "virus2")
        {
            GameManager.instance.energy -= 2;
            GameManager.instance.steps++;

            m_ArrayMusic[2].Play();
        }

        if (other.tag == "virus3")
        {
            GameManager.instance.energy -= 3;
            GameManager.instance.steps++;

            m_ArrayMusic[2].Play();
        }

        if (other.tag == "virus4")
        {
            GameManager.instance.energy -= 4;
            GameManager.instance.steps++;

            m_ArrayMusic[2].Play();
        }

        if (other.tag == "rock")
        {
            if (GameManager.instance.hasHope)
            {
                Debug.Log("break rock");
                GameManager.instance.steps++;

                m_ArrayMusic[4].Play();
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

            m_ArrayMusic[3].Play();
        }

        Debug.Log("Current " + GameManager.instance.energy);
    }
}

