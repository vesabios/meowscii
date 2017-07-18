using UnityEngine;
using System.Collections;

[System.Serializable]
public class PWorldObject : PD {

    [HideInInspector]
    public System.Guid zoneID;

    [HideInInspector]
    public SerializableVector3 location;

    [Space(10)]
    public string shortDisplayName;

    [TextArea(3, 10), Space(10)]
    public string description;


    [HideInInspector]
    public SerializableVector3 dims = new SerializableVector3(1, 1, 1);


    public virtual void Draw() { }

}
