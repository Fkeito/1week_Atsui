using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private GameObject manager;

    private Rigidbody rb;
    public float speed = 3f;

    public GameObject kimi;
    public GameObject smoke;

    public SpriteRenderer shiromi1;
    public SpriteRenderer shiromi2;
    public SpriteRenderer kimi1;
    public SpriteRenderer kimi2;
    public ParticleSystem smoke1;
    public ParticleSystem smoke2;
    private ParticleSystem.EmissionModule _smoke1;
    private ParticleSystem.EmissionModule _smoke2;
    private JumpController jc;
    private bool jump;

    [Range(0f, 1f)]
    public float baked;

    private const float EPSILON = 1E-6f;

	// Use this for initialization
	void Start () {
        manager = GameObject.Find("IroIroManager");

        rb = this.GetComponent<Rigidbody>();
        _smoke1 = smoke1.emission;
        _smoke2 = smoke2.emission;
        jc = this.GetComponent<JumpController>();
	}

    private float input;
	// Update is called once per frame
    void Update () {
        input = Input.GetAxis("Horizontal");

        isBaked = false;
        CheckSun();
        CheckHotSpot();

        if(jump){
            if(Input.GetKeyDown(KeyCode.UpArrow)){
                if(Physics.Raycast(this.transform.position, Vector3.down, 1f)){
                    rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
                    Debug.Log("jump");
                }
            }
        }
	}
    void LateUpdate()
    {
        BakeEgg();
    }
    void FixedUpdate() {
        if (baked < 1f) Move(input);
        else
        {
            jc.enabled = true;
            jc.SetProperty(rb, smoke, kimi);
            manager.GetComponent<ScoreController>().ShowCanvas(true);
            Destroy(this);
        }
    }

    private float kimiNormPos = 0f;
    private float kimiDistrict = 0.12f;
    private void Move(float inputX) {
        if (System.Math.Abs(inputX) < EPSILON) return;

        float move = inputX * speed * Time.deltaTime * (1 - baked);

        Debug.Log(kimi.transform.localPosition);
        if(kimi.transform.localPosition.x >= kimiNormPos - kimiDistrict && kimi.transform.localPosition.x >= -0.12f && inputX < 0f){
            Vector3 tmpPos = kimi.transform.localPosition;
            tmpPos.x += move;
            kimi.transform.localPosition = tmpPos;
            return;
        }
        if (kimi.transform.localPosition.x <= kimiNormPos + kimiDistrict && kimi.transform.localPosition.x <= 0.12f && inputX > 0f)
        {
            Vector3 tmpPos = kimi.transform.localPosition;
            tmpPos.x += move;
            kimi.transform.localPosition = tmpPos;
            return;
        }
        /*
        if (kimi.transform.localPosition.x <= kimiNormPos + kimiDistrict && kimi.transform.localPosition.x <= 0.12f && kimi.transform.localPosition.x >= kimiNormPos - kimiDistrict && kimi.transform.localPosition.x >= -0.12f)
        {
            Vector3 tmpPos = kimi.transform.localPosition;
            tmpPos.x += move;
            kimi.transform.localPosition = tmpPos;
            return;
        }
        //*/

        rb.position += move * Vector3.right;
    }

    private bool isBaked;
    [HideInInspector]
    public bool isBakedByHotSpot;
    private float bakeSpeed = 0.08f;
    public void SetBakeSpeed(float speed){
        bakeSpeed = speed;
    }
    private void CheckHotSpot(){
        if (isBakedByHotSpot) isBaked = true;
    }
    private void CheckSun(){
        Ray ray = new Ray(this.transform.position, Vector3.up);
        if (!Physics.Raycast(ray, 8f)) isBaked = true;
    }
    private void BakeEgg(){
        if (!isBaked)
        {
            if(smoke.activeSelf) smoke.SetActive(false);
            return;
        }

        if (!smoke.activeSelf) smoke.SetActive(true);
        if (baked >= 1) return;

        baked += bakeSpeed * Time.deltaTime;
        SetBaked();
        if(System.Math.Abs(baked - 0.5f) < 1E-2f)
        {
            //speed = 0.5f;
            kimiNormPos = kimi.transform.localPosition.x;
            kimiDistrict = 0.07f;
            _smoke1.rateOverTime = _smoke2.rateOverTime = 2f;
            jump = true;
        }
        else if (baked >= 1f)
        {
            //speed = 0f;
            kimiNormPos = kimi.transform.localPosition.x;
            kimiDistrict = 0f;
            _smoke1.rateOverTime = _smoke2.rateOverTime = 5f;
        }
    }
    private void SetBaked() {
        Color c = Color.white;
        c.a = baked;
        shiromi2.color = kimi2.color = c;
    }
}
