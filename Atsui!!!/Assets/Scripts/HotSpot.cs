using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotSpot : MonoBehaviour {

    public float hotLevel;
    private PlayerController pc;

	// Use this for initialization
	void Start () {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
	}

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player")){
            pc.SetBakeSpeed(hotLevel);
            pc.isBakedByHotSpot = true;
        }
    }
    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pc.SetBakeSpeed(0.08f);
            pc.isBakedByHotSpot = false;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pc.SetBakeSpeed(hotLevel);
            pc.isBakedByHotSpot = true;
        }
    }
    void OnCTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pc.SetBakeSpeed(0.08f);
            pc.isBakedByHotSpot = false;
        }
    }
}
