using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour {

    public GameObject pokemon;
    public string pokeName;

	// Use this for initialization
	void Start () {
        pokeName = GetComponent<PokemonHoverOver>().getName();

		
	}
	
	// Update is called once per frame
	void Update () {
        if(pokemon.name.Equals(pokeName))
        {
            DontDestroyOnLoad(pokemon);
        }

		
	}
}
