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

	void Awake () {
        renderer = GetComponent<SpriteRenderer>();
        originColor = renderer.color;
        Init();
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

    protected override void Init()
    {
        renderer.color = originColor;
        isBurning = false;
        delay = 0f;
    }

    public void Burn(ItemCtrl sender)
    {
        if (isBurning)
        {
            return;
        }

        if(sender.itemType != eItemType.Fire)
        {
            if (delay < burnDelay)
            {
                delay += Time.deltaTime;
                return;
            }
        }

        isBurning = true;
        renderer.color = Color.black;

        Debug.Log("탄다");

        StartCoroutine(CoBurn());
    }

    IEnumerator CoBurn()
    {
        yield return new WaitForSeconds(burningLifeTime);
		DestroyItem();
    }
}
