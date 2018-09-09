using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gauge : MonoBehaviour {

    public PlayerController pc;
    private UnityEngine.UI.Slider slider;
    public UnityEngine.UI.Image col;

    private Color newColor;

	void Start () {
        slider = this.gameObject.GetComponent<UnityEngine.UI.Slider>();
        newColor = col.color;
	}
	
	void LateUpdate () {
        if (!pc) Destroy(this);
        slider.value = pc.baked;
	}

    public void GaugeColor(float value){
        Debug.Log(value);
        newColor.g = 1f - value;
        col.color = newColor;
    }
}
