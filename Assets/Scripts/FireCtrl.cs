using UnityEngine;

public class FireCtrl : ItemCtrl {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Item"))
        {
			PlaySoundIfVelocityIsFast();
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
            if(item.itemType == eItemType.Metal)
            {
                MetalCtrl metal = item as MetalCtrl;
                if (metal != null)
                    metal.Hit();
            }
        }

        DestroyItem();
    }
}
