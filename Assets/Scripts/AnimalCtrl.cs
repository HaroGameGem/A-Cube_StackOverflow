using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalCtrl : ItemCtrl {

	public bool isBurning = false;

	//wait for burning
	WaitForSeconds waitForBurning = new WaitForSeconds(3f);

	//animal
	public GameObject animal;

	//meat
	public GameObject meat;

	new void Awake() {
		//base.Awake();
		itemType = eItemType.Animal;
	}

	protected override void Init() {
		animal.SetActive(true);
		meat.SetActive(false);
		isBurning = false;
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.CompareTag("Item")) {
			if(animal.activeSelf) {
				switch (collision.collider.GetComponent<ItemCtrl>().itemType) {
					case eItemType.Fire:
						Burn();
						break;
				}
			}
		}
	}

	public void Burn() {
		if (isBurning)
			return;
		isBurning = true;
		//TurnColor();
		StartCoroutine(BurnFromFire());
	}

	IEnumerator BurnFromFire() {
		yield return waitForBurning;
		ConvertToMeat();
	}

	void ConvertToMeat() {
		animal.SetActive(false);
		meat.SetActive(true);
	}
}