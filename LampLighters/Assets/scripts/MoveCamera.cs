using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine;
public class MoveCamera: MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
        
    // }
    public Transform cameraPosition;
    // Update is called once per frame
    private void Update()
    {
        transform.position = cameraPosition.position;
    }
}
