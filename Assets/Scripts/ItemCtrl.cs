using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemCtrl : MonoBehaviour {

	//item type
	public eItemType itemType;

	//transform
	protected Transform trans;

    //Rigidbody
    [HideInInspector]
    public new Rigidbody2D rigidbody2D;
    //Renderer
    [HideInInspector]
    public new SpriteRenderer renderer;

    //wait for checkDestroy
    WaitForSeconds waitForCheckY = new WaitForSeconds(0.1f);

    // 기존 색
    [HideInInspector]
    public Color originColor;

    // 상호작용 시 바뀔 색
    public Color desiredTurnColor;
    Tweener colorTweener = null;

    public Vector3[] scale = new Vector3[3];

    protected void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        originColor = renderer.color;
        for (int i = 0; i < 3; i++)
        {
            if (scale[i] == Vector3.zero)
                scale[i] = Vector3.one  * (0.8f + (0.15f * i));
        }
    }

    void OnEnable() {
		trans = transform;
		Init();
		StartCoroutine("RunCheckYForDestroy");
	}

    protected virtual void Init() {
        StopAllCoroutines();
        InitColor();
        int sizeIdx = Random.Range(0, 3);
        trans.localScale = scale[sizeIdx];
    }

    IEnumerator RunCheckYForDestroy() {
		while(true) {
			if(trans.position.y < -20 || trans.position.y > 20
				|| trans.position.x < -20 || trans.position.x > 20) {
				DestroyItem();
			}
			yield return waitForCheckY;
		}
	}

	public virtual void DestroyItem() {
		trans.position = Vector3.one * 500f;
 		StopCoroutine("RunCheckYForDestroy");
		ObjectPool.Release(gameObject);
	}


    public virtual void TurnColor()
    {
        if (colorTweener != null)
            return;

        colorTweener = renderer.DOColor(desiredTurnColor, 1f)
            .OnComplete(() => { colorTweener = null; });

    }

    public virtual void InitColor()
    {
        if (colorTweener != null)
        {
            colorTweener.Kill();
            colorTweener = null;
        }
        renderer.color = originColor;
    }
}
