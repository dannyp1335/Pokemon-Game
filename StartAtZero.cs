using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAtZero : MonoBehaviour {

    public GameObject pokeSpawn;

	// Use this for initialization
	void Start () {
        //pokeSpawn = GetComponent<PokemonHoverOver>().getGameObject();
        Vector3 newPosition = new Vector3(0, 0, 0);
       pokeSpawn.transform.position = newPosition;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
