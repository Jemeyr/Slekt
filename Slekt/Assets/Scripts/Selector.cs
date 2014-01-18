using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Selector : MonoBehaviour {

	public float TargetRange = 5.0f;
	public float MinimumValue = 0.0f;
	
	private GameObject[] targets;
	private GameObject hilighted;

	public bool isTalking = false;


	void Awake () {
		targets = GameObject.FindGameObjectsWithTag("Target");
	}
	
	void Update () {
		//using a dictionary because unity doesn't support tuples yet, also it looks cleaner
		var nearTargets = new Dictionary<GameObject, float>();
		GameObject hovered = null;
		
		
		foreach(var targ in targets){
			float dist = Vector3.Distance (targ.transform.position, transform.position);
			if(dist < TargetRange){
				nearTargets.Add(targ, dist);
			}
		}
		
		float nearest = float.MaxValue;

		if(nearTargets.Count > 0){

			foreach(var targ in nearTargets){
				Vector3 dir = Vector3.Normalize(targ.Key.transform.position - transform.position);
				float testVal = Vector3.Dot(dir, transform.forward);
				
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
					t.Select(this);
					isTalking = true;
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
