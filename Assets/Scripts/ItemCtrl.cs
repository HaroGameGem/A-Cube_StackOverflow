using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCtrl : MonoBehaviour {

	//item type
	public eItemType itemType;

	//transform
	Transform trans;

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
    public Color turnColor;

    protected void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        originColor = renderer.color;
    }

    void OnEnable() {
		trans = transform;
		Init();
		StartCoroutine("RunCheckYForDestroy");
	}

	protected virtual void Init() {
        InitColor();
    }

    IEnumerator RunCheckYForDestroy() {
		while(true) {
			if(trans.position.y < -100) {
				DestroyItem();
			}
			yield return waitForCheckY;
		}
	}

	public virtual void DestroyItem() {
		trans.position = Vector3.one * 500f;
		Init();
		StopCoroutine("RunCheckYForDestroy");
		ObjectPool.Release(gameObject);
	}

    public virtual void TurnColor()
    {
        renderer.color = turnColor;
    }

    public virtual void InitColor()
    {
        renderer.color = originColor;
    }
}
