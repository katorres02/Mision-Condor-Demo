using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Facebook.MiniJSON;

public class FBcontrol : MonoBehaviour {

	public Button faceButton;
	public GameObject UIFBIsLoggedIn;
	public GameObject UIFBIsNotLoggedIn;
	public GameObject UIFBAvatar;
	public GameObject UIFBIsUserName;
	private Dictionary<string, string> profile = null;
	public GameObject UITextNameJhon;

	private List<object> scoreList = null;
	public GameObject ScoreScrollList;
	public GameObject ScoreEntryPanel;

	public GameObject scoresCanvas;
	
	// Use this for initialization
	void Awake () {
		FB.Init (SetInit, OnHideUnity);
	}
	
	private void SetInit()
	{
		Debug.Log ("FB Init done");
		if (FB.IsLoggedIn) { // la variable IsLoggedIn es propia del SDK de facebook, indica si un usuario esta logueado o no
			Debug.Log("FB Logged In");	
		} else {
			faceButton.gameObject.SetActive (true);
		}
	}
	
	/*
	 * Este metodo controla los pop ups de facebook, cuando se indique, el juego debera ser pausado para perder el focus
	 * y asignarlo a una ventana emergente de facebook
	 */
	private void OnHideUnity(bool isGameShown)
	{
		if (!isGameShown) {
			Time.timeScale = 0; // pusa el juego si algo en facebook esta tomando mi atencion	
		} else {
			Time.timeScale = 1; // continua con el ciclo normal del juego si no hay nada de facebook que necesite mi atencion
		}
	}

	public void FBlogin()
	{
		if (!FB.IsLoggedIn) {
			FB.Login ("email"/*,publish_actions"*/, AuthCallback);
		} else {
			FB.Logout();
			UIFBIsLoggedIn.SetActive(false);
			UITextNameJhon.SetActive(true);
		}
	}
	/*
	 * Este metodo es el mmetodo delegado que debe ser llamado por la funcion login cuando el proceso de logueo termine
	 * Recibe cuenta con un parametro por defecto que es el reuslt del proceso de logueo
	 */
	void AuthCallback(FBResult result)
	{
		if (FB.IsLoggedIn) {
			FB.API("/me/permissions", Facebook.HttpMethod.GET, delegate (FBResult response) {
				// inspect the response and adapt your UI as appropriate
				// check response.Text and response.Error
				Debug.Log(response.Text);
			});
			Debug.Log ("FB login worked!");
			DealWithFBMenus(true);
		} else {
			DealWithFBMenus(false);
			Debug.Log("FB login fail! :( ");
		}
	}

	/*
	 * Este metodo controla los elementos de UI que se muestran o no en caso de que el usuario este logueado en facebook
	 */
	void DealWithFBMenus(bool isLoggedIn)
	{
		if (isLoggedIn) {
			UIFBIsLoggedIn.SetActive (true);
			UITextNameJhon.SetActive(false);
			// Get profile picture code
			/*
			 * Para obtener la foto de perfil de facebook se hace referencia a la funcion 'API' que se encuentra en 'FB.API'
			 * recibe como parametros una cadena de caracteres que sea la peticion que se queire optener de facebook, el tipo
			 * de peticion post o get, y una funcion felegada que se encargara de usar la respuesta obtenida
			 */
			FB.API ( GetPictureURL("me", 128, 128),Facebook.HttpMethod.GET, DealWithProfilePicture);
			// Get username code
			FB.API ("/me?fields=id,first_name,last_name,age_range,birthday,devices,education,gender,email,relationship_status,work", Facebook.HttpMethod.GET, DealWithUserName);
			
		} else {
			UIFBIsLoggedIn.SetActive (false);
			UITextNameJhon.SetActive(true);
		}
	}

	/*
	 * Este metodo sirve para crear el query que consume la API del SDK  de facebook de manera dinamica dependiendo
	 * del user id y los tamaños widht y height que entren como paramatros para la imagen de perfil de facebook,
	 * retorna un string
	 */
	public static string GetPictureURL(string facebookID, int? width = null, int? height = null, string type = null)
	{
		string url = string.Format("/{0}/picture", facebookID);
		string query = width != null ? "&width=" + width.ToString() : "";
		query += height != null ? "&height=" + height.ToString() : "";
		query += type != null ? "&type=" + type : "";
		if (query != "") url += ("?g" + query);
		return url;
	}

