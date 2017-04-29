using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticCtrl : ItemCtrl {

    public bool isBreak = false;

    new private void Awake()
    {
        base.Awake();
        itemType = eItemType.Magnetic;
    }

    protected override void Init()
    {
        base.Init();
        isBreak = false;
    }

    public void Break(ItemCtrl sender)
    {
        if (isBreak)
        {
            DestroyItem();
        }
        TurnColor();
        isBreak = true;
    }
}
