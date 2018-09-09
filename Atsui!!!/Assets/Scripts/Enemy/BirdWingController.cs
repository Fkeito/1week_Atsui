using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdWingController : MonoBehaviour {

    public float flapSpeed = 1f;

	// Use this for initialization
	void Start () {
        StartCoroutine(Flap());
	}
	
    private IEnumerator Flap(){

        Vector3 startScale = this.transform.localScale;
        Vector3 startPosition = this.transform.localPosition;

        startScale.y = startPosition.y = 0;

        for (float rad = 0; ;rad += flapSpeed * Time.deltaTime){
            float cos = Mathf.Cos(rad);
            this.transform.localScale = startScale + cos * Vector3.up;
            this.transform.localPosition = startPosition + ((cos + 1) * 0.55f - 0.1f) * Vector3.up;

            yield return null;
        }
    }
}
