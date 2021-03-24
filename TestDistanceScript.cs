using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDistanceScript : MonoBehaviour {
    public GameObject player;
    private Transform playerPos;
	// Use this for initialization
	void Start () {
        playerPos = player.transform;
	}
	
	// Update is called once per frame
	void Update () {
        if (playerPos)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                float distance = Vector3.Distance(playerPos.position, transform.position);
                print("distance was " + distance);
            }
        }
	}
}
