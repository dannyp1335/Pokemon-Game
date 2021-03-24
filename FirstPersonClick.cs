using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonClick : MonoBehaviour
{

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray toMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rhInfo;
            bool didHit = Physics.Raycast(toMouse, out rhInfo, 500.0f);
            if (didHit)
            {
                //Debug.Log(rhInfo.collider.name);
                PokemonHoverOver displayScript = rhInfo.collider.GetComponent<PokemonHoverOver>();

                if (displayScript)
                {
                    displayScript.doFadeText();
                }


            }



        }

    }
}
