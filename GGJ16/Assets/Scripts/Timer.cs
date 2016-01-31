using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {

    public Text timer;
    public float minutes = 5f;
    private float seconds = 0f;
    private float writeTimer = 1.0f;

	// Use this for initialization
	void Start () {
        WriteTime();
	}
	
	// Update is called once per frame
	void Update () {
        if(minutes >= 0)
        {
            seconds -= Time.deltaTime;
            writeTimer += Time.deltaTime;
            if (seconds < 0)
            {
                minutes -= 1;
                seconds = 59;
            }
            if (writeTimer > 1.0)
            {
                WriteTime();
                writeTimer = 1.0f;
            }
        }
    }

    void WriteTime()
    {
        //Debug.Log("Timer Update!");
        string secondsFormatted = seconds.ToString();
        string timerText = minutes + ":";
        if (seconds < 10) { timerText += "0"; }
        timerText = timerText + secondsFormatted.Split('.')[0];
        timer.text = timerText;
    }
}
