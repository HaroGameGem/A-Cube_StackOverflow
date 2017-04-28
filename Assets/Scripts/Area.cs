using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour {

	//singleton
	static Area[] instance = null;
	public static Area[] Instance { get { return instance; } }
	[SerializeField]
	int singletonIndex;

	//number of object in area
	int objectCount;
	int objectCount_ { get { return objectCount; } set { objectCount = value; } }
	public int ObjectCount { get { return objectCount; } }

	void Awake() {
		InitializeSingleton();
	}

	void InitializeSingleton() {
		if (instance == null) {
			instance = new Area[2];
		}
		instance[singletonIndex] = this;
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.CompareTag("Item")) {
			++objectCount_;
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		if (collider.CompareTag("Item")) {
			--objectCount_;
		}
	}
}
