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
    private float  movementX;
    private float movementY;
    private bool isDead = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void OnMove(InputValue movementValue)
    { Debug.Log(movementValue.Get<Vector2>());
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    
    
    void FixedUpdate()
    {
        if (isDead) return; // STOP running after death
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
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

        // optional: stop physics immediately
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        // optional: hide player (so it "dies" visually) without destroying right away
        GetComponentInChildren<Renderer>().enabled = false;

        StartCoroutine(ReturnToMenu());
    }

    private IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(1.0f); // optional delay
        SceneManager.LoadScene("MainMenu");    // make sure name matches build list
    }

}


