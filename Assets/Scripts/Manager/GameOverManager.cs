using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverManager : MonoBehaviour {

	public PlayerHealth playerHealth;
	public float restartDelay = 5.0f;
	public AudioSource backgroundMusic;
	public AudioSource voiceMusic;
	public Button RestartLevel;

	
	Animator anim;
	float restartTimer;
	
	
	void Awake(){
		anim = GetComponent<Animator>();
		RestartLevel.onClick.AddListener(() => { Restartlevel(); });
		RestartLevel.enabled = false;
	}
	
	void Restartlevel(){
		Application.LoadLevel(Application.loadedLevel);
	}

	void Update()
	{
		if (playerHealth.currentHealth <= 0)
		{
			RestartLevel.enabled = true;
			voiceMusic.enabled = false;
			anim.SetTrigger("GameOver");
			backgroundMusic.Stop();

			/*restartTimer += Time.deltaTime;
			if(restartTimer >= restartDelay)
			{
				if(Input.GetButton("Fire1"))
				{
					Application.LoadLevel(Application.loadedLevel);
				}
			}*/
		}
	}
}
