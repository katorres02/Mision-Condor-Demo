using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BarSliderControl : MonoBehaviour {

	public GameObject slider;
	public GameObject[] bars;
	

	void Start () {
		for (int i=0; i< bars.Length ; i++) {
			bars[i].SetActive(false);
		}
	}

	void Update(){


		for (int i=0; i< slider.GetComponent<Slider> ().value*10 ; i++) {
			bars[i].SetActive(true);
		}
		float dif = slider.GetComponent<Slider> ().value * 10;
		for (int i=9; i>= dif ; i--) {
			bars[i].SetActive(false);
		}
	}

}
