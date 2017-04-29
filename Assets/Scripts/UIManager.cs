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

	//selector
	public Transform[] selectors;

	//time text
	public Text timeText;

	//result
	public GameObject resultWindow;
	public Text winPlayer;
	public Text count0Text;
	public Text count1Text;

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

	public void SetSelectedItem(int playerIndex, int itemIndex) {
		if(playerIndex == 0) {
			selectors[playerIndex].position = selectedItems0[itemIndex].transform.position;
		} else {
			selectors[playerIndex].position = selectedItems1[itemIndex].transform.position;
		}
	}

	public void SetTime(int time) {
		timeText.text = time.ToString();
	}

	public void SetResult() {
		resultWindow.SetActive(true);
		int count1 = Area.Instance[0].ObjectCount;
		int count2 = Area.Instance[1].ObjectCount;
		if(count1 < count2) {
			winPlayer.text = "1";
		} else if(count1 > count2) {
			winPlayer.text = "2";
		} else {
			winPlayer.text = "?";
		}
		count0Text.text = count1.ToString();
		count1Text.text = count2.ToString();
	}
}