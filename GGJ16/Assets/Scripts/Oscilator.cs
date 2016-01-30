using UnityEngine;
using System.Collections;

public class Oscilator : MonoBehaviour {

    public bool shouldHover = true;

    float initialHeight;
    public float floatFrequency = 1f;
    public float floatHeight = 1f;
    float occilator = 0;

    // Use this for initialization
    void Start () {
        initialHeight = transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
        if (shouldHover)
        {
            hover();
        }
        
	}

    void hover()
    {
        occilator += Time.deltaTime;
        float height = initialHeight + floatHeight + floatHeight * Mathf.Sin(occilator * floatFrequency);
        transform.position = new Vector3(transform.position.x, height);
    }
}
