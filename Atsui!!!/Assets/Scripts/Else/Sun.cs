using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {

    private Quaternion rot;

	// Use this for initialization
	void Start () {
        rot = Quaternion.identity;
	}
	
	// Update is called once per frame
	void Update () {
        rot.eulerAngles += 10 * Vector3.forward * Time.deltaTime;
        this.transform.rotation = rot;
	}
}
