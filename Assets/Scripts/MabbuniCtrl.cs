using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MabbuniCtrl : ItemCtrl {

	//animator
	public Animator animator;

	//eating
	public bool isEating = false;

	new void Awake() {
		itemType = eItemType.Mabbuni;
	}

	protected override void Init() {
		animator = GetComponent<Animator>();
		isEating = false;
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.CompareTag("Item")) {
			if (!isEating) {
				Transform target = collision.collider.transform;
				ItemCtrl item = target.GetComponent<ItemCtrl>();
				while(item == null) {
					target = target.parent;
					item = target.GetComponent<ItemCtrl>();
				}
				switch (item.itemType) {
					case eItemType.Animal:
						if((item as AnimalCtrl).meat.activeSelf) {
							isEating = true;
							item.DestroyItem();
							animator.SetTrigger("EatMeat");
						}
						break;
				}
			}
		}
	}
}
