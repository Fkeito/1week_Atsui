using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    private GameObject player;
    public Text score;
    private float height;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (height < player.transform.position.y)
        {
            height = player.transform.position.y;
            score.text = (int)((height - 0.68) * 1000) / 100f + " m";
        }
	}

    public GameObject scoreCanvas;
    public void ShowCanvas(bool visible){
        scoreCanvas.SetActive(visible);
    }
}
