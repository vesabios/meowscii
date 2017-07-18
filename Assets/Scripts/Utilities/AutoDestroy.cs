using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour {

    public float lifetime = 1.5f;
	void Start () {
        Destroy(gameObject, lifetime);
	}
	

}
