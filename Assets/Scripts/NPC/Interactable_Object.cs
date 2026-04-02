using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable_Object : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected string keyInteract;
    [SerializeField] protected KeyCode key;

    [Header("Status")]
    [SerializeField] protected bool interact;
    [SerializeField] protected Popup_Button button;

    public abstract KeyCode getKey();
    public abstract string getKeyInteract();
    public abstract void startInteract();
    public abstract void stopInteract();
}
