using UnityEngine;
using System.Collections;

[System.Serializable]
public class PZone : PD {

    [Space(10)]
    public string shortDisplayName;

    [TextArea(3, 10), Space(10)]
    public string description;

    public virtual void LoadAtlas() { }
}


