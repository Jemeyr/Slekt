using UnityEngine;
using System.Collections;

public class Chat : Target {

	public Material standardMaterial;
	public Material hoveredMaterial;
	public Material selectedMaterial;
	
	public string npcName = "Bob";

	
	void Awake () {
		renderer.material = standardMaterial;	
	}

	public override void Select(Selector s){
		isSelected = true;
		selector = s;

		renderer.material = selectedMaterial;
		Screen.lockCursor = false;
		Screen.showCursor = true;
	}

	public override void Deselect(){
		isSelected = false;
		selector = null;
		
		renderer.material = standardMaterial;
	}


	public override void Hover(){
		if(!isSelected)
			renderer.material = hoveredMaterial;
	}

	public override void Unhover(){
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
			//Trigger the selector to lose selection, causing it to call local deselect. ugh.
			selector.Deselect();
		}
		
	}



}
