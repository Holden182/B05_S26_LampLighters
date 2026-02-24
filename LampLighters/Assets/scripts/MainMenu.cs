using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1); // or LoadScene("GameSceneName")
    }
    public void QuitGame()
        {
        Application.Quit();
        }
}









// using System.Collections.Generic;
// using UnityEngine.InputSystem;
// using System.Collections;
// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;
// using UnityEngine.SceneManagement;


// public class PlayerController : MonoBehaviour

// {
//     public float speed = 10f;
//     private Rigidbody rb;
//     private float movementX;
//     private float movementY;
//     private bool isDead = false;
//     public Transform orientation;
//     //higher = snappier, lower = more floaty
//     public float accel = 15f;
//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {
//         rb = GetComponent<Rigidbody>();
        
//         isDead = false;
//         rb.isKinematic = false;
//     }
//     void OnMove(InputValue movementValue)
//     {
//         Debug.Log(movementValue.Get<Vector2>());
//         Vector2 movementVector = movementValue.Get<Vector2>();
//         movementX = movementVector.x;
//         movementY = movementVector.y;
//     }

//     // //ground check
//     public float playerHeight;
//     public LayerMask whatIsGround;
//     bool grounded;
//     public float groundAccel = 20f;
//     public float airAccel = 5f;

//     //jumping
//     public float jumpForce;
//     public float jumpCooldown;


//     // Runs once per rendered frame (good for non-physics checks like ground detection)
// void Update()
// {
//     // Cast a ray straight down from the player's position to see if we're standing on "ground"
//     grounded = Physics.Raycast(
//         transform.position,             // ray start: player's current position
//         Vector3.down,                   // ray direction: downward
//         playerHeight * 0.5f + 0.2f,      // ray length: half the player's height + a small buffer
//         whatIsGround                    // only count hits on layers marked as ground
//     );
// }

//     // Runs on the physics timestep (good for Rigidbody movement)
//     void FixedUpdate()
//     {
//         // If we already died, don't allow movement anymore
//         if (isDead) return;

//         // Build a movement direction based on camera-facing orientation:
//         // forward/back uses movementY (W/S), left/right uses movementX (A/D)
//         Vector3 moveDir = orientation.forward * movementY + orientation.right * movementX;

//         // Ensure we don't accidentally add any vertical movement from orientation
//         moveDir.y = 0f;

//         // Convert the direction into speed
//         // normalized makes diagonal movement the same speed as straight movement
//         Vector3 target = moveDir.normalized * speed;

//         // Read the current Rigidbody velocity (includes vertical velocity from gravity/jumps)
//         Vector3 vel = rb.velocity;

//         // Extract only the horizontal part of current velocity (ignore Y)
//         Vector3 horiz = new Vector3(vel.x, 0f, vel.z);

//         // Choose acceleration based on whether we're grounded:
//         // grounded -> snappy control, air -> floaty control
//         float accel;
//         if (grounded)
//             accel = groundAccel;
//         else
//             accel = airAccel;

//         // Smoothly move current horizontal velocity toward the target horizontal velocity (drag)
//         Vector3 newHoriz = Vector3.Lerp(horiz, target, accel * Time.fixedDeltaTime);

//         // Apply the new horizontal velocity, but preserve the current vertical velocity (vel.y)
//         rb.velocity = new Vector3(newHoriz.x, vel.y, newHoriz.z);
//     }

//     private void OnCollisionEnter(Collision collision)
//     {
//         if (isDead) return;

//         if (collision.gameObject.CompareTag("enemy"))
//         {
//             // Destroy the current object
//             Debug.Log("Enemy detected!");
//             Die();
//             // Update the winText to display "You Lose!"
//         }
//     }
//     private void Die()
//     {
//         isDead = true;
 
//         // stop physics
//         rb.linearVelocity = Vector3.zero;
//         rb.angularVelocity = Vector3.zero;
//         rb.isKinematic = true;

//         // SHOW MOUSE + UNLOCK IT so UI works
//         Cursor.lockState = CursorLockMode.None;
//         Cursor.visible = true;

//         //disable camera look so it stops moving the view
//         var cam = GetComponentInChildren<PlayerCam>();
//         if (cam != null) cam.enabled = false;

//         //disable input messages entirely
//         var input = GetComponent<PlayerInput>();
//         if (input != null) input.enabled = false;

//         GetComponentInChildren<Renderer>().enabled = false;

//         StartCoroutine(ReturnToMenu());
//     }

//     private IEnumerator ReturnToMenu()
//     {
//         yield return new WaitForSeconds(0.5f); // optional delay
//         SceneManager.LoadScene("MainMenu");    // make sure name matches build list
//     }

// }








