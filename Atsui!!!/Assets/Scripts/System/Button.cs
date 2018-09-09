using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour {

    public GameObject egg;
    public GameObject canvas;

    public void OnClick1(){
        egg.AddComponent<Rigidbody>();
        canvas.SetActive(false);
    }
    public void OnClick2(){
        SceneManager.LoadScene("Demo");
    }
	
}
