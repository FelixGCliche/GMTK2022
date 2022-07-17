using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private bool centered;
    [SerializeField] private RectTransform labelContainerTransform;
    [SerializeField] private RectTransform maskRectTransform;
    [SerializeField] private RectTransform iconContainerTransform;
    [SerializeField] private UnityEvent onClick;
    
    private float labelDeltaX;
    private float iconDeltaX;
    private Vector2 targetMaskSizeDelta;
    private bool Centered => centered && iconContainerTransform != null;
    

    private void Awake()
    {
        labelDeltaX = labelContainerTransform.localPosition.x;
        if (maskRectTransform)
        {
            targetMaskSizeDelta = labelContainerTransform.sizeDelta;
            iconDeltaX = -(targetMaskSizeDelta.x / 2) + targetMaskSizeDelta.y/2;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Centered)
        {
            iconContainerTransform.DOLocalMoveX(iconDeltaX, 0.33f).SetUpdate(true);
            maskRectTransform.DOSizeDelta(targetMaskSizeDelta, 0.33f).SetUpdate(true);
        }
        
        labelContainerTransform.DOLocalMoveX(0, 0.33f).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Close();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Close();
        onClick?.Invoke();
    }

    private void Close()
    {
        if (Centered)
        {
            iconContainerTransform.DOLocalMoveX(0, 0.33f).SetUpdate(true);
            maskRectTransform.DOSizeDelta(new Vector2(0,targetMaskSizeDelta.y), 0.33f).SetUpdate(true);
        }
        
        labelContainerTransform.DOLocalMoveX(labelDeltaX, 0.33f).SetUpdate(true);
    }
}
