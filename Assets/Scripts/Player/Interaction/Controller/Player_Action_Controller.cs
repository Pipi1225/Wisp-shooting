using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Action_Controller : MonoBehaviour
{
    [Header("Offset for actions:")]
    [SerializeField] private Vector3 inventoryOffset;

    [Header("Controller:")]
    [SerializeField] private CameraMovement mainCamera;
    [SerializeField] private UI_Inventory inventoryUI;

    private void Awake()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
    }

    public void stopCameraMovement()
    {
        mainCamera.stopEveryAction();
    }

    public void openInventory()
    {
        inventoryUI.Show();
        mainCamera.action(inventoryOffset);
    }

    public void closeInventory()
    {
        inventoryUI.Hide();
        mainCamera.stopEveryAction();
    }
}
