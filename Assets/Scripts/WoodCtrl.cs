using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodCtrl : ItemCtrl {

    new SpriteRenderer renderer;

    public bool isBurning = false;
    public float burningLifeTime = 3f;
    public float burnDelay = 0.1f;

    float delay;
    Color originColor;

	//detecting bug flag
	int contactedBugCount = 0;
	int eatenRate = 0;
	WaitForSeconds waitForEatenByBug = new WaitForSeconds(0.1f);

	void Awake () {
        renderer = GetComponent<SpriteRenderer>();
        originColor = renderer.color;
	}

	protected override void Init() {
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
        renderer.color = Color.black;

        StartCoroutine(CoBurn());
    }

    IEnumerator CoBurn()
    {
        yield return new WaitForSeconds(burningLifeTime);
		DestroyItem();
    }
}
