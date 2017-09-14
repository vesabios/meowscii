using UnityEngine;
using System.Collections;

[System.Serializable]
public class PZone : PD {

    [Space(10)]
    public string shortDisplayName;

	[Space(10)]
	public TextAsset inkJSONAsset;


    [TextArea(3, 10), Space(10)]
    public string description;

    public virtual void LoadAtlas() { }





}


