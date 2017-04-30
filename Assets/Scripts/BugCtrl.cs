using System.Collections;
using UnityEngine;

public class BugCtrl : ItemCtrl {

	public bool isBurning = false;
    public GameObject burningParticle;

	//collision count
	int collisionCount = 0;

	//wait for move
	WaitForSeconds waitForMove = new WaitForSeconds(3f);

	//wait for burning
	WaitForSeconds waitForBurning = new WaitForSeconds(3f);

	//bug type
	public eBugType bugType;

	new void Awake() {
		base.Awake();
		itemType = eItemType.Bug;
	}

	protected override void Init() {
        base.Init();
		StartCoroutine("RunMove");
		collisionCount = 0;
		isBurning = false;
        burningParticle.SetActive(false);
	}

	IEnumerator RunMove() {
		const float jumpPower = 4f;
		while (true) {
			if (collisionCount <= 0) {
				yield return waitForMove;
			}
			if (Random.Range(0, 2) == 0) {
				rigidbody2D.AddForce(jumpPower * (Vector2.up + Vector2.left + Vector2.up), ForceMode2D.Impulse);
			} else {
				rigidbody2D.AddForce(jumpPower * (Vector2.up + Vector2.right + Vector2.up), ForceMode2D.Impulse);
			}
			switch(bugType) {
				case eBugType.Cockroach:
					SoundManager.Instance.PlayEffect(eEffectType.Cockroach);
					break;
				case eBugType.Coreana:
					SoundManager.Instance.PlayEffect(eEffectType.Coreana);
					break;
				case eBugType.Trumph:
					SoundManager.Instance.PlayEffect(eEffectType.Trump1 + Random.Range(0, 3));
					break;
			}
			yield return waitForMove;
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		++collisionCount;
		if (collision.collider.CompareTag("Item")) {
			PlaySoundIfVelocityIsFast();
			switch (collision.collider.GetComponent<ItemCtrl>().itemType) {
				case eItemType.Fire:
					Burn();
					break;
                case eItemType.Metal:
                    {
                        MetalCtrl metal = collision.collider.GetComponent<MetalCtrl>();
                        if (metal.isHitting)
                            Burn();
                    }
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
				case eItemType.Fruit:
					(item as FruitCtrl).StartEatenByBug();
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

	public void Burn() {
		if (isBurning)
			return;
		SoundManager.Instance.PlayEffect(eEffectType.Burning);
		isBurning = true;
        burningParticle.SetActive(true);
		TurnColor();
		StartCoroutine(BurnFromFire());
	}

	IEnumerator BurnFromFire() {
		yield return waitForBurning;
		DestroyItem();
	}
}