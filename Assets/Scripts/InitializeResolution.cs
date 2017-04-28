using UnityEngine;

public class InitializeResolution {

	//resolution rate
	const int WIDTH_RATE = 16;
	const int HEIGHT_RATE = 9;

	public void FixResolution() {
		int screenWidth = Screen.width;
		int screenHeight = Screen.height;
		if(screenWidth / screenHeight >= WIDTH_RATE / HEIGHT_RATE) {
			screenWidth = GetFixedWidthToHeight();
		} else {
			screenHeight = GetFixedHeightToWidth();
		}
		Screen.SetResolution(screenWidth, screenHeight, false);
	}

	int GetFixedWidthToHeight() {
		return Screen.height * WIDTH_RATE / HEIGHT_RATE;
	}

	int GetFixedHeightToWidth() {
		return Mathf.CeilToInt(Screen.width * HEIGHT_RATE / WIDTH_RATE);
	}
}
