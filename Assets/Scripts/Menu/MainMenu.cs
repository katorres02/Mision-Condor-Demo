using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public Button mission;
	public Button exit;

	public GameObject backgroundMain;
	public GameObject levelSelection;
	public GameObject containerOptions;

	void Awake()
	{
		levelSelection.SetActive (false);
		containerOptions.SetActive (false);
		mission.onClick.AddListener(() => { goToSelectLevels(); });
		exit.onClick.AddListener(() => { Application.Quit(); });
	}

	void goToSelectLevels()
	{
		backgroundMain.SetActive (false);
		levelSelection.SetActive (true);
	}

	public void backToMainFromLevelSelection()
	{

		levelSelection.SetActive (false);
		backgroundMain.SetActive (true);
	}

	public void loadLevelOne()
	{
		Application.LoadLevel ("Level0");
	}

	public void goToOptionsFromMain(){
		containerOptions.SetActive (true);
		backgroundMain.SetActive (false);
	}

}
