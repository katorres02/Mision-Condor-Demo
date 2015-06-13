using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// las 3 librerias siguientes, son necesarias para serliazar informacion, convertir a binario y para escritura y lectura de archivos
using System;
using System.Runtime.Serialization.Formatters.Binary; //convertir informacion a binaria para envitar hackers
using System.IO;

public class Options : MonoBehaviour {

	public static Options playerSettings;

	public GameObject sliderMusic;
	public GameObject sliderSound;
	public GameObject sliderBrightness;
	public GameObject canvasOptions;
	public GameObject canvasMainMenu;
	public GameObject buttonVibrateOption;
	public Sprite vibrateNo;
	public Sprite vibrateYes;

	private float myMusic;
	private float mySound;
	private float myBrightness;
	private bool myVibrate = false;
	private bool loaded = false; //controla que solo se llave la funcion LoadOptionsData una vez

	void Awake(){
		if (myVibrate) {
			Image img = buttonVibrateOption.GetComponent<Image>();
			img.sprite = vibrateYes;
			myVibrate = true;
		} else {
			Image img = buttonVibrateOption.GetComponent<Image>();
			img.sprite = vibrateNo;
			myVibrate = false;
		}
	}

	void Update(){
		if( canvasOptions.activeSelf && loaded == false ){
			LoadOptionsData();
			//Debug.Log("cargado");
		}
		if (myVibrate) {
			Image img = buttonVibrateOption.GetComponent<Image>();
			img.sprite = vibrateYes;
		} else {
			Image img = buttonVibrateOption.GetComponent<Image>();
			img.sprite = vibrateNo;
		}
		myMusic = sliderMusic.GetComponent<Slider> ().value;
		mySound = sliderSound.GetComponent<Slider> ().value;
		myBrightness = sliderBrightness.GetComponent<Slider> ().value;
	}

	public void ShareOnFacebook(){
		if (FB.IsLoggedIn) {
			FB.Feed (
				linkCaption: "I'm playing this awesome game",
				picture: "https://fbcdn-photos-c-a.akamaihd.net/hphotos-ak-xfa1/t39.2081-0/p128x128/11057032_368565326672249_504750798_n.png",
				linkName: "Check out this game, I just scored "+ScoreManager.score.ToString()+" points",
				//link: "https://www.facebook.com/games/mision-condor"
				link: "http://apps.facebook.com/"+FB.AppId+"/?challenge_brag="+(FB.IsLoggedIn ? FB.UserId : "guest")
			);
		}
	}

	public void vibrateContorl(){
		myVibrate = !myVibrate;
	}

	public void goBack(){
		canvasMainMenu.SetActive (true);
		canvasOptions.SetActive (false);
		SaveOptions();
	}

	public void SaveOptions(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerOptionsData.dat");// crea un archivo en blanco, se le pasa como parametro la ruta
		
		GameOptionsData data = new GameOptionsData ();
		data.music = myMusic;
		data.sound = mySound;
		data.brightness = myBrightness;
		data.vibrate = myVibrate;
		bf.Serialize (file, data);
		file.Close ();
		loaded = false;
		//Debug.Log("guardado");
	}

	public void LoadOptionsData() {
		if (File.Exists (Application.persistentDataPath + "/playerOptionsData.dat")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath +"/playerOptionsData.dat", FileMode.Open);
			GameOptionsData data = (GameOptionsData)bf.Deserialize(file);
			file.Close();

			sliderMusic.GetComponent<Slider>().value = data.music;
			sliderBrightness.GetComponent<Slider>().value = data.brightness;
			sliderSound.GetComponent<Slider>().value = data.sound;
			myVibrate = data.vibrate;
			loaded = true;
		}
	}

	public void backTOMain(){
		//Application.LoadLevel ("menu_selection");
		Time.timeScale = Time.timeScale == 0 ? 1 : 0;
		Application.LoadLevel ("menu_from_level");
	}


}

// Se debe crear una clase aparte privada para almacenar la informacion cuando se quiere guardar en un archivo aparte
// porque la clase de mas arriba hereda de MonoBehaviour y eso genera problemas con la creacion y lectura de archivos.
// ya que esta clase sera convertida a un formato binario se debe serliazar
[Serializable]
class GameOptionsData{
	public float music;
	public float sound;
	public float brightness;
	public bool vibrate;
}
