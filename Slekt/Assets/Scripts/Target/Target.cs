using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Target : MonoBehaviour {


	protected bool isSelected = false;
	protected Selector selector;


	public virtual void Select(Selector s){
		Debug.LogError("Please create a subclass of Target");
		isSelected = true;
		selector = s;
	}

	public virtual void Deselect(){
		Debug.LogError("Please create a subclass of Target");
		isSelected = false;
		selector = null;
	}

	public virtual void Hover(){
		Debug.LogError("Please create a subclass of Target");
	}


	public virtual void Unhover(){
		Debug.LogError("Please create a subclass of Target");
	}



}




