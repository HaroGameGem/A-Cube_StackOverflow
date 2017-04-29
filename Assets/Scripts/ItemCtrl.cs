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

    protected void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable() {
		trans = transform;
		Init();
		StartCoroutine("RunCheckYForDestroy");
	}

	protected virtual void Init() { }

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
}
