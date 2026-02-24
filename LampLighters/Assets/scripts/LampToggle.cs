using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

// put this on a lamp so it can turn it on/off.
public class LampToggle : MonoBehaviour
{
    // The actual Light component needed to toggle
    [SerializeField] private Light lampLight;

    // Optional glow object (like emissive bulb mesh)
    [SerializeField] private GameObject glowObject;

    void Awake()
    {
        // If not assigned a light in the Inspector, try to find one
        if (lampLight == null)
            lampLight = GetComponentInChildren<Light>();
    }

    // Called when the player interacts with the lamp
    public void Toggle()
    {
        // Flip the light on/off
        if (lampLight != null)
            lampLight.enabled = !lampLight.enabled;

        // Match the glow to the light state
        if (glowObject != null)
            glowObject.SetActive(lampLight != null && lampLight.enabled);
    }
}