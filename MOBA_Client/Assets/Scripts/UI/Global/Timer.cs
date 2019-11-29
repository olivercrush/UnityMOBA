using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private int seconds;
    private int minutes;

	// Use this for initialization
	void Start () {
        seconds = 0;
        minutes = 0;
        InvokeRepeating("AddSecond", 0f, 1f);
	}
	
    private void AddSecond()
    {
        if (seconds + 1 == 60)
        {
            seconds = 0;
            minutes++;
        }
        else
        {
            seconds++;
        }

        UpdateUIText();
    }

    private void UpdateUIText()
    {
        string text = minutes.ToString();

        if (minutes < 10)
        {
            text = "0" + minutes;
        }

        if (seconds < 10)
        {
            text += ":0" + seconds;
        }
        else
        {
            text += ":" + seconds;
        }

        GetComponent<Text>().text = text;
    }
}
