using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour {

    private Rigidbody rb;
    private GameObject smoke;

	// Use this for initialization
	void Start () {
        StartCoroutine(DelayMethod(() => Jump()));
        Destroy(smoke.gameObject, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetProperty(Rigidbody rb, GameObject smoke){
        this.rb = rb;
        this.smoke = smoke;
    }
    private void Jump()
    {
        rb.AddForce(Vector3.up * 20, ForceMode.Impulse);
    }

    private IEnumerator DelayMethod(System.Action action, float delayTime = 1f){
        yield return new WaitForSeconds(delayTime);
        action();
    }
}
