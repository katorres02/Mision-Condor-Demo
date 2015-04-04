using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseManager : MonoBehaviour {


	public GameObject hudCanvas;
	public GameObject pauseCanvas;

	public AudioSource backgroundMusic;

	//
	public Sprite play;
	public Sprite pause;
	public Button pauseButton;
	Image images;
	// Use this for initialization
	void Start () {
		pauseButton.onClick.AddListener(() => { PauseGame(); });
		images = gameObject.GetComponentInChildren<Image>();
	}
	
	// Update is called once per frame
	void PauseGame () {

		if (Time.timeScale == 1) 
		{
			backgroundMusic.Pause();
			images.sprite = play;
			Time.timeScale = Time.timeScale == 0 ? 1 : 0;
		}
		else
		{
			backgroundMusic.Play();
			images.sprite = pause;
			Time.timeScale = Time.timeScale == 0 ? 1 : 0;
		}
	}

	void Update()
	{
		if ( Input.GetKeyDown(KeyCode.Escape))
		{
			if (Time.timeScale == 1) 
			{
				Time.timeScale = 0;
				images.sprite = play;
				//Time.timeScale = Time.timeScale == 0 ? 1 : 0;

			}
			else
			{	Time.timeScale = Time.timeScale == 0 ? 1 : 0;
				Application.LoadLevel("MainMenu");
				//images.sprite = pause;
				//Time.timeScale = Time.timeScale == 0 ? 1 : 0;
			}
		}
	}
}
