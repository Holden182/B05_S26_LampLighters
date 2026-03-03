using TMPro;
using UnityEngine;

public class LightsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;

    void Awake()
    {
        if (label == null) label = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (GameHandler.Instance == null) return;
        label.text = "Lights: " + GameHandler.Instance.LitCount;
    }
}