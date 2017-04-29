using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
	static InputManager instance;
	public static InputManager Instance {
		get {
			return instance;
		}
	}

	public GameObject[] arrTargetObject;
	public Rigidbody2D[] arrTargetRigidbody;

    public Transform[] arrUITarget;

	public float moveForce = 1f;
	public float rotSpeed = 1f;

	private void Awake() {
		instance = this;
		arrTargetObject = new GameObject[2];
		arrTargetRigidbody = new Rigidbody2D[2];
	}

	// Update is called once per frame
	void Update() {
		if (arrTargetObject[0] != null) {
			if (arrTargetRigidbody[0] != null) {
				//위
				if (Input.GetKey(KeyCode.W)) {
					arrTargetRigidbody[0].AddForce(Vector3.up * moveForce * 1.8f * Time.deltaTime, ForceMode2D.Impulse);
				}
				//아래
				if (Input.GetKey(KeyCode.S)) {
					arrTargetRigidbody[0].AddForce(Vector3.down * moveForce * 1.8f * Time.deltaTime, ForceMode2D.Impulse);
				}
				//왼쪽
				if (Input.GetKey(KeyCode.A)) {
					arrTargetRigidbody[0].AddForce(Vector3.left * moveForce * Time.deltaTime, ForceMode2D.Impulse);
					arrTargetObject[0].transform.Rotate(new Vector3(0f, 0f, 1f * rotSpeed));
				}
				//오른쪽
				if (Input.GetKey(KeyCode.D)) {
					arrTargetRigidbody[0].AddForce(Vector3.right * moveForce * Time.deltaTime, ForceMode2D.Impulse);
					arrTargetObject[0].transform.Rotate(new Vector3(0f, 0f, -1f * rotSpeed));
				}
				//변경좌측
				if (Input.GetKeyDown(KeyCode.R)) {
					if (SpawnManager.Instance[0].SelectedIndex <= 0) {
						SpawnManager.Instance[0].SelectedIndex = 4;
					} else {
						--SpawnManager.Instance[0].SelectedIndex;
					}
				}
				//변경우측
				if (Input.GetKeyDown(KeyCode.T)) {
					if (SpawnManager.Instance[0].SelectedIndex >= 4) {
						SpawnManager.Instance[0].SelectedIndex = 0;
					} else {
						++SpawnManager.Instance[0].SelectedIndex;
					}
				}
			}

            arrUITarget[0].position = arrTargetObject[0].transform.position;
		}
		if (arrTargetObject[1] != null) {
			if (arrTargetRigidbody[1] != null) {
				//위
				if (Input.GetKey(KeyCode.P)) {
                    arrTargetRigidbody[1].AddForce(Vector3.up * moveForce * 1.8f * Time.deltaTime, ForceMode2D.Impulse);
				}
				//아래
				if (Input.GetKey(KeyCode.Semicolon)) {
					arrTargetRigidbody[1].AddForce(Vector3.down * moveForce * 1.8f * Time.deltaTime, ForceMode2D.Impulse);
				}
				//왼쪽
				if (Input.GetKey(KeyCode.L)) {
					arrTargetRigidbody[1].AddForce(Vector3.left * moveForce * Time.deltaTime, ForceMode2D.Impulse);
					arrTargetObject[1].transform.Rotate(new Vector3(0f, 0f, 1f * rotSpeed));
				}
				//오른쪽
				if (Input.GetKey(KeyCode.Quote)) {
					arrTargetRigidbody[1].AddForce(Vector3.right * moveForce * Time.deltaTime, ForceMode2D.Impulse);
					arrTargetObject[1].transform.Rotate(new Vector3(0f, 0f, -1f * rotSpeed));
				}
				//변경좌측
				if (Input.GetKeyDown(KeyCode.RightBracket)) {
					if (SpawnManager.Instance[1].SelectedIndex <= 0) {
						SpawnManager.Instance[1].SelectedIndex = 4;
					} else {
						--SpawnManager.Instance[1].SelectedIndex;
					}
				}
				//변경우측
				if (Input.GetKeyDown(KeyCode.Backslash)) {
					if (SpawnManager.Instance[1].SelectedIndex >= 4) {
						SpawnManager.Instance[1].SelectedIndex = 0;
					} else {
						++SpawnManager.Instance[1].SelectedIndex;
					}
				}
			}

            arrUITarget[1].position = arrTargetObject[1].transform.position;
        }


    }
}