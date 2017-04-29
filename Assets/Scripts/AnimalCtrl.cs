using System.Collections;
using UnityEngine;

public class AnimalCtrl : ItemCtrl {

	public bool isBurning = false;

	//meat;
	public GameObject meat;

	//meat objectPool
	static ObjectPool meatPool;

	//wait for burning
	WaitForSeconds waitForBurning = new WaitForSeconds(3f);

	new void Awake() {
		base.Awake();
		trans = transform;
		meatPool = ObjectPool.CreateFor(meat);
		itemType = eItemType.Animal;
	}

	protected override void Init() {
		isBurning = false;
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.CompareTag("Item")) {
			PlaySoundIfVelocityIsFast();
			switch (collision.collider.GetComponent<ItemCtrl>().itemType) {
				case eItemType.Fire:
					Burn();
					break;
			}
		}
	}

	public void Burn() {
		if (isBurning)
			return;
		SoundManager.Instance.PlayEffect(eEffectType.Animal);
		isBurning = true;
		//TurnColor();
		StartCoroutine(BurnFromFire());
	}

	IEnumerator BurnFromFire() {
		yield return waitForBurning;
		ConvertToMeat();
	}

	void ConvertToMeat() {
		GameObject dropItem = meatPool.Retain(trans.position);
		DestroyItem();
	}
}