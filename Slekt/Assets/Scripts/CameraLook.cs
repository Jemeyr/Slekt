using UnityEngine;
using System.Collections;

[AddComponentMenu("PlayerMovement/CameraLook")]
public class CameraLook : MonoBehaviour {

	public GameObject player;

	private Move move;

	public float faceFactor;

	public float distance;
	public float height;

	public float rotationDamping;
	public float heightDamping;

	void Awake () {
		move  = player.GetComponent<Move>();
	}
	
	void Update () {
		if(!player){
			return;
		}

		Vector3 facing = move.GetFacing();

		//float rot = Mathf.Lerp(transform.eulerAngles.y, player.transform.eulerAngles.y, Time.deltaTime * rotationDamping);
		//float h = Mathf.Lerp(transform.position.y, player.transform.position.y, Time.deltaTime * heightDamping);
		float targetRotation = player.transform.eulerAngles.y;
		float targetHeight = height - facing.y;



		float currRotation = Mathf.LerpAngle(transform.eulerAngles.y, targetRotation, rotationDamping);
		float currHeight = Mathf.Lerp(transform.position.y - player.transform.position.y, targetHeight, heightDamping);


		Quaternion rotate = Quaternion.Euler(0, currRotation, 0);


		transform.position = player.transform.position + distance * (rotate * -Vector3.forward);
		transform.position += currHeight * Vector3.up - faceFactor * facing;

		transform.LookAt(player.transform.position + faceFactor * facing);

	}
}
