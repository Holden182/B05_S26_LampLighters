using UnityEngine;
using System.Collections;

public class LampManager : MonoBehaviour
{
    public LampToggle[] startOn;
    public LampToggle[] startOff;

    private IEnumerator Start()
{
    // I wait 1 frame so all the LampToggle scripts finish their own Start() first
    // (so my overrides don’t get overwritten)
    yield return null;

    // force these lamps to start ON
    foreach (var l in startOn)
        if (l != null) l.SetStatePublic(true);

    // I force these lamps to start OFF
    foreach (var l in startOff)
        if (l != null) l.SetStatePublic(false);
}
}