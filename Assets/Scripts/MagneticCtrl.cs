public class MagneticCtrl : ItemCtrl {

    public bool isBreak = false;

    new private void Awake()
    {
        base.Awake();
        itemType = eItemType.Magnetic;
    }

    protected override void Init()
    {
        base.Init();
        isBreak = false;
    }

	void OnCollisionEnter2D(UnityEngine.Collision2D collision) {
		if (collision.collider.CompareTag("Item")) {
			PlaySoundIfVelocityIsFast();
		}
	}

	public void Break(ItemCtrl sender)
    {
        if (isBreak)
        {
            DestroyItem();
        }
        TurnColor();
        isBreak = true;
    }
}
