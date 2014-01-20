using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
	
	public float speed = 0.5f;
	public float rotate = 2.0f;

	private CharacterController controller;
	private Selector selector;

	private Vector3 facing;

	void Awake () {
		controller = GetComponent<CharacterController>();		
		selector = GetComponent<Selector>();

		facing = transform.forward;
	}
	
	
	
	// Update is called once per frame
	void Update () {

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

		facing = Quaternion.Euler(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * facing;

		//rotate to look at it without the y axis
		transform.LookAt(transform.position + new Vector3(facing.x, 0, facing.z));

	}

	public Vector3 GetFacing(){
		return facing;
	}

}
