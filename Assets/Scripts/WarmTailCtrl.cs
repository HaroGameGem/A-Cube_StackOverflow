using UnityEngine;

public class WarmTailCtrl : MonoBehaviour {

    WarmCtrl warmHead;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Item"))
        {
			ItemCtrl item = collision.gameObject.GetComponent<ItemCtrl>();
            if(item.itemType == eItemType.Fire)
            {
                warmHead.Burn();
            }
        }
        
    }
    
}
