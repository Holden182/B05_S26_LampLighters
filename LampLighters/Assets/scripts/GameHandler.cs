using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    // lets other scripts do GameHandler.Instance
    public static GameHandler Instance { get; private set; }

    // how many lamps are currently ON in this level
    public int LitCount { get; private set; }

    // how many lamps exist in this level total
    public int TotalLamps { get; private set; }

    // cached list of all LampToggle objects in the current scene
    private LampToggle[] lamps;

    void Awake()
    {
        // singleton: only keep one GameHandler across scenes
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        // run code whenever a new scene loads
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // stop listening + clean up event subscriptions
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Unsubscribe();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // new level loaded -> re-find lamps for THIS scene
        RefreshLamps();
    }

    private void RefreshLamps()
    {
        // remove old subscriptions (from previous level)
        Unsubscribe();

        // find every lamp in the scene (even if disabled)
        lamps = FindObjectsOfType<LampToggle>(true);

        // reset counts for this level
        TotalLamps = lamps.Length;
        LitCount = 0;

        // count lamps + subscribe so we get notified when they toggle
        foreach (var l in lamps)
        {
            if (l == null) continue;

            if (l.IsOn) LitCount++;
            l.OnStateChanged += HandleLampChanged;
        }

        // check win immediately (in case the level starts with some on)
        CheckWin();
    }

    private void Unsubscribe()
    {
        // if we never found lamps yet, nothing to do
        if (lamps == null) return;

        // detach from each lamp event so we don’t double-count
        foreach (var l in lamps)
            if (l != null) l.OnStateChanged -= HandleLampChanged;
    }

    private void HandleLampChanged(bool isOn)
    {
        // update count based on whether a lamp turned on or off
        LitCount += isOn ? 1 : -1;

        // after every change, see if we won
        CheckWin();
    }

    private void CheckWin()
    {
        // win when every lamp in the level is ON
        if (TotalLamps > 0 && LitCount >= TotalLamps)
        {
            // go to win scene
            SceneManager.LoadScene("EndWin");
        }
    }
}