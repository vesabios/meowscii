using UnityEngine;
using System.Collections;

[System.Serializable]
public class PWorldObject : PD {

    [HideInInspector]
	public System.Guid zoneID = System.Guid.Empty;

    [HideInInspector]
	public SerializableVector3 location = new SerializableVector3 (0, 0, 0);

    [Space(10)]
    public string shortDisplayName = "description";

    [TextArea(3, 10), Space(10)]
    public string description = "item";


    [HideInInspector]
    public SerializableVector3 dims = new SerializableVector3(1, 1, 1);


    public virtual void Draw() { }

	protected bool IsVectorDiagonal(Vector2 v) {
		return (Mathf.Abs (v.x) + Mathf.Abs (v.y) > 1);
	}

	protected virtual bool IsMotile() {
		return false;
	}

	public virtual void Tick(int actionPoints) {


	}

}
