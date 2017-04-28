using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCtrl : MonoBehaviour {

	//item type
	public eItemType itemType;

	//transform
	Transform trans;

	//wait for checkDestroy
	WaitForSeconds waitForCheckY = new WaitForSeconds(0.1f);

	void OnEnable() {
		trans = transform;
		Init();
		StartCoroutine(RunCheckYForDestroy());
	}

	protected virtual void Init() { }

	IEnumerator RunCheckYForDestroy() {
		while(true) {
			if(trans.position.y < -100) {
				DestroyItem();
			}
			yield return waitForCheckY;
		}
	}

	public void DestroyItem() {
		trans.position = Vector3.one * 100f;
		Init();
		StopCoroutine(RunCheckYForDestroy());
		ObjectPool.Release(gameObject);
	}
}
