using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour {

    public PlayerController pc;
    public GameObject kimi;
    public GameObject shiromi;
    public GameObject egg;

    void OnCollisionEnter(Collision other)
    {
        pc.enabled = true;
        kimi.SetActive(true);
        shiromi.SetActive(true);
        egg.SetActive(true);
        Destroy(this.gameObject);
    }
}
