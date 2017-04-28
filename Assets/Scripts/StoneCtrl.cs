using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneCtrl : ItemCtrl {

    public bool isSparking = false;

<<<<<<< HEAD
	// Use this for initialization
	void Start () {
        itemType = eItemType.Stone;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

=======
>>>>>>> 6fc9c091357c97d2ec30e2a9679459fc273e142c
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
        foreach (var item in arrColl)
        {

        }
    }
}
