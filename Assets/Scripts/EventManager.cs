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

	void RunStage() {
		runStage.Run();
	}

	void StopStage() {
		runStage.Stop();
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
	}

	public void Stop() {
		timer.Stop();
	}
}

public class Timer {

	//limit Time
	static int limitTime;
	static int limitTime_ { get { return limitTime; } set { limitTime = value; } }
	public static int LimitTime { set { limitTime = value; } }

	//wait a second
	WaitForSeconds waitForASecond = new WaitForSeconds(1f);

	//Result
	Result result = new Result();

	public void Run() {
		CoroutineDelegate.Instance.StartCoroutine(RunASecond());
	}

	IEnumerator RunASecond() {
		while(limitTime_ < 0) {
			yield return waitForASecond;
			--limitTime_;
		}
		Stop();
		result.Run();
	}

	public void Stop() {
		CoroutineDelegate.Instance.StopCoroutine(RunASecond());
	}
}

public class Result {

	public void Run() {

	}
}