using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class control_menu_slider : MonoBehaviour {
	/*
	 * Autor: Carlos Andres Torres Cruz
	 * fecha de creacion: 7-04-2015
	 * fecha de actualizacion:
	 * funcion: clase que se encarga de controlar el funcionamiento del slider de niveles
	 * es necesario incluir la libreria 'UnityEngine.UI' en el encabezado
	 */
	/*variables publicas */
	public Button rightButton;		//se enlaza con el boton de la derecha del slider en la interfaz
	public Button leftButton;		//se enlaza con el boton de la izquierda del slider en la interfaz
	public Button levelButton;		//se enlaza con el boton de seleccion de nivel del slider
	public Sprite[] spriteOfLevels;	//array con las imagenes de los niveles, tipo sprite
	/*variables privadas */
	private int indexOfLevel = 0;	//control del nivel de menu seleccionado actualmente

	/*
	 * Autor: Carlos Andres Torres Cruz
	 * fecha de creacion: 7-04-2015
	 * fecha de actualizacion:
	 * funcion: metodo que se ejecuta una sola vez en el ciclo de vida de la app, asigna
	 * una funcion a ejecutar a cada boton cuando sean presioandos, ademas de cargar la imagen
	 * inicial del menu de seleccion del slider, y de desactivar el boton izquierdo del slider
	 * para que el usuario no trate de ir a niveles desconocidos
	 */
	void Start () {
		levelButton.GetComponentInChildren<Image> ().sprite = spriteOfLevels[indexOfLevel];
		rightButton.onClick.AddListener(() => { Right(); });
		leftButton.onClick.AddListener(() => { Left(); });
		levelButton.onClick.AddListener(() => { currentLevel(); });
		leftButton.gameObject.SetActive (false);
	}
	/*
	 * Autor: Carlos Andres Torres Cruz
	 * fecha de creacion: 7-04-2015
	 * fecha de actualizacion:
	 * funcion: metodo que contrala el boton derecho del slider encargado de pasar las imagenes del nivel
	 * incrementalmente, de desactivar el boton derecho cuando el slider llege a su maximo numero de niveles
	 * y de activar el boton izquierdo del slider en caso que sea necesario
	 */
	void Right()
	{	
		if (indexOfLevel < spriteOfLevels.Length -1) {
			indexOfLevel++;
			levelButton.GetComponentInChildren<Image> ().sprite = spriteOfLevels [indexOfLevel];
			if (indexOfLevel > 0)
				leftButton.gameObject.SetActive (true);
			if (indexOfLevel == spriteOfLevels.Length -1 )
				rightButton.gameObject.SetActive(false);
		} 
	}
	/*
	 * Autor: Carlos Andres Torres Cruz
	 * fecha de creacion: 7-04-2015
	 * fecha de actualizacion:
	 * funcion: metodo que contrala el boton izquierdo del slider encargado de pasar las imagenes del nivel
	 * de forma decreciente, de desactivar el boton izquierdo cuando el slider llege a su minimo numero de niveles
	 * y de activar el boton derecho del slider en caso que sea necesario
	 */
	void Left()
	{
		if (indexOfLevel > 0 ) {
			indexOfLevel--;
			if (indexOfLevel < spriteOfLevels.Length - 1)
				rightButton.gameObject.SetActive (true);
			levelButton.GetComponentInChildren<Image> ().sprite = spriteOfLevels [indexOfLevel];
			if (indexOfLevel == 0)
				leftButton.gameObject.SetActive(false);
		}
	}
	/*
	 * Autor: Carlos Andres Torres Cruz
	 * fecha de creacion: 7-04-2015
	 * fecha de actualizacion:
	 * funcion: metodo que se encarga de controlar que nivel apunta cada imagen del slider
	 */
	void currentLevel()
	{
		switch (indexOfLevel)
		{
		case 0:
			//Debug.Log("Load Level 0");
			break;
		case 1:
			//Debug.Log("Load level 1");
			break;
		case 2:
			//Application.LoadLevel ("Level_01");
			break;
		default:
			Debug.Log("Default");
			break;
		}
	}
}
