using System.Collections;
using UnityEngine;
using DG.Tweening;

public class WarmCtrl : ItemCtrl
{
    public bool isBurning = false;
    public GameObject burningParticle;
    public SpriteRenderer[] arrTailSpriteRenderer;

    //collision count
    int collisionCount = 0;

    //wait for move
    //WaitForSeconds waitForMove = new WaitForSeconds(.1f);

    //wait for burning
    //WaitForSeconds waitForBurning = new WaitForSeconds(3f);
    public float burningLifeTime = 3f;

    Tweener scaleTweener = null;

    new void Awake()
    {
        base.Awake();
        itemType = eItemType.Bug;
    }

    protected override void Init()
    {
        base.Init();
        StartCoroutine("RunMove");
        collisionCount = 0;
        isBurning = false;
        burningParticle.SetActive(false);
    }

    IEnumerator RunMove()
    {
        float jumpPower = Random.Range(3f, 6f);
        while (true)
        {
            if (collisionCount <= 0)
            {
                yield return new WaitForSeconds(Random.Range(0.05f, 0.15f));
            }
            if (Random.Range(0, 2) == 0)
            {
                rigidbody2D.AddForce(jumpPower * (Vector2.up + Vector2.left + Vector2.up), ForceMode2D.Impulse);
            }
            else
            {
                rigidbody2D.AddForce(jumpPower * (Vector2.up + Vector2.right + Vector2.up), ForceMode2D.Impulse);
            }
            yield return new WaitForSeconds(Random.Range(0.1f, 0.2f));
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        ++collisionCount;
        if (collision.collider.CompareTag("Item"))
        {
            switch (collision.collider.GetComponent<ItemCtrl>().itemType)
            {
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

    void OnCollisionExit2D(Collision2D collision)
    {
        --collisionCount;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Item"))
        {
            ItemCtrl item = collider.GetComponent<ItemCtrl>();
            switch (item.itemType)
            {
                case eItemType.Wood:
                    (item as WoodCtrl).StartEatenByBug();
                    break;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Item"))
        {
			PlaySoundIfVelocityIsFast();
			ItemCtrl item = collider.GetComponent<ItemCtrl>();
            switch (item.itemType)
            {
                case eItemType.Wood:
                    (item as WoodCtrl).StopEatenByBug();
                    break;
            }
        }
    }

    public override void DestroyItem()
    {
        StopCoroutine("RunMove");
        base.DestroyItem();
    }

    public override void TurnColor()
    {
        base.TurnColor();
        for (int i = 0; i < arrTailSpriteRenderer.Length; i++)
        {
            arrTailSpriteRenderer[i].color = desiredTurnColor;
        }
    }

    public override void InitColor()
    {
        base.InitColor();
        for (int i = 0; i < arrTailSpriteRenderer.Length; i++)
        {
            arrTailSpriteRenderer[i].color = originColor;
        }
    }

    public void Burn()
    {
        if (isBurning)
            return;
		SoundManager.Instance.PlayEffect(eEffectType.Burning);
		isBurning = true;
        burningParticle.SetActive(true);
        TurnColor();
        scaleTweener = transform.DOScale(0f, burningLifeTime * 4f);
        StartCoroutine(BurnFromFire());
    }

    IEnumerator BurnFromFire()
    {
        yield return new WaitForSeconds(burningLifeTime);
		DestroyItem();
    }
}