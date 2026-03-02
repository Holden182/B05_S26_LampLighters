using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// I use this to let the player interact with things they’re looking at.
public class PlayerInteract : MonoBehaviour
{
    // The camera player raycast from (usually my player cam)
    public Camera cam;

    // How far player can interact
    public float interactRange = 4f;

    // Only interact with certain layers (like Lamps)
    public LayerMask interactMask;

   //!! calls on the player animation script
    private PlayerAnimation anim; 

    void Start()
    {
        // If forgoteb to assign the camera, use MainCamera
        if (cam == null) cam = Camera.main;
    }

    void Update()
    {
        // Press E to interact
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Shoot a ray forward from my camera
            if (Physics.Raycast(cam.transform.position,
                                cam.transform.forward,
                                out RaycastHit hit,
                                interactRange,
                                interactMask))
            {
                // Check if what is hit is a lamp
                var lamp = hit.collider.GetComponentInParent<LampToggle>();

                // If it is, toggle it
                if (lamp != null)
                    lamp.Toggle();
                    //!! pulls the LightLamp animation
                    anim.LightLamp();
            }
        }
    }
}