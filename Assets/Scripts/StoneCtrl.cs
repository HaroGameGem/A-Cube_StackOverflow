using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneCtrl : ItemCtrl {

    public bool isSparking = false;

	// Use this for initialization
	void Start () {
        itemType = eItemType.Stone;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Item"))
        {
            ItemCtrl item = collision.gameObject.GetComponent<ItemCtrl>();
            if (item.itemType == eItemType.Stone)
            {
                StoneCtrl stone = item as StoneCtrl;
                if (stone != null)
                    stone.Spark(this);
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

        Vector3 dist = sender.transform.position - transform.position;
        Collider[] arrColl = Physics.OverlapSphere(transform.position + (dist * 0.5f), 1f);
        foreach (var n in arrColl)
        {
            ItemCtrl item = n.GetComponent<ItemCtrl>();
            if(item.itemType == eItemType.Wood)
            {
                WoodCtrl wood = item as WoodCtrl;
                if(wood != null)
                {
                    wood.Burn(this);
                }
            }
        }
    }
}
