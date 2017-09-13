using UnityEngine;
using System;
using System.Runtime.Serialization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class PD : ScriptableObject {

	public System.Guid guid;
   
    public PD() {
		guid = System.Guid.NewGuid();
	}

    public void PrepareForWrite() {
        ISerializationRefs();
    }

    protected virtual IEnumerator ISerializationRefs() {
        yield return null;
    }


    public virtual void Init() {

	}

    public virtual void LoadExternalData() {

    }

    public virtual void Prepare() { }


 

}



[Serializable]
public class PX : ISerializable { 

	public PD pd;

	public PX() {}

	public void GetObjectData(SerializationInfo info, StreamingContext context) {
		if (pd == null) return;

		Debug.Log("--------------------");
		pd.Prepare();

		info.AddValue("ScriptableType", pd.GetType().AssemblyQualifiedName, typeof(string));
		info.AddValue("ScriptableName", pd.name, typeof(string));

		foreach (FieldInfo field in pd.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.Instance)) {


			if (
				(field.FieldType != typeof(GameObject)) &&
				(field.FieldType != typeof(Sprite)) &&
				(field.FieldType != typeof(List<PItem>)) &&
				(!field.IsNotSerialized)
			)
			{
				Debug.Log(field.Name + ", " + field.GetValue(pd) + ", " + field.FieldType);

				info.AddValue(field.Name, field.GetValue(pd), field.FieldType);
			}
		}

	}

	public PX(SerializationInfo info, StreamingContext context) {
		Type type = Type.GetType((string)info.GetValue("ScriptableType", typeof(string)));
		if (type == null)
			return;

		pd = (PD)(ScriptableObject)ScriptableObject.CreateInstance(type);
		if (pd == null)
			return;

		pd.name = (string)info.GetValue("ScriptableName", typeof(string));

		foreach (FieldInfo field in pd.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.Instance)) {

			if (
				(field.FieldType != typeof(GameObject)) &&
				(field.FieldType != typeof(Sprite)) &&
				(field.FieldType != typeof(List<PItem>)) &&
				(!field.IsNotSerialized) 
			){


				field.SetValue(pd, info.GetValue(field.Name, field.FieldType));
			}
		}
	}
}
