using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
	
	public float speed = 0.5f;
	public float rotate = 2.0f;

	public float altitudeLimitDegrees = 60.0f;//60 degrees up down is max
	private float altitudeLimit;

	private CharacterController controller;
	private Selector selector;

	private Vector3 facing;

	void Awake () {
		controller = GetComponent<CharacterController>();		
		selector = GetComponent<Selector>();

		facing = transform.forward;
		altitudeLimit = Mathf.Deg2Rad * altitudeLimitDegrees;
	}
	
	
	
	// Update is called once per frame
	void Update () {

		//TODO: remove after adjusting?
		altitudeLimit = Mathf.Sin(Mathf.Deg2Rad * altitudeLimitDegrees);


		//no moving while talking
		if(selector.isTalking){
			return;
		}

		if (Input.GetMouseButtonUp(0)){
			Screen.lockCursor = true;
			Screen.showCursor = false;
		}
		
		if (Input.GetKey(KeyCode.Escape)){
			Screen.showCursor = true;
		}

		
		if (Input.GetAxis("Vertical") != 0)
		{
			controller.Move(transform.forward * speed * Input.GetAxis("Vertical"));
		}
		if (Input.GetAxis("Horizontal") != 0)
		{
			controller.Move(transform.right * speed * Input.GetAxis("Horizontal"));
		}

		Debug.DrawLine(transform.position, transform.position + 10.0f * facing, Color.cyan, 0.5f, false);

		//componentwise rotation
		Quaternion yRot = Quaternion.Euler(0, Input.GetAxis("Mouse X"), 0);
		Quaternion xRot = Quaternion.Euler(-Input.GetAxis("Mouse Y"), 0, 0);


		//test to see if we look too far up/dorn
		Vector3 nextFacing = xRot * facing;
		if(nextFacing.y > altitudeLimit || nextFacing.y < -altitudeLimit){
			nextFacing = facing;
		}


		//add rotation around y
		nextFacing = yRot * nextFacing;

		//set facing
		facing = nextFacing;


		//rotate to look at it without the y axis
		transform.LookAt(transform.position + new Vector3(facing.x, 0, facing.z));

	}

	public Vector3 GetFacing(){
		return facing;
	}

}
