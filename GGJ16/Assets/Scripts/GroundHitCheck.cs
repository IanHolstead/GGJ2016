using UnityEngine;
using System.Collections;

public class GroundHitCheck : MonoBehaviour {

	bool isGrounded = false;

	public Transform GroundCheck1;
	public Transform GroundCheck2;

	public LayerMask ground_layers;

	void FixedUpdate(){
		isGrounded = Physics2D.OverlapArea (GroundCheck1.position, GroundCheck2.position, ground_layers);

		Debug.Log ("Grounded: " + isGrounded);
	}
}