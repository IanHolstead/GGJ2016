using UnityEngine;
using System.Collections;

public class Shocking : MonoBehaviour {
	public GameObject[] players;
	private Transform[] children = new Transform[4];
	private SpriteRenderer[] childrenSprite = new SpriteRenderer[4];
	private Animator[] childrenAnimator = new Animator[4];
	private AudioSource zap;

	// Use this for initialization
	void Start () {
		for (int x = 0; x < 4; x++) {
			children [x] = gameObject.transform.GetChild (x);
			childrenSprite [x] = children [x].GetComponent<SpriteRenderer> ();
			childrenAnimator [x] = children [x].GetComponent<Animator> ();
		}

		zap = GetComponent<AudioSource> ();
	}

	void Update(){
		if (!zap.isPlaying) {
			for (int x = 0; x < 4; x++) {
				childrenSprite [x].enabled = false;
				childrenAnimator [x].enabled = false;
			}
		}
	}

	public void AttackTarget(int target){
		children [target].position = players[target].transform.position;
		childrenSprite [target].enabled = true;
		childrenAnimator [target].enabled = true;
		zap.Play ();
	}

	public void AttackAll(){
		for (int x = 0; x < 4; x++) {
			children [x].position = players[x].transform.position;
			childrenSprite [x].enabled = true;
			childrenAnimator [x].enabled = true;
		}
		zap.Play ();
	}
}
