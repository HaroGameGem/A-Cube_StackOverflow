using UnityEngine;
using System.Collections;

public class MetalCtrl : ItemCtrl {

    public bool isHitting = false;
    public float hittingLifeTime = 3f;
    public GameObject hittingParticle;

    new void Awake()
    {
        base.Awake();
        itemType = eItemType.Metal;
        originColor = renderer.color;
    }

    protected override void Init()
    {
        base.Init();
        renderer.color = originColor;
        isHitting = false;
        hittingParticle.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Item"))

        {
            PlaySoundIfVelocityIsFast();
            ItemCtrl item = collision.gameObject.GetComponent<ItemCtrl>();

            if(isHitting)
            {
                if (item.itemType == eItemType.Wood)
                {
                    WoodCtrl wood = item as WoodCtrl;
                    if (wood != null)
                        wood.Burn(this);
                }
                if (item.itemType == eItemType.Bomb)
                {
                    BombCtrl bomb = item as BombCtrl;
                    if (bomb != null)
                        bomb.Burn(this);
                }
                if(item.itemType == eItemType.Animal)
                {
                    AnimalCtrl animal = item as AnimalCtrl;
                    if (animal != null)
                        animal.Burn();
                }
            }
        }
    }

    public void Hit()
    {
        if (isHitting)
            StopCoroutine(CoHitFromFire());

        //SoundManager.Instance.PlayEffect(eEffectType.Burning);
        isHitting = true;
        hittingParticle.SetActive(true);
        StartCoroutine(CoHitFromFire());
    }

    IEnumerator CoHitFromFire()
    {
        yield return new WaitForSeconds(hittingLifeTime);
        isHitting = false;
        hittingParticle.SetActive(false);
    }
}
