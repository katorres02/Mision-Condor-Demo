using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public Button newButton;
	public Button quitButton;

	// Use this for initialization
	void Start () {
		newButton.onClick.AddListener(() => { NewGame(); });
		quitButton.onClick.AddListener(() => { ExitGame(); });
	}

	void NewGame()
	{	
		Application.LoadLevel ("Level_01");
	}

	void ExitGame()
	{
		Application.Quit ();
	}
}
