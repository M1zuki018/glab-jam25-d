using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragShip : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;
    private bool isDragging = false;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void OnMouseDown()
    {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        offset = transform.position - mousePos;
        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (!isDragging) return;
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        transform.position = mousePos + offset;
    }

    /*void OnMouseUp()
    {
        isDragging = false;

        // シーン内のすべてのJudgeを取得して、それぞれに通知
        Judge[] judges = FindObjectsOfType<Judge>();
        foreach (var judge in judges)
        {
            judge.OnShipPlaced(gameObject);
        }
    }*/
}
