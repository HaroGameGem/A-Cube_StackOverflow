using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodCtrl : ItemCtrl {

    public bool isBurning = false;
    public float burningLifeTime = 2f;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
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
                        wood.Burn();
                }
            }
        }
    }

    void Burn()
    {
        Debug.Log("탄다");
    }
}
