using System.Collections;
using UnityEngine;

public class FruitCtrl : ItemCtrl {

	//detecting bug flag
	int contactedBugCount = 0;
	int eatenRate = 0;
	WaitForSeconds waitForEatenByBug = new WaitForSeconds(0.1f);

	protected override void Init() {
		contactedBugCount = 0;
		eatenRate = 0;
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if(collision.collider.CompareTag("Item")) {
			PlaySoundIfVelocityIsFast();
		}
	}

	public void StartEatenByBug() {
		if (contactedBugCount == 0) {
			StartCoroutine("EatenByBug");
		}
		++contactedBugCount;
	}

	public void StopEatenByBug() {
		--contactedBugCount;
		if (contactedBugCount <= 0) {
			StopCoroutine("EatenByBug");
		}
	}

	IEnumerator EatenByBug() {
		while (eatenRate < 30) {
			yield return waitForEatenByBug;
			eatenRate += 1;
		}
		DestroyItem();
	}
}
