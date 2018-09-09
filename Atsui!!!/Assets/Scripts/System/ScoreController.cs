using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    private GameObject player;
    private PlayerController pc;
    public Text score;
    private float height;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
        pc = player.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (height < player.transform.position.y)
        {
            height = player.transform.position.y;
        }
        else if (pc) if(pc.cheatMode){
            height = player.transform.position.y;
        }
        //Debug.Log(height);
        string s = "";

        //ここきもい
        if (height < 0.1f) s += "0.00 m";
        else if (height < 10) s += (height * 10).ToString(".00") + " m";
        else if (height < 30) s += (45 * (height - 10) + 100).ToString(".00") + " m";
        else if (height < 70) s += ((height - 30) / 4f).ToString("0.00") + " km";
        else if (height < 100) s += (3 * (height - 70) + 10).ToString(".00") + " km";
        else if (height < 130) s += (10 * (height - 100) + 100).ToString(".00") + " km";
        else s += ((height - 130) + 400).ToString(".00") + " km";
        score.text = s;
	}

    public GameObject scoreCanvas;
    public void ShowCanvas(bool visible){
        scoreCanvas.SetActive(visible);
        height = 0;
        score.text = "0.00 m";
    }
}
