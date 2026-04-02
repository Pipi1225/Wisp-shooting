using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Logic : Interactable_Object
{
    [Header("Controller")]
    [SerializeField] private Rigidbody2D rig;
    [SerializeField] private Animator anim;
    [SerializeField] private CameraMovement mainCamera;
    [SerializeField] private UI_Shop shop;
    [SerializeField] private Player_Controller player;

    [Header("Stats")]
    [SerializeField] private Inventory.Model.InventorySO shopData;

    // Start is called before the first frame update
    void Awake()
    {
        //inside = false;
        interact = false;

        mainCamera = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
        shop = GameObject.Find("Shop").GetComponent<UI_Shop>();
        player = GameObject.Find("Player").GetComponent<Player_Controller>();
        key = (KeyCode) System.Enum.Parse(typeof(KeyCode), keyInteract);
    }

    public override string getKeyInteract()
    {
        return keyInteract;
    }

    public override KeyCode getKey()
    {
        return key;
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if (inside && !interact && !player.getInActionStatus() && Input.GetKeyDown(key))
    //     {
    //         interact = true;

    //         mainCamera.chatToNPC(transform.position);
    //         shop.Show(shopData);

    //         button.press();
    //     }

    //     if ((Input.GetKeyDown(KeyCode.Escape) && interact) || ((!inside && interact)))
    //     {
    //         interact = false;

    //         if (inside)
    //         {
    //             button.resetPress();
    //             button.popUp(keyInteract);
    //         }

    //         mainCamera.stopEveryAction();
    //         shop.Hide();
    //     }
    // }

    public override void startInteract()
    {
        if (!interact)
        {
            interact = true;

            mainCamera.chatToNPC(transform.position);
            shop.Show(shopData);
        }
    }

    public override void stopInteract()
    {
        if (interact)
        {
            interact = false;

            mainCamera.stopEveryAction();
            shop.Hide();
        }
    }

    public void exitInteract()
    {
        mainCamera.stopEveryAction();
        interact = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // inside = true;
            button = collision.transform.Find("Button").GetComponent<Popup_Button>();
            button.addInteratable(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // inside = false;
            button = collision.transform.Find("Button").GetComponent<Popup_Button>();
            button.removeInteratable(this);
        }
    }
}
