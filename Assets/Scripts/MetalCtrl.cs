public class MetalCtrl : ItemCtrl {

	// Use this for initialization
	void Start () {
        itemType = eItemType.Metal;
	}

	void OnCollisionEnter2D(UnityEngine.Collision2D collision) {
		if (collision.collider.CompareTag("Item")) {
			PlaySoundIfVelocityIsFast();
		}
	}
}
