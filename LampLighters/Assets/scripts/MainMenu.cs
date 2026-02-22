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
