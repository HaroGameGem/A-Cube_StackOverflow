using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorCtrl : ItemCtrl {

    bool isFlip = false;
    float originRotSpeed = 0f;
    public float rotSpeed = 90f;

	// Use this for initialization
	void Start () {
        originRotSpeed = rotSpeed;
	}

    protected override void Init()
    {
        base.Init();
        rotSpeed = originRotSpeed + Random.Range(0f, rotSpeed);
        if (Random.Range(0, 2) == 0)
        {
            isFlip = true;
            rotSpeed = Mathf.Abs(rotSpeed);
        }
        else
        {
            isFlip = false;
            rotSpeed = -Mathf.Abs(rotSpeed);
        }
    }

    // Update is called once per frame
    void Update () {

        transform.Rotate(new Vector3(0f, 0f, 1f) * rotSpeed * Time.deltaTime);
	}
}
