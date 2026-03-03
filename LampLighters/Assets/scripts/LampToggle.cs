using System;
using UnityEngine;

public class LampToggle : MonoBehaviour
{
    [SerializeField] private Light lampLight;
    [SerializeField] private GameObject glowObject;
    [SerializeField] private bool startOn = false;

    public bool IsOn => lampLight != null && lampLight.enabled;

    public event Action<bool> OnStateChanged; // passes new state (on/off)

    void Awake()
    {
        // Find the light if not assigned
        if (lampLight == null)
            lampLight = GetComponentInChildren<Light>();

        // Apply the starting ON/OFF state as early as possible
        // so GameHandler sees the correct state when the scene loads.
        ApplyState(startOn);
    }

    public void Toggle()
    {
        SetState(!IsOn);
    }

    public void SetStatePublic(bool on) => SetState(on);

    // Used only for first-time initialization (no events)
    private void ApplyState(bool on)
    {
        if (lampLight != null) lampLight.enabled = on;
        if (glowObject != null) glowObject.SetActive(on);
    }

    // Used for runtime changes (fires events)
    private void SetState(bool on)
    {
        bool wasOn = IsOn;

        if (lampLight != null) lampLight.enabled = on;
        if (glowObject != null) glowObject.SetActive(on);

        // Only notify if it actually changed
        if (wasOn != on)
            OnStateChanged?.Invoke(on);
    }
}


// using System;
// using UnityEngine;

// public class LampToggle : MonoBehaviour
// {
//     [SerializeField] private Light lampLight;
//     [SerializeField] private GameObject glowObject;
//     [SerializeField] private bool startOn = false;

//     public bool IsOn => lampLight != null && lampLight.enabled;

//     public event Action<bool> OnStateChanged; // passes new state (on/off)

//     void Awake()
//     {
//         if (lampLight == null) lampLight = GetComponentInChildren<Light>();
//     }

//     void Start()
//     {
//         SetState(startOn);
//     }

//     public void Toggle()
//     {
//         SetState(!IsOn);
//     }

//     public void SetStatePublic(bool on) => SetState(on);

//     private void SetState(bool on)
//     {
//         bool wasOn = IsOn;

//         if (lampLight != null) lampLight.enabled = on;
//         if (glowObject != null) glowObject.SetActive(on);

//         if (wasOn != on)
//             OnStateChanged?.Invoke(on);
//     }
// }

