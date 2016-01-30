﻿using UnityEngine;
using System.Collections;
public class Player : MonoBehaviour
{
	private bool isGrounded;
	static private float verticalStop = 0.01f;
	private float vel;
	public Vector3 colour;
	private bool isAtLevelEnd;
	public string levelEndName;
	private int clientID;

	[RPC]
	void updateClientID(int id){
		clientID = id;
		Debug.Log (id + " now knows who it is");
	}

	void Start(){
		isGrounded = true;
		Screen.lockCursor = true;
		if (GetComponent<NetworkView> ().isMine) {
			GetComponentInChildren<Camera> ().enabled = true;
		} else {
			GetComponentInChildren<Camera> ().enabled = false;
		}
		isAtLevelEnd = false;
	}

	void Update()
	{
		if (GetComponent<NetworkView>().isMine){
			ChangeColorTo (colour);
			vel = Mathf.Abs(GetComponent<Rigidbody>().velocity.y);
		}
		if (Input.GetKey(KeyCode.Escape))
			Screen.lockCursor = false;
		else
			Screen.lockCursor = true;
		if(Input.GetKeyDown ("f")){
			GetComponent<AudioSource> ().Play ();
		}
		if(Input.GetKeyDown ("t")){
			Application.LoadLevel(1);
		}

	}

	void FixedUpdate(){
		if (GetComponent<NetworkView> ().isMine) {
			InputMovement();
			InputCamera();
		}
	}
	
	[RPC] void ChangeColorTo(Vector3 color)
	{
		GetComponent<Renderer>().material.color = new Color(color.x, color.y, color.z, 1f);
		
		if (GetComponent<NetworkView>().isMine)
			GetComponent<NetworkView>().RPC("ChangeColorTo", RPCMode.OthersBuffered, color);
	}
	
	void InputMovement()
	{
		float hor = Input.GetAxis ("Horizontal") * 1200;
		float vir = Input.GetAxis ("Vertical") * 1200;
		float jump = 0f;
		if (isGrounded && vel < verticalStop) {
			jump = Input.GetAxis ("Jump") * 15000;
		} else {
			hor = hor / 4;
			vir = vir / 4;
		}
		GetComponent<Rigidbody> ().AddRelativeForce (new Vector3 (hor, jump, vir) * Time.deltaTime);
	}

	void OnCollisionEnter(Collision c){
		Debug.Log ("We are colliding");
		Debug.Log (c.collider.ToString());
		if (c.collider.ToString().Contains(levelEndName) & !isAtLevelEnd) {
			isAtLevelEnd = true;
			if (colour.Equals (new Color (0, 0, 1))) {
				Debug.Log ("Blue is now at the end of the level");
			} else if (colour.Equals (new Color (0, 1, 0))) {
				Debug.Log ("Green is now at the end of the level");
			} else {
				Debug.Log ("Red is now at the end of the level");
			}
			//send to the server the fact that we are now cloided with the level end and who we are
		}
	}

	void OnCollisionStay(Collision c){
		isGrounded = true;
	}
	
	void OnCollisionExit(Collision c){
		isGrounded = false;
	}

	void InputCamera(){
		float yaw = Input.GetAxis ("Mouse X") * 200 * Time.deltaTime;
		float pit = Input.GetAxis ("Mouse Y") * -75 * Time.deltaTime;
		GetComponent<Rigidbody> ().rotation = Quaternion.Euler (0, yaw, 0) * GetComponent<Rigidbody> ().rotation;
		GetComponentInChildren<Camera> ().transform.rotation = GetComponentInChildren<Camera> ().transform.rotation * Quaternion.Euler (pit, 0f, 0f);
	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		Vector3 syncPosition = Vector3.zero;
		if (stream.isWriting)
		{
			syncPosition = GetComponent<Rigidbody>().position;
			stream.Serialize(ref syncPosition);
		}
		else
		{
			stream.Serialize(ref syncPosition);
			GetComponent<Rigidbody>().position = syncPosition;
		}
	}
}