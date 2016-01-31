using UnityEngine;
using System.Collections;

public class Spikes : MonoBehaviour {

    private bool useable = true;
    public bool use = false;
    private bool poking = false;
    private bool receding = false;
    public float pokeTime = 0.5f;
    private float timePoked = 0.0f;
    public float pokeHeight = 2.0f;
    public float speed = 0.1f;
    private Vector3 initialPos;

    // Use this for initialization
    void Start () {
        initialPos = gameObject.transform.position;
	}

    void OnTriggerEnter2D(Collider2D otherObj)
    {
        if (otherObj.gameObject.tag == "Player")
        {
            otherObj.GetComponent<PlayerMotion>().Die();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(use == true && useable == true)
        {
            Use(); // Should start process, but not BE the process 
        }
        SpikeAction();
	}

    void Use()
    {
        useable = false;
        poking = true;
    }

    void SpikeAction()
    {
        if (useable == false && poking == true)
        {
            gameObject.transform.position += new Vector3(0, speed, 0);
            if (gameObject.transform.position.y - initialPos.y >= pokeHeight)
            {
                gameObject.transform.position = initialPos + new Vector3(0, pokeHeight, 0);
                poking = false;
            }
        }
        else if (useable == false && receding == true)
        {
            gameObject.transform.position -= new Vector3(0, speed, 0);
            if (gameObject.transform.position.y <= initialPos.y)
            {
                gameObject.transform.position = initialPos;
                receding = false;
                useable = true;
            }
        }
        else if (useable == false) // Waiting to recede
        {
            timePoked += Time.deltaTime;
            if (timePoked >= pokeTime)
            {
                receding = true;
                timePoked = 0.0f;
            }
        }
    }
}
