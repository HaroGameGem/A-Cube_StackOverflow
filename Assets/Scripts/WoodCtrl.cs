using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WoodCtrl : ItemCtrl {

    public bool isBurning = false;
    public float burningLifeTime = 3f;
    public float burnDelay = 0.1f;

    float delay;

	//detecting bug flag
	int contactedBugCount = 0;
	int eatenRate = 0;
	WaitForSeconds waitForEatenByBug = new WaitForSeconds(0.1f);

    Tweener scaleTweener = null;

    new void Awake () {
		base.Awake();
        itemType = eItemType.Wood;
		originColor = renderer.color;
	}

	protected override void Init() {
        base.Init();
		renderer.color = originColor;
		isBurning = false;
		delay = 0f;
		contactedBugCount = 0;
		eatenRate = 0;
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
		while(eatenRate < 30) {
			yield return waitForEatenByBug;
			eatenRate += 1;
		}
		DestroyItem();
	}

	private void OnCollisionStay2D(Collision2D collision)
    {
        if(isBurning)
        {
            if (collision.collider.CompareTag("Item"))
            {
                ItemCtrl item = collision.gameObject.GetComponent<ItemCtrl>();
                if(item.itemType == eItemType.Wood)
                {
                    WoodCtrl wood = item as WoodCtrl;
                    if (wood != null)
                        wood.Burn(this);
                }

                if (item.itemType == eItemType.Bomb)
                {
                    BombCtrl bomb = item as BombCtrl;
                    if (bomb != null)
                        bomb.Burn(this);
                }
            }
        }
    }

    public void Burn(ItemCtrl sender)
    {
        if (isBurning)
        {
            return;
        }

        if((sender.itemType == eItemType.Wood))
        {
            if (delay < burnDelay)
            {
                delay += Time.deltaTime;
                return;
            }
        }

        isBurning = true;
        TurnColor();
        scaleTweener = transform.DOScale(0f, burningLifeTime * 4f);
        StartCoroutine(CoBurn());
    }

    IEnumerator CoBurn()
    {
        yield return new WaitForSeconds(burningLifeTime);
        if (scaleTweener != null)
            scaleTweener.Kill();
        scaleTweener = null;
		DestroyItem();
    }
}
