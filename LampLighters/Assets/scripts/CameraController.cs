using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = transform.position - player.transform.position;

    }

    // Update is called once per frame
    //void Update order for scripts is not controlled
    //happens after all other updates
    void LateUpdate()
    {
        //camera matches potiotion of game object
        transform.position = player.transform.position + offset;
    }
}
