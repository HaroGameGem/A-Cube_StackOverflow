using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

	//singleton
	static SpawnManager[] instance = null;
	public static SpawnManager[] Instance { get { return instance; } }
	[SerializeField]
	int singletonIndex;

	//spawn position
	Transform spawnPosition;

	//target items
	public GameObject[] itemObjects;
	ObjectPool[] itemPools;

	//selected items per turn
	ObjectPool[] selectedItems = new ObjectPool[5];

	//selected item index
	int selectedIndex = 0;
	public int SelectedIndex {
		get { return selectedIndex; }
		set {
			selectedIndex = value;
			SoundManager.Instance.PlayEffect(eEffectType.ItemChange);
			UIManager.Instance.SetSelectedItem(singletonIndex, selectedIndex);
		}
	}

	public float spawnDelayPerSec = 3f;
	public WaitForSeconds waitForSpawn;

	void Awake() {
		InitializeSingleton();
		spawnPosition = transform;
		itemPools = new ObjectPool[itemObjects.Length];
		for (int i = 0; i < itemObjects.Length; ++i) {
			itemPools[i] = ObjectPool.CreateFor(itemObjects[i]);
		}
		waitForSpawn = new WaitForSeconds(spawnDelayPerSec);
	}

	void InitializeSingleton() {
		if (instance == null) {
			instance = new SpawnManager[2];
		}
		instance[singletonIndex] = this;
	}

	void SelecteItemsPerTurn() {
		for(int i = 0; i < selectedItems.Length; ++i) {
			selectedItems[i] = itemPools[Random.Range(0, itemPools.Length)];
		}
		UIManager.Instance.SetSelectedItemsFromObjectPools(singletonIndex, selectedItems);
	}

	public void Run() {
		StartCoroutine(CoSpawn());
	}

	public void Stop() {
		StopAllCoroutines();
	}

	IEnumerator CoSpawn() {
		while (true) {
			SelecteItemsPerTurn();
			yield return waitForSpawn;
			GameObject dropItem = selectedItems[SelectedIndex].Retain(spawnPosition.position);
			SoundManager.Instance.PlayEffect(eEffectType.ItemCreate);
			GiveControlFocus(dropItem);
		}
	}

	void GiveControlFocus(GameObject dropItem) {
		InputManager inputManager = InputManager.Instance;
		inputManager.arrTargetObject[singletonIndex] = dropItem.gameObject;
		inputManager.arrTargetRigidbody[singletonIndex] = dropItem.GetComponent<Rigidbody2D>();
	}
}