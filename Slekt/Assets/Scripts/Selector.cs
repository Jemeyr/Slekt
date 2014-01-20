using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Selector : MonoBehaviour {

	public float TargetRange = 5.0f;
	public float MinimumValue = 0.0f;
	
	private GameObject[] targets;
	private GameObject hilighted;

	public bool isTalking = false;

	private Move move;

	void Awake () {
		targets = GameObject.FindGameObjectsWithTag("Target");

		move = transform.GetComponentInChildren<Move>();

		Debug.Log (move);

		Debug.Log (targets.Length);
	}
	
	void Update () {
		//using a dictionary because unity doesn't support tuples yet, also it looks cleaner
		var nearTargets = new Dictionary<GameObject, float>();
		GameObject hovered = null;
		

		//create a dict of close targets
		foreach(var targ in targets){
			float dist = Vector3.Distance (targ.transform.position, transform.position);
			if(dist < TargetRange){
				nearTargets.Add(targ, dist);
				Debug.DrawLine(transform.position,targ.transform.position, Color.blue);
			}
		}
		
		float nearest = float.MaxValue;

		//TODO: replace this with a weighted value of val and closeness?

		Debug.DrawLine(transform.position, transform.position + move.GetFacing() * TargetRange ,Color.red);
		
		//find closest target
		if(nearTargets.Count > 0){

			foreach(var targ in nearTargets){
				Vector3 dir = Vector3.Normalize(targ.Key.transform.position - transform.position);
				//float testVal = Vector3.Dot(dir, transform.forward);
				float testVal = Vector3.Dot(dir, move.GetFacing());

				if (testVal > MinimumValue && targ.Value < nearest){
					nearest = targ.Value;
					hovered = targ.Key;
				}
			}	
		}

		//lose track of hilighted target
		if(hilighted != null && hovered != hilighted){
			Target t = hilighted.GetComponent<Target>();
			
			if(t != null){
				t.Unhover();
			}
		}

		//hover or activate a target
		if(hovered != null){
			Target t = hovered.GetComponent<Target>();
			
			if(t != null){
				if(Input.GetMouseButtonDown(0)){
					//isTalking should be something else I think. Maybe stick it in a player state and give the
					// selector ability to flip it at will?
					isTalking = true;
					t.Select(this);
				}
				else
				{
					t.Hover();
					hilighted = hovered;
				}
			}
		}

	}

	public void Deselect(){
		isTalking = false;
		hilighted.GetComponent<Target>().Deselect();
	}

}
