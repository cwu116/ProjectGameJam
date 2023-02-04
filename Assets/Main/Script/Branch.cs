using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class Branch : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public Vector3 pos = new Vector2();
    [HideInInspector] public Vector3 originPos;
    public BranchManager branchmanager;

    bool isMouseUp;
    bool isInTrigger;

    public int branchindex;

    void Start()
    {
        originPos = this.transform.position;
        branchmanager = GameObject.Find("BranchManager").GetComponent<BranchManager>();
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
        if (collision.CompareTag("Rock") && isMouseUp )
        {
            HexCell cell = collision.GetComponent<HexCell>();
            if (cell.canPutBranchIn)
            {
                this.gameObject.SetActive(false);
                transform.position = originPos;
                GameObject branch = Instantiate<GameObject>(branchmanager.branchlist[branchindex]);
                branch.transform.position = collision.transform.position;
            }
            else
            {
                transform.position = originPos;
            }
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
}
