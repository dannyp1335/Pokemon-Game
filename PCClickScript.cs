using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCClickScript : MonoBehaviour {

    public GameObject FPSController;

	// Use this for initialization
	void Start () {
        FPSController = GameObject.FindWithTag("FPS");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        FPSController.GetComponent<PCManager>().showPCCanvas();
    }
}
