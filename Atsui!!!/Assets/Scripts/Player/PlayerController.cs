using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public bool cheatMode;

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
        if(cheatMode) manager.GetComponent<ScoreController>().ShowCanvas(true);

        rb = this.GetComponent<Rigidbody>();
        _smoke1 = smoke1.emission;
        _smoke2 = smoke2.emission;
        jc = this.GetComponent<JumpController>();
	}

    private float input;
	// Update is called once per frame
    void Update () {
        input = Input.GetAxis("Horizontal");

        if (!cheatMode)
        {
            isBaked = false;
            CheckSun();
            CheckHotSpot();
        }
        else{
            if (Input.GetKey(KeyCode.E)) BakeEgg(true);
            else if (Input.GetKey(KeyCode.Q)) BakeEgg(false);

            if (Input.GetKeyDown(KeyCode.LeftShift)) rb.isKinematic = true;
            if (Input.GetKey(KeyCode.LeftShift)) this.transform.position += 3f * Vector3.up * Time.deltaTime;
            if (Input.GetKeyUp(KeyCode.LeftShift)) rb.isKinematic = false;

            if(Input.GetKeyDown(KeyCode.J)){
                jc.enabled = true;
                jc.SetProperty(rb, smoke, kimi);
                manager.GetComponent<ScoreController>().ShowCanvas(true);
                Destroy(this);
            }

            if(Input.GetKeyDown(KeyCode.I)){
                jc.GetItem();
            }
        }

        if(jump){
            if(Input.GetKeyDown(KeyCode.UpArrow)){
                if(Physics.Raycast(this.transform.position, Vector3.down, 1f)){
                    rb.AddForce(Vector3.up * 6, ForceMode.Impulse);
                    Debug.Log("jump");
                }
            }
        }
	}
    void LateUpdate()
    {
        if(!cheatMode) BakeEgg();
    }
    void FixedUpdate() {
        if (!cheatMode)
        {
            if (baked < 1f) Move(input);
            else
            {
                jc.enabled = true;
                jc.SetProperty(rb, smoke, kimi);
                manager.GetComponent<ScoreController>().ShowCanvas(true);
                Destroy(this);
            }
        }
        else{
            Move(input);
        }
    }

    private float kimiNormPos = 0f;
    private float kimiDistrict = 0.12f;
    private void Move(float inputX) {
        if (System.Math.Abs(inputX) < EPSILON) return;

        float bakedDistrict = -baked * baked + 1;
        float move = inputX * speed * Time.deltaTime * bakedDistrict;

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

        rb.position += move * Vector3.right;
    }

    private bool isBaked;
    [HideInInspector]
    public bool isBakedByHotSpot;
    private float bakeSpeed = 0.05f;
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
    private void BakeEgg(bool plus)
    {
        if (!plus)
        {
            if (smoke.activeSelf) smoke.SetActive(false);
            baked -= 0.1f * Time.deltaTime;
            return;
        }

        if (!smoke.activeSelf) smoke.SetActive(true);
        if (baked >= 1) return;

        baked += 0.1f * Time.deltaTime;
        SetBaked();
        if (System.Math.Abs(baked - 0.5f) < 1E-2f)
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

    private IEnumerator DelayMethod(System.Action action, float delayTime = 1f){
        yield return new WaitForSeconds(delayTime);
        action();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Item")){
            jc.GetItem(3);
            Destroy(other.gameObject);
        }
    }
}
