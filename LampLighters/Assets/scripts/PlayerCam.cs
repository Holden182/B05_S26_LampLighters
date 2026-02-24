using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        //just how unity handles roations and inputs
        yRotation += mouseX;
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        xRotation -= mouseY;

        //clamp x rotion to enusre roation cant be more than 90º
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        //rotate along x
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        //rotate along y
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);


    }
}
