using UnityEngine;
using System.Collections;

public class MovingPlatform : Controllable {

    public Vector2 startPos;
    public Vector2 endPos;

    public float cycleTime = 10f;
    public bool active = false;
    public bool returnToStartWhenDeactive = false;

    float age = 0f;
    float alpha = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (active)
        {
            age += Time.deltaTime;
            alpha = .5f - .5f * Mathf.Cos(age * 2 * Mathf.PI / cycleTime);
            UpdatePosition();
        }
        else if (returnToStartWhenDeactive)
        {
            alpha -= Time.deltaTime * 2 / cycleTime;
            age = cycleTime * Mathf.Acos((.5f - alpha) / .5f) / (2 * Mathf.PI);
            if (alpha < 0)
            {
                alpha = 0f;
                age = 0f;
            }
            UpdatePosition();
        }
	}

    void UpdatePosition()
    {
        transform.position = new Vector3(Mathf.Lerp(startPos.x, endPos.x, alpha), Mathf.Lerp(startPos.y, endPos.y, alpha), 0);
    }

    public override void Activate()
    {
        active = true;
    }

    public override void Deactivate()
    {
        active = false;
    }
}