	void DealWithUserName(FBResult result)
	{
		Debug.Log (result.Text);
		if (result.Error != null) {
			Debug.Log("Problem with getting User Name");
			FB.API ("/me?fields=id,first_name,last_name,age_range,birthday,devices,education,gender,email,relationship_status,work", Facebook.HttpMethod.GET, DealWithUserName);
			return;
		}
		var responseObject = Json.Deserialize(result.Text) as Dictionary<string, object>;
		Debug.Log (responseObject);
		object nameH;
		object lastName;
		var temp_response = new Dictionary<string, string>();
		if (responseObject.TryGetValue("first_name", out nameH))
		{
			temp_response["first_name"]= (string)nameH;
			Debug.Log(nameH);
		}
		if (responseObject.TryGetValue("last_name", out lastName))
		{
			temp_response["last_name"]= (string)lastName;
		}
		profile = temp_response;
		Text userName = UIFBIsUserName.GetComponent<Text> ();
		userName.text =  profile["first_name"]+" "+profile["last_name"];
		Debug.Log (result.Text);
	}
	/*
	 * Obtiene la respuesta de la peticion de imagen de perfil, en caso de obtener respuesta valida coloca
	 * la imagen de perfil
	 */
	void DealWithProfilePicture(FBResult result)
	{
		if (result.Error != null) {
			Debug.Log("Problem with getting profile picture");
			FB.API ( GetPictureURL("me", 128, 128),Facebook.HttpMethod.GET, DealWithProfilePicture);
			return;
		}
		
		Image UserAvatar = UIFBAvatar.GetComponent<Image> (); //Accedemos al componente Imagen del gameObject UIFBAvatar
		/*
		 * La respuesra de de facebook con la imagen de perfil es de tipo 'Texture', para poder ser colocado en la interfaz
		 * de unity se necesita convertirla tipo 'sprite' que es el tipo de imagen permitido para UI en unity
		 */
		UserAvatar.sprite = Sprite.Create (result.Texture, new Rect (0, 0, 128, 128), new Vector2 (0, 0));
	}

	/*
	 * Puntuacion API, los metodos a continuacion se encargan de controlar la asignacion de puntajes y 
	 * demas tareas relacionadas
	 */
	public void QueryScores()
	{
		if (FB.IsLoggedIn)
			FB.API ("/app/scores?fields=score,user.limit(30)", Facebook.HttpMethod.GET, ScoresCallBack);
		else
			Debug.Log("No esta logueado");
	}
	
	private void ScoresCallBack(FBResult result)
	{
		Debug.Log ("Scores callback " + result.Text);
		
		scoreList = DeserializeScores (result.Text);
		
		foreach (Transform child in ScoreScrollList.transform) {
			GameObject.Destroy(child.gameObject);
		}
		
		foreach (object score in scoreList) {
			var entry = (Dictionary<string, object>) score;
			var user = (Dictionary<string, object>) entry["user"];
			
			GameObject ScorePanel;
			ScorePanel = Instantiate (ScoreEntryPanel) as GameObject;
			ScorePanel.transform.SetParent( ScoreScrollList.transform);
			ScorePanel.transform.localScale = new Vector3(1,1,1); 

			Transform ThisScoreName = ScorePanel.transform.Find("FriendName");
			Transform ThisScoreScore = ScorePanel.transform.Find("FriendScore");
			Text ScoreName = ThisScoreName.GetComponent<Text>();
			Text ScoreScore = ThisScoreScore.GetComponent<Text>();
			
			ScoreName.text = user["name"].ToString();
			ScoreScore.text = entry["score"].ToString();
			
			Transform TheFriendAvatar = ScorePanel.transform.Find("FriendAvatar");
			Image friendAvatar = TheFriendAvatar.GetComponent<Image>();
			
			FB.API ( GetPictureURL(user["id"].ToString(), 128, 128),Facebook.HttpMethod.GET, delegate(FBResult pictureResult) {
				if(pictureResult.Error != null){//su hubo error
					Debug.Log(pictureResult.Error);
				}else{
					friendAvatar.sprite = Sprite.Create( pictureResult.Texture, new Rect(0,0,128,128), new Vector2(0,0));
				}
			});
		}
		scoresCanvas.SetActive (true);
	}

	private List<object> DeserializeScores(string response) 
	{
		
		var responseObject = Json.Deserialize(response) as Dictionary<string, object>;
		object scoresh;
		var scores = new List<object>();
		if (responseObject.TryGetValue ("data", out scoresh)) 
		{
			scores = (List<object>) scoresh;
		}
		return scores;
	}

	public void closeScores(){
		scoresCanvas.SetActive (false);
	}

}
