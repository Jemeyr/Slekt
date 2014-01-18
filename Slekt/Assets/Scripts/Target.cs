using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Target : MonoBehaviour {
	public Material standardMaterial;
	public Material hoveredMaterial;
	public Material selectedMaterial;

	public string npcName = "Bob";

	private bool isSelected = false;

	private Selector selector;


	void Awake () {

		renderer.material = standardMaterial;

	}
	
	public void Activate(Selector s){
		isSelected = true;
		this.selector = s;

		renderer.material = selectedMaterial;
		Screen.lockCursor = false;
		Screen.showCursor = true;
	}

	public void Hover(){
		if(!isSelected)
			renderer.material = hoveredMaterial;
	}

	public void Unhover(){
		if(!isSelected)
			renderer.material = standardMaterial;
	}


	void OnGUI(){
		if (isSelected) {

			var bounds = new Rect(0.0f, Screen.height * 0.8f, Screen.width, Screen.height * 0.2f);
			GUILayout.Window(0, bounds, Dismiss, "NPC: " + npcName);

		}
	}


	void Dismiss(int windowID){
		//GUILayout.Space(10);

		//button
		if (GUILayout.Button("My name is " + npcName, GUILayout.Height(Screen.height*0.15f))){
			renderer.material = standardMaterial;
			isSelected = false;

			//unlock selector and lose connection
			selector.isTalking = false;
			selector = null;
		}
	
	}




}




