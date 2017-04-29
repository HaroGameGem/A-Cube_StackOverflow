using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatCtrl : ItemCtrl {

	new void Awake() {
		base.Awake();
		itemType = eItemType.Meat;
	}
}
