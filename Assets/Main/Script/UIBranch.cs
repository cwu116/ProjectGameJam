
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class UIBranch : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public Vector3 pos = new Vector2();
    [HideInInspector] public Vector3 originPos;
    public BranchManager branchmanager;

    bool isMouseUp;
    bool isInTrigger;

    [HideInInspector]public int branchUIindex;
    public int branchIndex;

    GameObject audioManager;
    AudioSource[] m_ArrayMusic;

    void Start()
    {
        originPos = this.transform.position;
        branchmanager = GameObject.Find("BranchManager").GetComponent<BranchManager>();

        audioManager = GameObject.Find("Audio Manager");
        m_ArrayMusic = audioManager.GetComponents<AudioSource>();
    }

    void Update()
    {
        originPos = transform.parent.position;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {

        RectTransformUtility.ScreenPointToWorldPointInRectangle(GetComponent<RectTransform>(), eventData.position,
            Camera.main, out pos);

        var position = Camera.main.WorldToViewportPoint(pos);

        if (position.x > 0 && position.x < 1 && position.y > 0 && position.y < 1)
            transform.position = pos;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isInTrigger = true;
        if ((collision.CompareTag("mud") ||
            collision.CompareTag("water2") ||
            collision.CompareTag("water3") ||
            collision.CompareTag("water4") ||
            collision.CompareTag("virus2") ||
            collision.CompareTag("virus3") ||
            collision.CompareTag("virus4") ||
            collision.CompareTag("rock") ||
            collision.CompareTag("hope") )
            && isMouseUp )
        {
            if (collision.CompareTag("rock") && GameManager.instance.hasHope != true)
            {
                transform.position = originPos;
                return;
            }
            HexCell cell = collision.GetComponent<HexCell>();
            if (cell.canPutBranchIn)
            {
               
                transform.position = originPos;
                GameObject branch = Instantiate<GameObject>(branchmanager.branchList[branchIndex]);
                TerrainCheck(collision);
                branch.transform.SetParent(branchmanager.transform);
                branch.transform.position = collision.transform.position;
                branchmanager.CreateNewBranch(branchUIindex);
                GameObject.Destroy(this.gameObject);
            }
            else
            {
                transform.position = originPos;
            }
        }
        else if(isMouseUp)
        {
            transform.position = originPos;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInTrigger = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isInTrigger = true;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        isMouseUp = false;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        
        isMouseUp = true;  
        if(isInTrigger == false)
            transform.position = originPos;
    }


    void TerrainCheck(Collider2D other)
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

