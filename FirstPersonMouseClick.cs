using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMouseClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
    private GameObject[] trainers;
	void Update () {
        if((Input.GetKeyDown(KeyCode.Escape)))
        {
            trainers = GameObject.FindGameObjectsWithTag("Trainer");
            for (int i = 0; i > trainers.Length; i++)
            {
                trainers[i].SetActive(false);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray toMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rhInfo;
            bool didHit = Physics.Raycast(toMouse, out rhInfo, 500.0f);
            if (didHit)
            {
                //Debug.Log(rhInfo.collider.name);
                PokemonHoverOver displayScript = rhInfo.collider.GetComponent<PokemonHoverOver>();
                HealerCode healScript = rhInfo.collider.GetComponent<HealerCode>();
                
                TrainerScript coolDown = rhInfo.collider.GetComponent<TrainerScript>();

                if (displayScript)
                {
                    displayScript.doFadeText();
                }
                if (healScript)
                {
                    healScript.showDisplay();
                }
                if (coolDown)
                {
                    coolDown.trainerCoolDown();
                }
            }
            else
            {
                Debug.Log("Clicked on empty space");
            }


        }

		
	}
}
