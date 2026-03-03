using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using UnityEngine;

public class LampToggle : MonoBehaviour
{
   
    [SerializeField] private Light lampLight;
    [SerializeField] private GameObject glowObject;

    // intialize all lamps off
    [SerializeField] private bool startOn = false;
    //set which lamps are on or off
    public void SetStatePublic(bool on) => SetState(on);
    

    void Awake()
    {
        if (lampLight == null)
            lampLight = GetComponentInChildren<Light>();
    }

    void Start()
    {
        SetState(startOn);
    }

    public void Toggle()
    {
        SetState(!(lampLight != null && lampLight.enabled));
    }

    private void SetState(bool on)
    {
        if (lampLight != null)
            lampLight.enabled = on;

        if (glowObject != null)
            glowObject.SetActive(on);
    }
}