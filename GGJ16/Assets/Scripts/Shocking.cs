using UnityEngine;
using System.Collections;

public class Shocking : MonoBehaviour {
	public GameObject target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AttackTarget(GameObject target){
		this.target = target;
		AttackTarget ();
	}

	public void AttackTarget(){
		
	}
}
