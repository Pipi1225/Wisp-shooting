using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Controller:")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform target;

    [Header("Stats:")]
    [SerializeField] private Vector3 targetPos;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 chatOffset;
    [SerializeField] private float smoothTime;
    [SerializeField] private float normal;
    [SerializeField] private float zoom;

    private Vector3 velocity = Vector3.zero;
    private float zoom_velocity = 0;

    [Header("Status:")]
    [SerializeField] private bool busy;

    void Awake()
    {
        mainCamera = GetComponent<Camera>();
        target = GameObject.Find("Player").GetComponent<Transform>();
        busy = false;
    }

    void LateUpdate()
    {
        if (busy == false)
        {
            targetPos = target.position - offset;
            mainCamera.orthographicSize = Mathf.SmoothDamp(mainCamera.orthographicSize, normal, ref zoom_velocity, smoothTime);
        }
        else
        {
            mainCamera.orthographicSize = Mathf.SmoothDamp(mainCamera.orthographicSize, zoom, ref zoom_velocity, smoothTime);
        }

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }

    public void chatToNPC(Vector3 NPC_pos)
    {
        busy = true;
        //targetPos = ((target.position + NPC_pos)) * 0.5f - chatOffset;
        targetPos = NPC_pos - chatOffset;
    }

    public void action(Vector3 actionOffset) 
    {
        busy = true;
        targetPos = target.position - actionOffset;
    }

    public void stopEveryAction()
    {
        busy = false;
    }

    public bool getBusyStatus()
    {
        return busy;
    }
}
