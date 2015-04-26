using UnityEngine;
using System.Collections;

public class SwipeDetector : MonoBehaviour 
{
	public float minSwipeDistX;
	private Vector2 startPos;

	private float screenCenter;
	private Vector3 initialPos;
		
	void Start(){
		screenCenter = Screen.width*0.7f;
	}

	void Update()
	{
		if (Input.touchCount > 0)
		{
			Touch touch = Input.touches[0];
			if (touch.position.x > 0 && touch.position.x < screenCenter){
				switch (touch.phase){	
				case TouchPhase.Began:
					startPos = touch.position;
					initialPos = new Vector3(touch.position.x, 0, 0);
					break;
				case TouchPhase.Moved:
					if (touch.position.x > startPos.x){ //moving rigth
						startPos = touch.position;
						float rotatation_value = (new Vector3(startPos.x, 0, 0) - initialPos).x;
						transform.Rotate(Vector3.down * Time.deltaTime * rotatation_value);
					}else if(touch.position.x < startPos.x ){ //moving left;
						startPos = touch.position;
						float rotatation_value = (initialPos - new Vector3(startPos.x, 0, 0)).x;
						if (rotatation_value < 0)
							rotatation_value = rotatation_value * -1;
						transform.Rotate(Vector3.down * Time.deltaTime*-rotatation_value);
					}
					break;
				}
			}

		}
	}
}