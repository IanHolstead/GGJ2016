using UnityEngine;
using System.Collections;

public class Crusher : Trap {

    private bool useable = true;
    public bool use = false;
    public float shakeTime = 0.5f;
    private float timeShaking = 0.0f;
    private bool crushing = false;
    private bool raising = false;
    public float crushHeight = 4.0f;
    public float crushLower = 0.5f;
    public float crushRaise = 0.1f;
    public float shakeAmnt = 0.1f;
    private Vector3 initialPos;
    private AudioSource source;
    public AudioClip squish;
    private BoxCollider2D killCollider;

	// Use this for initialization
	void Start () {
        initialPos = gameObject.transform.position;
        source = GetComponent<AudioSource>();
        BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].enabled == false) killCollider = colliders[i];
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (use == true && useable == true)
        {
            Activate(); // Should start the process, but not be the whole process
        }
        CrusherAction();
    }

    void OnTriggerEnter2D(Collider2D otherObj)
    {
        if (otherObj.gameObject.tag == "Player")
        {
            source.PlayOneShot(squish, 1F);
            otherObj.GetComponent<PlayerMotion>().Die();
        }
    }

    void Shake()
    {
        gameObject.transform.position = new Vector3(Random.Range(-shakeAmnt, shakeAmnt) + initialPos.x, Random.Range(-shakeAmnt, shakeAmnt) + initialPos.y, 0);
    }

    void Crush()
    {
        while(initialPos.y - gameObject.transform.position.y < crushHeight)
        {
            gameObject.transform.position -= new Vector3(0, crushLower, 0);
        }
        while(gameObject.transform.position.y < initialPos.y)
        {
            gameObject.transform.position += new Vector3(0, crushRaise, 0);
        }
        gameObject.transform.position = initialPos;
        useable = true;
    }

    override public void Activate()
    {
        killCollider.enabled = true;
        useable = false;
        timeShaking += Time.deltaTime;
    }

    void CrusherAction()
    {
        if (timeShaking > 0 && timeShaking < shakeTime)
        {
            Shake();
            timeShaking += Time.deltaTime;
            crushing = true;
        }
        else if (timeShaking > 0 && timeShaking >= shakeTime)
        {
            gameObject.transform.position = initialPos;
            timeShaking = 0.0f;
        }
        if (crushing == true)
        {
            gameObject.transform.position -= new Vector3(0, crushLower, 0);
            if (initialPos.y - gameObject.transform.position.y >= crushHeight)
            {
                gameObject.transform.position = initialPos - new Vector3(0, crushHeight, 0);
                crushing = false;
                raising = true;
                killCollider.enabled = false;
            }
        }
        if (raising == true)
        {
            gameObject.transform.position += new Vector3(0, crushRaise, 0);
            if (initialPos.y - gameObject.transform.position.y <= 0)
            {
                gameObject.transform.position = initialPos;
                raising = false;
                useable = true;
            }
        }
    }

}
