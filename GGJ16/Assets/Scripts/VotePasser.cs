using UnityEngine;
using System.Collections;

public class VotePasser : MonoBehaviour {
	public GameObject[] players;
	private PlayerMotion[] playerCtrls = new PlayerMotion[4];

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 4; i++) {
			playerCtrls [i] = players [i].GetComponent<PlayerMotion> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void giveVoteToo(int player){
		playerCtrls [player].givenPatition ();
	}
}
