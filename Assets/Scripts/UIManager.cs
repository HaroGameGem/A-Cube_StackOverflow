using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	//singleton
	static UIManager instance = null;
	public static UIManager Instance { get { return instance; } }

	//selected items
	public SpriteRenderer[] selectedItems0;
	public SpriteRenderer[] selectedItems1;

	//area count
	public Text[] areaCount;

	void Awake() {
		instance = this;
	}

	public void SetSelectedItemsFromObjectPools(int itemsIndex, ObjectPool[] selectedPools) {
		for(int i = 0; i < selectedItems0.Length; ++i) {
			if(itemsIndex == 0) {
				selectedItems0[i].sprite = selectedPools[i].Origin.GetComponent<SpriteRenderer>().sprite;
				selectedItems0[i].color = selectedPools[i].Origin.GetComponent<SpriteRenderer>().color;
			} else if(itemsIndex == 1) {
				selectedItems1[i].sprite = selectedPools[i].Origin.GetComponent<SpriteRenderer>().sprite;
				selectedItems1[i].color = selectedPools[i].Origin.GetComponent<SpriteRenderer>().color;
			}
		}
	}

	public void SetAreaCount(int areaIndex, int value) {
		areaCount[areaIndex].text = value.ToString();
	}
}