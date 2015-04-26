using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingAsyncTest : MonoBehaviour {

	private AsyncOperation async;
	public Slider healthSlider;
	public Text textPercentage;

	void Start()
	{
		StartCoroutine (LoadLevelWithBar("menu_selection"));
	}

	IEnumerator LoadLevelWithBar(string level)
	{
		async = Application.LoadLevelAsync(level);
		while (!async.isDone) {
			healthSlider.value = async.progress;
			textPercentage.text = (async.progress * 100) +"%";
			yield return null;
		}
	}

/*	void OnGUI()
	{
		if (async != null) {
			healthSlider.value = async.progress;
			textPercentage.text = (async.progress * 100) +"%";
			if (async.progress == 0.1)
				Debug.Log("llegue al 10%");
			else if(async.progress == 0.2)
				Debug.Log("llegue al 20%");
			Debug.Log (async.progress);
		}
	}*/

	/*
	IEnumerator Start() {
		AsyncOperation async = Application.LoadLevelAsync("Level_01");
		Debug.Log (async.progress);
		yield return async;
		Debug.Log("Loading complete");
	}*/
}
