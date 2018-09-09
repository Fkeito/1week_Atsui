using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour {

    private Rigidbody rb;
    private GameObject smoke;
    private GameObject kimi;

    private float time;
    [SerializeField, Range(2f, 18f)]
    private float jumpTime = 2f;

	// Use this for initialization
	void Start () {
        StartCoroutine(DelayMethod(()=>Rotate()));
        if(smoke) Destroy(smoke.gameObject, 1f);
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
        //Debug.Log("a");
        rb.AddTorque(Vector3.forward * 5f * kimi.transform.localPosition.x, ForceMode.Impulse);
    }

    public UnityEngine.UI.Image[] icons;
    private int i = 0;
    public void GetItem(float plusTime = 1f){
        jumpTime += plusTime;
        icons[i++].color = Color.yellow;
    }

    private IEnumerator DelayMethod(System.Action action, float delayTime = 1f){
        yield return new WaitForSeconds(delayTime);
        action();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Obstacle")){
            //Debug.Log("a");
            rb.velocity = Vector3.zero;
            Destroy(this);
        }
    }
}
