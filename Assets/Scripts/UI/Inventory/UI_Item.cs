using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class UI_Item : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text quantityTxt;
    [SerializeField] private Image selected;

    public event Action<UI_Item> OnItemClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, OnRightMouseBtnClick;

    private bool empty = true;

    public void Awake()
    {
        resetData();
        Deselect();
    }

    public void resetData()
    {
        empty = true;
        this.itemImage.gameObject.SetActive(false);
    }

    public void Deselect()
    {
        selected.enabled = false;
    }

    public void setData(Sprite sprite, int quantity, bool stackable)
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;

        if (stackable)
        {            
            this.quantityTxt.text = quantity + ""; 
        }
        else
        {
            this.quantityTxt.text = "";
        }

        empty = false;
    }

    public void Select()
    {
        selected.enabled = true;
    }

    public void OnBeginDrag(BaseEventData data)
    {
        if (empty) {
            return;
        }
        OnItemBeginDrag?.Invoke(this);
    }

    public void OnDrop(BaseEventData data)
    {
        OnItemDroppedOn?.Invoke(this);
    }

    public void OnEndDrag(BaseEventData data)
    {
        OnItemEndDrag?.Invoke(this);
    }

    public void OnPointerClick(BaseEventData data)
    {
        // if (empty) {
        //     return;
        // }
        PointerEventData pointerData = (PointerEventData)data;
    
        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseBtnClick?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }
    }
}
