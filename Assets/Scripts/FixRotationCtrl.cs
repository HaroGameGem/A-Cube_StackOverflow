using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixRotationCtrl : MonoBehaviour {
    Vector3 angle;

    private void Start()
    {
        angle = new Vector3(270f, 0f, 0f);
    }

    // Update is called once per frame
    void Update () {
        transform.eulerAngles = angle;
	}
}
