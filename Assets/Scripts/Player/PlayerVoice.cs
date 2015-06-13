using UnityEngine;
using System.Collections;

public class PlayerVoice : MonoBehaviour {

	AudioSource audio;

	public AudioClip thismeanstrouble;
	public AudioClip hurry;
	public AudioClip underAttack;
	
	float voiceTroubleTimer;
	float voiceHurryTimer;
	float voiceUnderAttackTimer;

	float playTroubleTime = 35.0f;		//Se reprodduce cuando nace el golem
	float playHurryTime = 8.0f;		
	float playUndeAttackTime = 15.0f;

	void Awake()
	{
		audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {

		voiceTroubleTimer += Time.deltaTime;
		voiceHurryTimer += Time.deltaTime;
		voiceUnderAttackTimer += Time.deltaTime;

		if (playTroubleTime <= voiceTroubleTimer && audio.isActiveAndEnabled ) 
		{
			audio.clip = thismeanstrouble;
			audio.Play();
			voiceTroubleTimer = 0;
		}
		else if(playHurryTime <= voiceHurryTimer && audio.isActiveAndEnabled)
		{
			audio.clip = hurry;
			audio.Play();
			voiceHurryTimer = 0;
		}
		else if(playUndeAttackTime <= voiceUnderAttackTimer && audio.isActiveAndEnabled)
		{
			audio.clip = underAttack;
			audio.Play();			 
			voiceUnderAttackTimer = 0;
		}
	}
}
