using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

	//singleton
	static EventManager instance = null;
	public static EventManager Instance { get { return instance; } }

	//initialize stage
	InitializeStage initializeStage = new InitializeStage();

	//Run stage
	RunStage runStage = new RunStage();

	//Result stage
	Result result = new Result();

	//limit time
	[SerializeField]
	int limitTime = 60;

	void Awake() {
		instance = this;
		Initialize();
	}

	void Initialize() {
		Timer.LimitTime = limitTime;
		initializeStage.Initialize();
	}

	void Start() {
		RunStage();
	}

	public void RunStage() {
		runStage.Run();
	}

	public void StopStage() {
		runStage.Stop();
	}

	public void RunResult() {
		result.Run();
	}
}

public class InitializeStage {

	//fix resolution
	InitializeResolution initializeResolution = new InitializeResolution();

	public void Initialize() {
		initializeResolution.FixResolution();
	}
}

public class RunStage {

	//timer for limit time
	Timer timer = new Timer();

	public void Run() {
		timer.Run();
		SpawnManager.Instance[0].Run();
		SpawnManager.Instance[1].Run();
	}

	public void Stop() {
		timer.Stop();
		SpawnManager.Instance[0].Stop();
		SpawnManager.Instance[1].Stop();
	}
}

public class Timer {

	//coroutine
	//Coroutine runASecond;

	//limit Time
	static int limitTime;
	static int limitTime_ {
		get { return limitTime; }
		set {
			limitTime = value;
			UIManager.Instance.SetTime(limitTime);
		}
	}
	public static int LimitTime { set { limitTime = value; } }

	//wait a second
	WaitForSeconds waitForASecond = new WaitForSeconds(1f);

	public void Run() {
		CoroutineDelegate.Instance.StartCoroutine(RunASecond());
	}

	IEnumerator RunASecond() {
		while(limitTime_ > 0) {
			yield return waitForASecond;
			--limitTime_;
		}
		EventManager.Instance.StopStage();
		EventManager.Instance.RunResult();
	}

	public void Stop() {
		CoroutineDelegate.Instance.StopCoroutine(RunASecond());
	}
}

public class Result {

	public void Run() {

	}
}