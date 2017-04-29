using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : ItemCtrl {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Item"))
        {
            ItemCtrl item = collision.gameObject.GetComponent<ItemCtrl>();
            if (item.itemType == eItemType.Wood)
            {
                WoodCtrl wood = item as WoodCtrl;
                if (wood != null)
                    wood.Burn(this);
            }
            if(item.itemType == eItemType.Bomb)
            {
                BombCtrl bomb = item as BombCtrl;
                if (bomb != null)
                    bomb.Burn(this);
            }

            DestroyItem();
        }
    }
}
