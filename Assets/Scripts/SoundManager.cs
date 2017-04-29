using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	//singleton
	static SoundManager instance = null;
	public static SoundManager Instance { get { return instance; } }

	//audio sources
	AudioSource[] effectSources;

	//effect list
	public AudioClip[] effectClips;

	//number of channel
	public int channelCount;
	bool[] isUsingChannels;

	//pitch range
	public float minPitch;
	public float maxPitch;

	void Awake() {
		instance = this;
		effectSources = new AudioSource[channelCount];
		isUsingChannels = new bool[channelCount];
		GameObject parentObject = gameObject;
		for(int i = 0; i < effectSources.Length; ++i) {
			effectSources[i] = parentObject.AddComponent<AudioSource>();
			isUsingChannels[i] = false;
		}
	}

	public void PlayEffect(eEffectType effectType) {
		if((int)effectType >= effectClips.Length) {
			return;
		}
		int channelIndex = GetUnusedChannelIndex();
		effectSources[channelIndex].clip = effectClips[(int)effectType];
		effectSources[channelIndex].pitch = Random.Range(minPitch, maxPitch);
		effectSources[channelIndex].Play();
		StartCoroutine(RunEffect(channelIndex));
	}

	int GetUnusedChannelIndex() {
		for(int i = 0; i < isUsingChannels.Length; ++i) {
			if(!isUsingChannels[i]) {
				return i;
			}
		}
		effectSources[0].Stop();
		return 0;
	}

	IEnumerator RunEffect(int index) {
		isUsingChannels[index] = true;
		yield return new WaitWhile(() => (effectSources[index].isPlaying));
		isUsingChannels[index] = false;
	}
}
