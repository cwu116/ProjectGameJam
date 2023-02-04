using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip pickUpClip;
    [SerializeField] private AudioClip dropClip;
    [SerializeField] private Canvas canvas;
    private RectTransform rectTrans;
    private CanvasGroup canvasGroup;
    
    private Vector3 startPos;
    public bool correctPosition = false;
    
    private void Start()
    {
        rectTrans = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        startPos = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.35f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTrans.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        if (!correctPosition)
        {
            transform.position = startPos;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Hex")
        {
            correctPosition = true;
            Debug.Log(correctPosition);
        }
        else
            Debug.Log(correctPosition);
    }
}
