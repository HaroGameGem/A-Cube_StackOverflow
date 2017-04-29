using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugCtrl : ItemCtrl {

	//collision count
	int collisionCount = 0;

	//wait for move
	WaitForSeconds waitForMove = new WaitForSeconds(2f);

	//wait for burning
	WaitForSeconds waitForBurning = new WaitForSeconds(3f);

	new void Awake() {
		base.Awake();
		itemType = eItemType.Bug;
	}

	protected override void Init() {
		StartCoroutine("RunMove");
		collisionCount = 0;
	}

	IEnumerator RunMove() {
		const float jumpPower = 4f;
		while(true) {
			if(collisionCount <= 0) {
				yield return waitForMove;
			}
			if (Random.Range(0, 2) == 0) {
				rigidbody2D.AddForce(jumpPower * (Vector2.up + Vector2.left + Vector2.up), ForceMode2D.Impulse);
			} else {
				rigidbody2D.AddForce(jumpPower * (Vector2.up + Vector2.right + Vector2.up), ForceMode2D.Impulse);
			}
			yield return waitForMove;
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		++collisionCount;
		if (collision.collider.CompareTag("Item")) {
			switch (collision.collider.GetComponent<ItemCtrl>().itemType) {
				case eItemType.Fire:
					StartCoroutine(BurnFromFire());
					break;
			}
		}
	}

	void OnCollisionExit2D(Collision2D collision) {
		--collisionCount;
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.CompareTag("Item")) {
			ItemCtrl item = collider.GetComponent<ItemCtrl>();
			switch (item.itemType) {
				case eItemType.Wood:
					(item as WoodCtrl).StartEatenByBug();
					break;
			}
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		if (collider.CompareTag("Item")) {
			ItemCtrl item = collider.GetComponent<ItemCtrl>();
			switch (item.itemType) {
				case eItemType.Wood:
					(item as WoodCtrl).StopEatenByBug();
					break;
			}
		}
	}

	public override void DestroyItem() {
		StopCoroutine("RunMove");
		base.DestroyItem();
	}

	IEnumerator BurnFromFire() {
		yield return waitForBurning;
		DestroyItem();
	}
}