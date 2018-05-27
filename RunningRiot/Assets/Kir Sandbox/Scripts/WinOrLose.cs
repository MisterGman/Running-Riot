using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinOrLose : MonoBehaviour {
    public Text win;
    public Text lose;
    public LeftCursorHandler lch;

	// Use this for initialization
	void Start () {
        win.enabled = false;
        lose.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(lch.countwin == 3)
        {
            Time.timeScale = 0.0f;
            win.enabled = true;
        }
        else if (lch.countlose == 3)
        {
            Time.timeScale = 0.0f;
            lose.enabled = true;
        }
	}
}
