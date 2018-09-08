using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour {

    private Rigidbody rb;
    private GameObject smoke;
    private GameObject kimi;

    private float time;
    private float jumpTime = 18f;

	// Use this for initialization
	void Start () {
        StartCoroutine(DelayMethod(()=>Rotate()));
        Destroy(smoke.gameObject, 1f);
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;

        if(time > 1f){
            rb.velocity = Vector3.up * 10f;
            if (time > jumpTime - 1f) Destroy(this);
        }
	}

    public void SetProperty(Rigidbody rb, GameObject smoke, GameObject kimi){
        this.rb = rb;
        this.smoke = smoke;
        this.kimi = kimi;
    }
    private void Rotate()
    {
        Debug.Log("a");
        rb.AddTorque(Vector3.right * 5f * kimi.transform.localPosition.x, ForceMode.Impulse);
    }

    private IEnumerator DelayMethod(System.Action action, float delayTime = 1f){
        yield return new WaitForSeconds(delayTime);
        action();
    }
}
