using UnityEngine;

public class Target : MonoBehaviour {

	//contemplating removing these and just making it an interface
	protected bool isSelected = false;
	protected Selector selector;


	public virtual void Select(Selector s){}

	public virtual void Deselect(){}

	public virtual void Hover(){}
	
	public virtual void Unhover(){}

}




