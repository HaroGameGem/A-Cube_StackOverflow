using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    static InputManager instance;
    public static InputManager Instance
    {
        get
        {
            return instance;
        }
    }

    public GameObject[] arrTargetObject;
    public Rigidbody2D[] arrTargetRigidbody;

    public float moveForce = 1f;

    private void Awake()
    {
        instance = this;
        arrTargetObject = new GameObject[2];
        arrTargetRigidbody = new Rigidbody2D[2];
    }
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if(arrTargetObject[0] != null)
        {
            Debug.Log(arrTargetRigidbody[0]);
            if(arrTargetRigidbody[0] != null)
            {
                //위
                if (Input.GetKey(KeyCode.W))
                {

                }
                //아래
                if (Input.GetKey(KeyCode.S))
                {

                }
                //왼쪽
                if (Input.GetKey(KeyCode.A))
                {
                    arrTargetRigidbody[0].AddForce(Vector3.left * moveForce, ForceMode2D.Force);
                    Debug.Log("hi");
                }
                //오른쪽
                if (Input.GetKey(KeyCode.D))
                {
                    arrTargetRigidbody[0].AddForce(Vector3.right * moveForce, ForceMode2D.Force);
                }
            }
        }
        if(arrTargetObject[1] != null)
        {
            if(arrTargetRigidbody[1] != null)
            {
                //위
                if (Input.GetKey(KeyCode.P))
                {

                }
                //아래
                if (Input.GetKey(KeyCode.Semicolon))
                {

                }
                //왼쪽
                if (Input.GetKey(KeyCode.L))
                {
                    arrTargetRigidbody[1].AddForce(Vector3.left * moveForce, ForceMode2D.Force);
                }
                //오른쪽
                if (Input.GetKey(KeyCode.Quote))
                {
                    arrTargetRigidbody[1].AddForce(Vector3.right * moveForce, ForceMode2D.Force);
                }
            }
        }


    }
}
