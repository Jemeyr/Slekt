﻿using UnityEngine;
using System.Collections;

public class Door : Target {

	public Material standardMaterial;
	public Material hoveredMaterial;



	private bool isMoving = false;
	private bool opening = true;

	public float moveHeight = 4.0f;
	private float targetHeight;

	void Awake(){
		renderer.material = standardMaterial;
	}

	void Update(){
		if(!isMoving){
			return;
		}


		//move and stop when at target
		if(opening){
			rigidbody.MovePosition(transform.position + 0.05f * Vector3.up);

			if(transform.position.y >= targetHeight){
				isMoving = false;
				opening = false;
			}
		}
		else
		{
			rigidbody.MovePosition(transform.position + 0.05f * Vector3.down); 
			
			if(transform.position.y <= targetHeight){
				isMoving = false;
				opening = true;
			}
		}

	}


	public override void Select(Selector s){

		//only trigger if not moving
		if(isMoving != true){
			isMoving = true;

			if(opening){
				targetHeight = transform.position.y + moveHeight;
			}
			else{
				targetHeight = transform.position.y - moveHeight;
			}
		}

		//deselect once triggered
		s.Deselect();

	}

	public override void Hover(){
		renderer.material = hoveredMaterial;	
	}
	
	public override void Unhover(){
		renderer.material = standardMaterial;	
	}
}
