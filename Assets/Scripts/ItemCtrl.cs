using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCtrl : MonoBehaviour {

	//item type
	public eItemType itemType;

	//transform
	Transform trans;

	void OnEnable() {
		trans = transform;
		Init();
	}

	protected virtual void Init() { }

	public void DestroyItem() {
		trans.position = Vector3.one * 100f;
		Init();
		ObjectPool.Release(gameObject);
	}
}
