using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class PCManager : MonoBehaviour {

    public Transform PCcanvas;
    public Transform Player;
    public Text PCPokemonText;
    public Text PlayerPokemonText;

    public InputField partyPokemonInputField;
    public InputField pcPokemonInputField;
    public InputField releaseInputField;

    public Text displayText;

	void Start () {
        PCcanvas.gameObject.SetActive(false);
	}

    public void returnButtonPressed()
    {
        Time.timeScale = 1;
        PCcanvas.gameObject.SetActive(false);
        Player.GetComponent<FirstPersonController>().enabled = true;
    }

    public void showPCCanvas()
    {
        Time.timeScale = 0;
        Player.GetComponent<FirstPersonController>().enabled = false;
        
        PCcanvas.gameObject.SetActive(true);

        updateTexts();
    }

    public void hidePCCanvas()
    {
        PCcanvas.gameObject.SetActive(false);
    }

    public void swapPokemon()
    {
        int partyNum = int.Parse(partyPokemonInputField.text);
        int pcNum = int.Parse(pcPokemonInputField.text);

        Debug.Log(partyNum + " pcnum: " + pcNum);

        GameObject temp = null;

        if (partyNum == 0 || pcNum == 0 || GetComponent<PokemonManager>().pokemonArray[partyNum - 1] == null || GetComponent<PokemonManager>().extraPokemonArray[pcNum - 1] == null)
        {
            displayText.text = "Please enter valid numbers!";
        }
        else
        {
            temp = GetComponent<PokemonManager>().pokemonArray[partyNum -1];
            GetComponent<PokemonManager>().pokemonArray[partyNum - 1] = GetComponent<PokemonManager>().extraPokemonArray[pcNum - 1];
            GetComponent<PokemonManager>().extraPokemonArray[pcNum - 1] = temp;
            updateTexts();
            displayText.text = "You successfully swapped the Pokemon!";
        }
    }

    public void updateTexts()
    {

        PCPokemonText.text = "PC Pokemon";
        PlayerPokemonText.text = "Party Pokemon";

        for (int i = 0; i < 150; i++)
        {
            if (GetComponent<PokemonManager>().extraPokemonArray[i] != null)
            {
                PCPokemonText.text = PCPokemonText.text + "\n " + (i + 1) + ": " + GetComponent<PokemonManager>().extraPokemonArray[i].GetComponent<BasePokemon>().returnName() + " lvl " + GetComponent<PokemonManager>().extraPokemonArray[i].GetComponent<BasePokemon>().returnLevel();
            }
        }

        for (int i = 0; i < 6; i++)
        {
            if (GetComponent<PokemonManager>().pokemonArray[i] != null)
            {
                PlayerPokemonText.text = PlayerPokemonText.text + "\n" + (i + 1) + ": " + GetComponent<PokemonManager>().pokemonArray[i].GetComponent<BasePokemon>().returnName() + " lvl " + GetComponent<PokemonManager>().pokemonArray[i].GetComponent<BasePokemon>().returnLevel();
            }
        }
    }

    public void releasePokemon()
    {
        int releaseNum = int.Parse(releaseInputField.text);

        for (int i = releaseNum; i < 150; i++)
        {
            GetComponent<PokemonManager>().extraPokemonArray[i - 1] = GetComponent<PokemonManager>().extraPokemonArray[i];
        }

        GetComponent<PokemonManager>().extraPokemonArray[149] = null;

        updateTexts();

    }
}
