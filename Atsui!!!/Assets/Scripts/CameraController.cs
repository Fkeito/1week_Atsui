using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private GameObject player;
    private PlayerController pc;
    public float xMin, xMax;
    public float yMin;
    private Vector3 offset;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
        pc = player.GetComponent<PlayerController>();
        offset = this.transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 tmp = player.transform.position + offset;
        tmp.x = Mathf.Clamp(tmp.x, xMin, xMax);
            
        if (tmp.y > yMin) if (tmp.y < this.transform.position.y)
        {
            if (pc != null) if (pc.cheatMode) return;
            Destroy(this);
        }

        this.transform.position = tmp;
	}
}
