using UnityEngine;
using System.Collections;

public class PStaticZone : PZone {

    public override void LoadAtlas()
    {
        Debug.Log("PStaticZone::LoadAtlas -> " + name);

        Landscape.LoadStaticZone(name);

    }

}
