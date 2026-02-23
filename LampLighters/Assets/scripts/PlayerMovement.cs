using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour

{
    public float speed = 10f;
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private bool isDead = false;
    public Transform orientation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        isDead = false;
        rb.isKinematic = false;
    }
    void OnMove(InputValue movementValue)
    {
        Debug.Log(movementValue.Get<Vector2>());
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }


    void FixedUpdate()
    {
        if (isDead) return;

    Vector3 moveDir = orientation.forward * movementY + orientation.right * movementX;
moveDir.y = 0f;

rb.linearVelocity = moveDir.normalized * speed + Vector3.up * rb.linearVelocity.y;

    Debug.Log($"kinematic={rb.isKinematic} vel={rb.linearVelocity} pos={rb.position}");
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isDead) return;

        if (collision.gameObject.CompareTag("enemy"))
        {
            // Destroy the current object
            Debug.Log("Enemy detected!");
            Die();
            // Update the winText to display "You Lose!"
        }
    }
    private void Die()
    {
        isDead = true;

        // stop physics
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        // SHOW MOUSE + UNLOCK IT so UI works
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // optional: disable camera look so it stops moving the view
        var cam = GetComponentInChildren<PlayerCam>();
        if (cam != null) cam.enabled = false;

        // optional: disable input messages entirely
        var input = GetComponent<PlayerInput>();
        if (input != null) input.enabled = false;

        GetComponentInChildren<Renderer>().enabled = false;

        StartCoroutine(ReturnToMenu());
    }

    private IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(1.0f); // optional delay
        SceneManager.LoadScene("MainMenu");    // make sure name matches build list
    }

}










