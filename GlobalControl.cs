using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalControl : MonoBehaviour
{

    public static GlobalControl Instance;

    //public GameObject Trainer;
    public GameObject FPSController;
    //public GameObject Healer;
    public GameObject charmander;
    public GameObject bulbasaur;
    public GameObject squirtle;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            // DontDestroyOnLoad(Trainer);
            DontDestroyOnLoad(FPSController);
            FPSController.SetActive(false);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            Destroy(FPSController);
        }
    }

    public void loadScene(int starter)
    {
        Vector3 spawnPosition = new Vector3(0, 1, 0);

        if (starter == 0)
        {
            FPSController.GetComponent<PokemonManager>().setPokemonNumber(0, bulbasaur);
            bulbasaur.SetActive(false);
            DontDestroyOnLoad(bulbasaur);
        }
        else if (starter == 1)
        {
            FPSController.GetComponent<PokemonManager>().setPokemonNumber(0, charmander);
            charmander.SetActive(false);
            DontDestroyOnLoad(charmander);
        }
        else if (starter == 2)
        {
            FPSController.GetComponent<PokemonManager>().setPokemonNumber(0, squirtle);
            squirtle.SetActive(false);
            DontDestroyOnLoad(squirtle);
        }
        //SceneManager.LoadScene("Scene");
        FPSController.SetActive(true);
    }

}


