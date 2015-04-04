using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class TimerManager : MonoBehaviour {

	public float maxTime;
	Text text;
	float timer;

	// Use this for initialization
	void Start () {
		text = GetComponent <Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		text.text = timer.ToString ("0");

	}
}
