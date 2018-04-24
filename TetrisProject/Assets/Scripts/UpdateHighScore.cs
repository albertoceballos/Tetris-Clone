using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateHighScore : MonoBehaviour {

	// Use this for initialization
	void Start () {
        getHighScore();
    }

    void getHighScore()
    {
        var textUIComp = GameObject.Find("HighScore").GetComponent<Text>();
        textUIComp.text = PlayerPrefs.GetInt("HighScore").ToString();
        //Debug.Log("HighScore=" + textUIComp.text);
    }
}
