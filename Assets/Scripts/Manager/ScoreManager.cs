using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Facebook.MiniJSON;

using System;
using System.Runtime.Serialization.Formatters.Binary; //convertir informacion a binaria para envitar hackers
using System.IO;

public class ScoreManager : MonoBehaviour {
	public static int score;
	Text text;

	void Awake (){
		text = GetComponent <Text> ();
		loadManagerScore ();
	}

	void Start(){
		//Debug.Log (InternetChecker.InternetConnectionChecker.isConnected);
	}

	void Update (){
		text.text = score.ToString();
	}

	/*
	 * Autor: Carlos Andres Torres Cruz
	 * fecha de creacion: 30-04-2015
	 * fecha de actualizacion:
	 * funcion: metodo que se encarga de traer la puntuacion almacenada por el jugador, en caso de que este logeado
	 * o no, y con acceso a internet
	 */
	void loadManagerScore(){
		if (InternetChecker.InternetConnectionChecker.isConnected && FB.IsLoggedIn) {
			loadScoresFromFacebook();
		} else {
			loadScoreOffLine();
		}
	}
	/*
	 * Autor: Carlos Andres Torres Cruz
	 * fecha de creacion: 30-04-2015
	 * fecha de actualizacion:
	 * funcion: metodo que se encarga de traer la puntuacion almacenada por el jugador, en caso de que no tenga
	 * acceso a internet
	 */
	void loadScoreOffLine(){
		if (File.Exists (Application.persistentDataPath + "/ScoreUserData.dat")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath +"/ScoreUserData.dat", FileMode.Open);
			UserCurrentScore data = (UserCurrentScore)bf.Deserialize(file);
			file.Close();
			score = data.userScore;
		}
	}
	/*
	 * Autor: Carlos Andres Torres Cruz
	 * fecha de creacion: 30-04-2015
	 * fecha de actualizacion:
	 * funcion: metodo que se encarga de traer la puntuacion almacenada por el jugador, desde la base de datos de facebook
	 */
	void loadScoresFromFacebook(){
		QueryScoreUser ();
	}

	/*
	 * Autor: Carlos Andres Torres Cruz
	 * fecha de creacion: 30-04-2015
	 * fecha de actualizacion:
	 * funcion: metodo que se encarga de hacer la peticion a facebook para que retorne la informacion de los puntajes
	 */
	public void QueryScoreUser()
	{
		FB.API ("/me/scores", Facebook.HttpMethod.GET, ScoreUserCallBack);
	}
	/*
	 * Autor: Carlos Andres Torres Cruz
	 * fecha de creacion: 30-04-2015
	 * fecha de actualizacion:
	 * funcion: metodo que se encarga de trabajar con el resultado obtenido den la peticion de facebook
	 */
	private void ScoreUserCallBack(FBResult result){
		Debug.Log (result.Text);
		List<object> userScore = null;
		userScore = DeserializeScores (result.Text);

		foreach (object scores in userScore) {
			var entry = (Dictionary<string, object>) scores;
			score = int.Parse(entry["score"].ToString());
			 
		}
	}
	/*
	 * Autor: Carlos Andres Torres Cruz
	 * fecha de creacion: 30-04-2015
	 * fecha de actualizacion:
	 * funcion: metodo que se encarga de convertir a json los datos obtenidos desde facebook
	 */
	private List<object> DeserializeScores(string response){
		var responseObject = Json.Deserialize(response) as Dictionary<string, object>;
		object scoresh;
		var scores = new List<object>();
		if (responseObject.TryGetValue ("data", out scoresh)) 
		{
			scores = (List<object>) scoresh;
		}
		return scores;
	}
}

	/*
	 * Autor: Carlos Andres Torres Cruz
	 * fecha de creacion: 30-04-2015
	 * fecha de actualizacion:
	 * funcion: Clase para generar una estrucura de datos a guardarse en un archivo local
	 */
// Se debe crear una clase aparte privada para almacenar la informacion cuando se quiere guardar en un archivo aparte
// porque la clase de mas arriba hereda de MonoBehaviour y eso genera problemas con la creacion y lectura de archivos.
// ya que esta clase sera convertida a un formato binario se debe serializar
[Serializable]
class UserCurrentScore{
	public int userScore;
}
