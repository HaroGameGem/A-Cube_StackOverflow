public class MeatCtrl : ItemCtrl {

	new void Awake() {
		base.Awake();
		itemType = eItemType.Meat;
	}

	void OnCollisionEnter2D(UnityEngine.Collision2D collision) {
		if (collision.collider.CompareTag("Item")) {
			PlaySoundIfVelocityIsFast();
		}
	}
}