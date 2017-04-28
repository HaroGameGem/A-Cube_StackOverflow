using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    public GameObject prefab;
    public Transform P1spawnPosObject;
    public Transform P2spawnPosObject;

    ObjectPool prefabPool;

    public float spawnDelayPerSec = 3f;

	// Use this for initialization
	void Start () {
        StartCoroutine(CoSpawn());

        prefabPool = ObjectPool.CreateFor(prefab, 100);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Run()
    {
        StartCoroutine(CoSpawn());
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    IEnumerator CoSpawn()
    {
        yield return new WaitForSeconds(spawnDelayPerSec);
        //GameObject P1Object = Instantiate(prefab, P1spawnPosObject.position, Quaternion.identity);
        //GameObject P2Object = Instantiate(prefab, P2spawnPosObject.position, Quaternion.identity);
        GameObject P1Object = prefabPool.Retain(P1spawnPosObject.position, Quaternion.identity);
        GameObject P2Object = prefabPool.Retain(P2spawnPosObject.position, Quaternion.identity);
        
        InputManager inputManager = InputManager.Instance;

        inputManager.arrTargetObject[0] = P1Object.gameObject;
        inputManager.arrTargetObject[1] = P2Object.gameObject;
        inputManager.arrTargetRigidbody[0] = P1Object.GetComponent<Rigidbody2D>();
        inputManager.arrTargetRigidbody[1] = P2Object.GetComponent<Rigidbody2D>();
        StartCoroutine(CoSpawn());
    }
}
