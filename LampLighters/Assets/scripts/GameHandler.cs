
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public static GameHandler Instance { get; private set; }

    public int LitCount { get; private set; }
    public int TotalLamps { get; private set; }

    private LampToggle[] lamps;
    private PlayerController player;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
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
// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class GameHandler : MonoBehaviour
// {
//     // lets other scripts do GameHandler.Instance
//     public static GameHandler Instance { get; private set; }

//     // how many lamps are currently ON in this level
//     public int LitCount { get; private set; }

//     // how many lamps exist in this level total
//     public int TotalLamps { get; private set; }

//     // cached list of all LampToggle objects in the current scene
//     private LampToggle[] lamps;

//     void Awake()
//     {
//         // singleton: only keep one GameHandler across scenes
//         if (Instance != null && Instance != this) { Destroy(gameObject); return; }
//         Instance = this;
//         DontDestroyOnLoad(gameObject);
//     }

//     void OnEnable()
//     {
//         // run code whenever a new scene loads
//         SceneManager.sceneLoaded += OnSceneLoaded;
//     }

//     void OnDisable()
//     {
//         // stop listening + clean up event subscriptions
//         SceneManager.sceneLoaded -= OnSceneLoaded;
//         Unsubscribe();
//     }

//     private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
//     {
//         // new level loaded -> re-find lamps for THIS scene
//         RefreshLamps();
//     }

//     private void RefreshLamps()
//     {
//         // remove old subscriptions (from previous level)
//         Unsubscribe();

//         // find every lamp in the scene (even if disabled)
//         lamps = FindObjectsOfType<LampToggle>(true);

//         // reset counts for this level
//         TotalLamps = lamps.Length;
//         LitCount = 0;

//         // count lamps + subscribe so we get notified when they toggle
//         foreach (var l in lamps)
//         {
//             if (l == null) continue;

//             if (l.IsOn) LitCount++;
//             l.OnStateChanged += HandleLampChanged;
//         }

//         // check win immediately (in case the level starts with some on)
//         CheckWin();
//     }

//     private void Unsubscribe()
//     {
//         // if we never found lamps yet, nothing to do
//         if (lamps == null) return;

//         // detach from each lamp event so we don’t double-count
//         foreach (var l in lamps)
//             if (l != null) l.OnStateChanged -= HandleLampChanged;
//     }

//     private void HandleLampChanged(bool isOn)
//     {
//         // update count based on whether a lamp turned on or off
//         LitCount += isOn ? 1 : -1;

//         // after every change, see if we won
//         CheckWin();
//     }

//     private PlayerMovement player;

//     private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
//     {
//         RefreshLamps();
//         player = FindObjectOfType<PlayerMovement>(true);
//     }

//     private void CheckWin()
//     {
//         bool lost = false;
//             if (player != null) player.Die(false);
//     }
//     // private void CheckWin()
//     // {
//     //     // win when every lamp in the level is ON
//     //     if (TotalLamps > 0 && LitCount >= TotalLamps)
//     //     {
//     //         // go to win scene
//     //         SceneManager.LoadScene("EndWin");
//     //     }


//     // }
// }


