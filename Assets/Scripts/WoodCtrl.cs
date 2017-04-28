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


	// Use this for initialization
	void Start () {
        renderer = GetComponent<SpriteRenderer>();
        originColor = renderer.color;
        Init();
	}
	
	// Update is called once per frame
	void Update () {
		
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

    public void Init()
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
        transform.position = Vector3.one * 100f;
        Init();
        ObjectPool.Release(gameObject);
    }
}
