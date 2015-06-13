using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using System;
using System.Runtime.Serialization.Formatters.Binary; //convertir informacion a binaria para envitar hackers
using System.IO;

public class PlayerHealth : MonoBehaviour {

	public int startingHealth = 100;
	public int currentHealth;
	public Slider healthSlider;
	public Image damageImage;
	public AudioClip deathClip;
	public float flashSpeed = 5f;
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

	Animator anim;
	AudioSource playerAudio;
	PlayerMovement playerMovement;
	PlayerShooting playerShooting;
	bool isDead;
	bool damaged;

	void Awake (){
		anim = GetComponent <Animator> ();
		playerAudio = GetComponent <AudioSource> ();
		playerMovement = GetComponent <PlayerMovement> ();
		playerShooting = GetComponentInChildren <PlayerShooting> ();
		currentHealth = startingHealth;
	}
	
	
	void Update (){
		if(damaged){
			damageImage.color = flashColour;
		}
		else{
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;
	}

	public void TakeDamage (int amount){
		damaged = true;
		currentHealth -= amount;
		healthSlider.value = currentHealth;
		playerAudio.Play ();
		if(currentHealth <= 0 && !isDead){
			Death ();
		}
	}

	void Death (){
		SaveScore ();
		isDead = true;
		playerShooting.DisableEffects ();
		anim.SetTrigger ("Die");
		playerAudio.clip = deathClip;
		playerAudio.Play ();
		playerMovement.enabled = false;
		playerShooting.enabled = false;
	}
	
	public void RestartLevel (){
		Application.LoadLevel (Application.loadedLevel);
	}

	/*
	 * Autor: Carlos Andres Torres Cruz
	 * fecha de creacion: 30-04-2015
	 * fecha de actualizacion:
	 * funcion: Almacena el puntaje obtenido en base datos local o en facebook
	 */
	public void SaveScore(){

		if (InternetChecker.InternetConnectionChecker.isConnected && FB.IsLoggedIn) {
			saveOnLine();
			saveOffLine();
		} else {
			saveOffLine();
		}
	}
	/*
	 * Autor: Carlos Andres Torres Cruz
	 * fecha de creacion: 30-04-2015
	 * fecha de actualizacion:
	 * funcion: Almacena el puntaje obtenido en facebook
	 */
	public void saveOnLine(){
		var score_data = new Dictionary<string,string> ();
		score_data ["score"] = ScoreManager.score.ToString();
		FB.API ("/me/scores", Facebook.HttpMethod.POST, delegate(FBResult result) {
			Debug.Log("Score submit result: "+result.Text);
		}, score_data);
	}
	/*
	 * Autor: Carlos Andres Torres Cruz
	 * fecha de creacion: 30-04-2015
	 * fecha de actualizacion:
	 * funcion: Almacena el puntaje obtenido en el dispositivo de manera local
	 */
	public void saveOffLine(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/ScoreUserData.dat");// crea un archivo en blanco, se le pasa como parametro la ruta
		UserCurrentScore data = new UserCurrentScore ();
		data.userScore = ScoreManager.score;
		bf.Serialize (file, data);
		file.Close ();
	}

}
