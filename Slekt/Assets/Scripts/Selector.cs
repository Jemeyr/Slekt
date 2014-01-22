using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Selector : MonoBehaviour {

	public float TargetRange = 15.0f;


	private Move move;
	public bool isTalking = false;


	private Target hilighted;



	private Ray ray;

	void Awake () {
		move = transform.GetComponentInChildren<Move>();
	}
	
	void Update () {

		Target hovered = null;
		RaycastHit hit;

		Debug.DrawLine(transform.position, transform.position + TargetRange * move.GetFacing(),Color.yellow);


		ray.origin = transform.position;
		ray.direction = move.GetFacing();

		if(Physics.Raycast(ray, out hit, TargetRange)){

			hovered = hit.transform.GetComponent<Target>();

			if(Input.GetMouseButtonDown(1)){
				Debug.Log(hovered);
			}

		}



		//lose track of hilighted target
		if(hilighted != null && hovered != hilighted){
			if(hilighted != null){
				hilighted.Unhover();
			}
		}

		//hover or activate a target
		if(hovered != null){
			if(Input.GetMouseButtonDown(0)){
				//isTalking should be something else I think. Maybe stick it in a player state and give the
				// selector ability to flip it at will?
				isTalking = true;
				hovered.Select(this);
			}
			else
			{
				hovered.Hover();
				hilighted = hovered;
			}
		}

	}

	public void Deselect(){
		isTalking = false;
		hilighted.Deselect();
	}

}
