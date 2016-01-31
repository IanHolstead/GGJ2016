using UnityEngine;
using System.Collections;

public class Shocking : MonoBehaviour {
	public GameObject[] players;
	public GameObject cameraFollow;
	private Transform[] children = new Transform[4];
	private SpriteRenderer[] childrenSprite = new SpriteRenderer[4];
	private Animator[] childrenAnimator = new Animator[4];
	private PlayerMotion[] playerControllers = new PlayerMotion[4];
	private PlayerMediam cameraFollowScript;
	private AudioSource zap;

	// Use this for initialization
	void Start () {
		for (int x = 0; x < 4; x++) {
			children [x] = gameObject.transform.GetChild (x);
			childrenSprite [x] = children [x].GetComponent<SpriteRenderer> ();
			childrenAnimator [x] = children [x].GetComponent<Animator> ();
			playerControllers [x] = players [x].GetComponent<PlayerMotion> ();
		}
		cameraFollowScript = cameraFollow.GetComponent<PlayerMediam> ();
		zap = GetComponent<AudioSource> ();
	}

	void Update(){
		for (int x = 0; x < 4; x++) {
			if (!zap.isPlaying || childrenAnimator [x].GetCurrentAnimatorStateInfo (0).normalizedTime > 1f) {
				childrenSprite [x].enabled = false;
				childrenAnimator [x].enabled = false;
				cameraFollowScript.UnFollowPlayer ();
			}
		}
	}

	public void AttackTarget(int target){
		children [target].position = players[target].transform.position;
		childrenSprite [target].enabled = true;
		childrenAnimator [target].enabled = true;
		playerControllers [target].Shock (1);
		zap.Play ();
	}

	public void AttackAll(){
		for (int x = 0; x < 4; x++) {
			children [x].position = players[x].transform.position;
			childrenSprite [x].enabled = true;
			childrenAnimator [x].enabled = true;
			playerControllers [x].Shock (4);
		}
		zap.Play ();
	}
}
