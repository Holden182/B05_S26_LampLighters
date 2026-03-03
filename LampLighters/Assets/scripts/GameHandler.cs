using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public static GameHandler Instance { get; private set; }

    public int LitCount { get; private set; }
    public int TotalLamps { get; private set; }

    private LampToggle[] lamps;
    private PlayerController player;

    [Header("Random blackout")]
    [SerializeField] private float blackoutInterval = 60f;
    [SerializeField] private bool enableRandomBlackouts = true;

    private Coroutine blackoutRoutine;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (enableRandomBlackouts)
            blackoutRoutine = StartCoroutine(RandomBlackoutLoop());
    }

    private IEnumerator RandomBlackoutLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(blackoutInterval);

            // If we haven't cached lamps yet (or scene has none), skip.
            if (lamps == null || lamps.Length == 0) continue;

            // Build a list of ON lamps.
            List<LampToggle> onLamps = new List<LampToggle>();
            foreach (var l in lamps)
                if (l != null && l.IsOn) onLamps.Add(l);

            // Nothing currently ON.
            if (onLamps.Count == 0) continue;

            // Pick one at random and turn it OFF.
            int idx = Random.Range(0, onLamps.Count);
            onLamps[idx].SetStatePublic(false);

            // Debug.Log($"[GameHandler] Blackout! Turned off {onLamps[idx].name}");
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Unsubscribe();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RefreshLamps();
        player = FindObjectOfType<PlayerController>(true); // cache player for this scene
    }

    private void RefreshLamps()
    {
        Unsubscribe();

        lamps = FindObjectsOfType<LampToggle>(true);
        TotalLamps = lamps.Length;
        LitCount = 0;

        foreach (var l in lamps)
        {
            if (l == null) continue;

            if (l.IsOn) LitCount++;
            l.OnStateChanged += HandleLampChanged;
        }

        CheckWin();
    }

    private void Unsubscribe()
    {
        if (lamps == null) return;

        foreach (var l in lamps)
            if (l != null) l.OnStateChanged -= HandleLampChanged;
    }

    private void HandleLampChanged(bool isOn)
    {
        LitCount += isOn ? 1 : -1;
        CheckWin();
    }

    private void CheckWin()
    {
        // only win if there are lamps AND all are on
        if (TotalLamps > 0 && LitCount >= TotalLamps)
        {
            if (player != null) player.Die(false); 
            else Debug.LogWarning("[GameHandler] Win, but no PlayerMovement found.");
        }
    }
}
