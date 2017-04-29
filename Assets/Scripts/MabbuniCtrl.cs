using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MabbuniCtrl : ItemCtrl {

	//animator
	public Animator animator;

	//eating
	public bool isEating = false;

	new void Awake() {
		itemType = eItemType.ETC;
		base.Awake();	}

	protected override void Init() {
		animator = GetComponent<Animator>();
		isEating = false;
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.CompareTag("Item")) {
			if (!isEating) {
				ItemCtrl item = collision.collider.GetComponent<ItemCtrl>();
				switch (item.itemType) {
					case eItemType.Meat:
						isEating = true;
						item.DestroyItem();
						animator.SetTrigger("EatMeat");
						break;
				}
			}
		}
	}
}
