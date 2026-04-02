using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Popup_Button : MonoBehaviour
{
    [SerializeField] private Player_Controller player;
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private string button;
    [SerializeField] private bool poped = false;
    [SerializeField] private List<Interactable_Object> interactableStack = new List<Interactable_Object>();
    [SerializeField] private Interactable_Object currentInteractable;

    [SerializeField] private Animator anim;

    private void Awake()
    {
        player = GetComponentInParent<Player_Controller>();
        anim = GetComponent<Animator>();
    }

    public void popUp()
    {
        anim.SetBool("PopUp", true);
        poped = true;
    }

    public void popDown()
    {
        anim.SetBool("PopUp", false);
        poped = false;
    }

    public bool getPopedStatus()
    {
        return poped;
    }

    private void setKey()
    {
        buttonText.text = button;
    }
    
    public void press()
    {
        anim.SetTrigger("Press");
    }

    public void resetPress()
    {
        anim.ResetTrigger("Press");
    }

    public Interactable_Object getCurrentInteractable()
    {
        return currentInteractable;
    }

    public string addInteratable(Interactable_Object interactable)
    {
        interactableStack.Add(interactable);
        button = interactable.getKeyInteract();
        currentInteractable = interactable;
        setKey();
        resetPress();
        popUp();
        return button;
    }

    public void removeInteratable(Interactable_Object interactable)
    {
        player.StopAction();
        interactable.stopInteract();
        interactableStack.Remove(interactable);
        currentInteractable = null;
        
        if (interactableStack.Count > 0)
        {
            currentInteractable = interactableStack[interactableStack.Count - 1];
            button = currentInteractable.getKeyInteract();
            setKey();
        }
        else
        {
            resetPress();
            popDown();
        }
    }
}
