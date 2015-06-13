using UnityEngine;
using System.Collections;
using UnitySampleAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour {

	//variables para la deteccion del joystick
	public Joystick leftJoystick;
	public Joystick rightJoystick;
	float posx;
	float posy;
	float posxr;
	float posyr;
	//...........................

	public float speed = 6f;

	Vector3 movement;

	Animator anim;
	Rigidbody playerRigidbody;
	//int floorMask;
	//float camRayLength = 100f;
	

	void Awake()
	{
		//floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent <Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
		//rightJoystick = leftJoystick;

		//recordar llamar la funcion loadSettings() al iniciar la escena
		//Settings.playerSettings.loadSettings ();

	}

	void FixedUpdate()
	{
		/********* controles de movimiento para dispositivos mobiles **************/
		//posx = leftJoystick.position.x;
		//posy = leftJoystick.position.y;
		posx = CrossPlatformInputManager.GetAxis ("Horizontal");
		posy = CrossPlatformInputManager.GetAxis ("Vertical");

		posxr = CrossPlatformInputManager.GetAxis ("HorizontalR");
		posyr = CrossPlatformInputManager.GetAxis ("VerticalR");
		
		if (posx > 0)
		{
			posx = 1;
		}
		else if(posx < 0)
		{
			posx = -1;
		}
		if(posy < 0)
		{
			posy = -1;
		}
		else if (posy >0)
		{
			posy = 1;
		}
		// Move the player around the scene.
		Move (posx, posy);
		Turning (posxr, posyr);
		Animating (posx, posy);
		/************************************************************************/


		/****** control de movimiento para PC **********************************
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		//float h = CrossPlatformInputManager.GetAxis ("Horizontal");
		//float v = CrossPlatformInputManager.GetAxis ("Vertical");
		
		Move ( h , v );
		Turning ();
		Animating (h, v);
		/*********************************************************************/
	}

	void Move(float h ,float v){
		/*movement.Set( h , 0f , v );
		movement = movement.normalized * speed * Time.deltaTime;
		playerRigidbody.MovePosition (transform.position + movement); */

		//playerRigidbody.MovePosition (new Vector3( 0, Mathf.Atan2( h, v) * 180 / Mathf.PI, 0 ) + movement);
		if (h != 0 || v != 0 && posxr ==0 && posyr == 0) {

			float temp_rotation_h = CrossPlatformInputManager.GetAxis ("Horizontal");
			float temp_rotation_v = CrossPlatformInputManager.GetAxis ("Vertical");
			
			transform.eulerAngles = new Vector3( 0, Mathf.Atan2( temp_rotation_h, temp_rotation_v) * 180 / Mathf.PI, 0 );
			transform.localPosition += transform.forward*Time.deltaTime*speed;
		}
		else {
			movement.Set( h , 0f , v );
			movement = movement.normalized * speed * Time.deltaTime;
			playerRigidbody.MovePosition (transform.position + movement);
		}
	}

	void Turning(float h, float v){
		/*
		 * control de rotacion 2015 eulerangles
		 */
		if (posxr != 0  || posyr != 0 )
			transform.eulerAngles = new Vector3( 0, Mathf.Atan2( h, v) * 180 / Mathf.PI, 0 );
		/*********************************************************************************/

		/************************Control de rotacion para moviles ************************
		float grado;
		if(h > 0 && h < 1 && v > 0 )
		{	grado = h *10;
			Quaternion newRotation = Quaternion.Euler(0, (grado*4.5f), 0);
			//transform.rotation = Quaternion.Euler(Vector3(0, (grado*4.5f), 0));
			playerRigidbody.MoveRotation (newRotation);
		}	
		else if(h > 0 && h > 0 && v < 1)
		{	grado = 10 - (h *10);
			//transform.rotation = Quaternion.Euler(Vector3(0, (grado*4.5)+45, 0));
			Quaternion newRotation = Quaternion.Euler(0, (grado*4.5f)+45f, 0);
			playerRigidbody.MoveRotation (newRotation);
		}
		
		if(h < 0 && h > -1 && v > 0 )
		{	grado =posx *-10;
			//transform.rotation = Quaternion.Euler(Vector3(0, 360-(grado*4.5), 0));
			Quaternion newRotation = Quaternion.Euler(0, 360f-(grado*4.5f), 0);
			playerRigidbody.MoveRotation (newRotation);
		}
		else if(h < 0 && v > 0 && v < 1)
		{	grado = v *10;
			//transform.rotation = Quaternion.Euler(Vector3(0, (grado*4.5)+270, 0));
			Quaternion newRotation = Quaternion.Euler(0, (grado*4.5f)+270f, 0);
			playerRigidbody.MoveRotation (newRotation);
		}
		
		if(h > 0 && v < 0 && v > -1)
		{	grado = v *-10;
			//transform.rotation = Quaternion.Euler(Vector3(0, (grado*4.5)+90, 0));
			Quaternion newRotation = Quaternion.Euler(0, (grado*4.5f)+90f, 0);
			playerRigidbody.MoveRotation (newRotation);
		}
		else if(h > 0 && h < 1 && v < 0 )
		{	grado =10 -(h *10);
			//transform.rotation = Quaternion.Euler(Vector3(0, (grado*4.5)+135, 0));
			Quaternion newRotation = Quaternion.Euler(0, (grado*4.5f)+135f, 0);
			playerRigidbody.MoveRotation (newRotation);
		}
		
		if(h < 0 && h > -1 && v < 0 )
		{	grado = posx *-10;
			//transform.rotation = Quaternion.Euler(Vector3(0, (grado*4.5)+180, 0));
			Quaternion newRotation = Quaternion.Euler(0, (grado*4.5f)+180f, 0);
			playerRigidbody.MoveRotation (newRotation);
		}
		else if(h < 0 && v < 0 && v > -1)
		{	grado = 10 -(v *-10);
			//transform.rotation = Quaternion.Euler(Vector3(0, (grado*4.5)+225, 0));
			Quaternion newRotation = Quaternion.Euler(0, (grado*4.5f)+225f, 0);
			playerRigidbody.MoveRotation (newRotation);
		}
		/******************************************************************************/

		/******* control de rotacion para PC  usando el cursor del mouse **************
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit;

		if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
		{
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;
			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
			playerRigidbody.MoveRotation(newRotation);
		}
		/*******************************************************************/
	}

	void Animating(float h, float v)
	{
		bool walking = h != 0 || v != 0;
		anim.SetBool ("IsWalking",walking);
	}
}
