using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
	
	public float speed = 0.5f;
	public float rotate = 2.0f;

	private CharacterController controller;
	private Selector selector;

	void Awake () {
		controller = GetComponent<CharacterController>();		
		selector = GetComponent<Selector>();
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

		
		transform.Rotate (new Vector3 (0, Input.GetAxis("Mouse X") * rotate, 0), Space.World);
	}
}
