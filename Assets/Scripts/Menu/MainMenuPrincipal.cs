using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuPrincipal : MonoBehaviour {
	/*
	 * Autor: Carlos Andres Torres Cruz
	 * fecha de creacion: 8-04-2015
	 * fecha de actualizacion:
	 * funcion: clase que se encarga de acomodar de manera responsive los elementos del
	 * menu principal, es necesario incluir la libreria 'UnityEngine.UI' en el encabezado
	 */
	/* Variables Publicas */
	public GameObject upperStick;	  // Baner gris de la parte superior, contiene los botones de facebook, compra, coins etc.
	public GameObject coinsContainer; //Contenedor de coins, y boton de compra
	public GameObject coin;
	public GameObject buttonBuy;
	public GameObject buttonInfo;
	public GameObject buttonFace;
	/* Variables Privadas */
	private float widthScreen;		// Ancho de la pantalla
	private float heightScreen;		// Alto de la pantalla
	// Use this for initialization
	void Start () {
		Debug.Log (Screen.width +"-"+Screen.height);
		widthScreen = Screen.width;
		heightScreen = Screen.height;
		//responsiveLocation ();
	}
	/*
	 * Autor: Carlos Andres Torres Cruz
	 * fecha de creacion: 8-04-2015
	 * fecha de actualizacion:
	 * funcion: metodo que se encarga de llamar todas los metodos de ubicacion responsive
	 */
	void responsiveLocation()
	{
		locateUpperStick ();
		locateCoinsContainer ();
		locateCoin ();
		locateButtonBuy ();
		locateButtonInfo ();
		locateButtonFace ();
	}
	/*
	 * Autor: Carlos Andres Torres Cruz
	 * fecha de creacion: 8-04-2015
	 * fecha de actualizacion:
	 * funcion: metodo que se encarga de ubicar el UpperStick, baner superior
	 */
	void locateUpperStick()
	{
		RectTransform rectUS = upperStick.GetComponentInChildren<RectTransform> ();
		rectUS.anchoredPosition = new Vector2 (0, heightScreen*0.08f/-2f); 	// esta funcion edita la posicion y en el segundo parametro
		rectUS.sizeDelta = new Vector2( 0 , heightScreen*0.08f); //esta funcion edita el (left, right) en el primer parametro, y el height como segundo parametro
	}
	/*
	 * Autor: Carlos Andres Torres Cruz
	 * fecha de creacion: 8-04-2015
	 * fecha de actualizacion:
	 * funcion: metodo que se encarga de ubicar el contener de coins dentro del upperStick
	 */
	void locateCoinsContainer()
	{
		RectTransform rectCC = coinsContainer.GetComponentInChildren<RectTransform> ();
		RectTransform rectUS = upperStick.GetComponentInChildren<RectTransform> ();
		rectCC.anchoredPosition = new Vector2 (-heightScreen*0.15f, rectUS.rect.height /-2f);// esta funcion edita la posicion y en el segundo parametro
		rectCC.sizeDelta = new Vector2( heightScreen*0.22f , rectUS.rect.height /2f);
	}
	/*
	 * Autor: Carlos Andres Torres Cruz
	 * fecha de creacion: 8-04-2015
	 * fecha de actualizacion:
	 * funcion: metodo que se encarga de ubicar el icono de coins dentro del CoinsContainer
	 */
	void locateCoin()
	{
		RectTransform rectC = coin.GetComponentInChildren<RectTransform> ();
		RectTransform rectCC = coinsContainer.GetComponentInChildren<RectTransform> ();
		rectC.anchoredPosition = new Vector2 (rectCC.rect.width * -0.6f, rectCC.rect.height /-2f);
		rectC.sizeDelta = new Vector2( rectCC.rect.height *0.75f , rectCC.rect.height *0.75f);
	}
	/*
	 * Autor: Carlos Andres Torres Cruz
	 * fecha de creacion: 8-04-2015
	 * fecha de actualizacion:
	 * funcion: metodo que se encarga de ubicar el icono de compra
	 */
	void locateButtonBuy()
	{
		RectTransform rectBB = buttonBuy.GetComponentInChildren<RectTransform> ();
		RectTransform rectCC = coinsContainer.GetComponentInChildren<RectTransform> ();
		rectBB.anchoredPosition = new Vector2 (rectCC.rect.width * -0.8f, rectCC.rect.height /-2f);
		rectBB.sizeDelta = new Vector2( rectCC.rect.height *0.75f , rectCC.rect.height *0.75f);
	}
	/*
	 * Autor: Carlos Andres Torres Cruz
	 * fecha de creacion: 8-04-2015
	 * fecha de actualizacion:
	 * funcion: metodo que se encarga de ubicar el icono de informacion izquierdo
	 */
	void locateButtonInfo()
	{
		RectTransform rectBI = buttonInfo.GetComponentInChildren<RectTransform> ();
		RectTransform rectUS = upperStick.GetComponentInChildren<RectTransform> ();
		rectBI.anchoredPosition = new Vector2 (rectUS.rect.width * 0.07f, rectUS.rect.height /-2f);
		rectBI.sizeDelta = new Vector2( rectUS.rect.width *0.04f , rectUS.rect.height *0.75f);
	}
	/*
	 * Autor: Carlos Andres Torres Cruz
	 * fecha de creacion: 8-04-2015
	 * fecha de actualizacion:
	 * funcion: metodo que se encarga de ubicar el icono de facebook izquierdo
	 */
	void locateButtonFace()
	{
		RectTransform rectBF = buttonFace.GetComponentInChildren<RectTransform> ();
		RectTransform rectUS = upperStick.GetComponentInChildren<RectTransform> ();
		rectBF.anchoredPosition = new Vector2 (rectUS.rect.width * 0.12f, rectUS.rect.height /-2f);
		rectBF.sizeDelta = new Vector2( rectUS.rect.width *0.04f , rectUS.rect.height *0.75f);
	}
}
