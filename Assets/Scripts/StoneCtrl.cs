using UnityEngine;

public class StoneCtrl : ItemCtrl {

    public float radius = 0.8f;
    public bool isSparking = false;
    public bool isBreak = false;

    new private void Awake()
    {
        base.Awake();
		itemType = eItemType.Stone;
    }
	
    protected override void Init()
    {
        base.Init();
        isSparking = false;
        isBreak = false;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Item"))
        {
			PlaySoundIfVelocityIsFast();
			ItemCtrl item = collision.gameObject.GetComponent<ItemCtrl>();
            if (item.itemType == eItemType.Stone)
            {
                StoneCtrl stone = item as StoneCtrl;
                if (stone != null)
                {
                    if (stone.rigidbody2D.velocity.magnitude > 0.5f)
                    {
						SoundManager.Instance.PlayEffect(eEffectType.StoneCrash);
                        stone.Spark(this);
                    }
                }
            }
        }
    }

    public void Spark(ItemCtrl sender)
    {
        if (isSparking)
        {
            return;
        }

        if (sender.itemType == eItemType.Stone)
        {
            StoneCtrl stone = sender as StoneCtrl;
            isSparking = true;
            stone.isSparking = true;
        }

        Vector2 dist = sender.transform.position - transform.position;
        Vector2 pos = transform.position;
        dist = pos + (dist * 0.5f);
        ParticleManager.SpawnParticle(eParticleType.CrashParticle, dist);
        Collider2D[] arrColl = Physics2D.OverlapCircleAll(dist, radius);
        foreach (var n in arrColl)
        {
            ItemCtrl item = n.GetComponent<ItemCtrl>();
            if(item != null)
            {
                if (item.itemType == eItemType.Wood)
                {
                    WoodCtrl wood = item as WoodCtrl;
                    if (wood != null)
                    {
                        wood.Burn(this);
                    }
                }

                if (item.itemType == eItemType.Bomb)
                {
                    BombCtrl bomb = item as BombCtrl;
                    if (bomb != null)
                    {
                        bomb.Burn(this);
                    }
                }
            }
        }   
    }

    public void Break(ItemCtrl sender)
    {
        if(isBreak)
        {
			SoundManager.Instance.PlayEffect(eEffectType.StoneBreak);
            ParticleManager.SpawnParticle(eParticleType.BreakParticle, trans.position);
			DestroyItem();
        }
        ParticleManager.SpawnParticle(eParticleType.KrunchParticle, trans.position);
        TurnColor();
        isBreak = true;
    }
}
