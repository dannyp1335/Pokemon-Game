using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PokemonHoverOver : MonoBehaviour
{
    new public string name;
    public bool trainer;
    public Text myText;

    public GameObject runButton;
    public GameObject fightButton;

    public GameObject[] wildPokemon;
    private GameObject playerPokemon;
    private GameObject playerPokemonTwo;
    private GameObject playerPokemonThree;
    private GameObject playerPokemonFour;
    private GameObject playerPokemonFive;
    private GameObject playerPokemonSix;
    public GameObject FPSController;

    private GameObject battleManager;
    void Start()
    {

        runButton.SetActive(false);
        fightButton.SetActive(false);

        FPSController = GameObject.Find("FPSController1");
    }

    IEnumerator FadeText()
    {
        if (FPSController.GetComponent<PokemonManager>().pokemonArray[0] == null || FPSController.GetComponent<PokemonManager>().pokemonArray[0].GetComponent<BasePokemon>().isFainted())
        {
            if (FPSController.GetComponent<PokemonManager>().pokemonArray[1] == null || FPSController.GetComponent<PokemonManager>().pokemonArray[1].GetComponent<BasePokemon>().isFainted())
            {
                if (FPSController.GetComponent<PokemonManager>().pokemonArray[2] == null || FPSController.GetComponent<PokemonManager>().pokemonArray[2].GetComponent<BasePokemon>().isFainted())
                {
                    if (FPSController.GetComponent<PokemonManager>().pokemonArray[3] == null || FPSController.GetComponent<PokemonManager>().pokemonArray[3].GetComponent<BasePokemon>().isFainted())
                    {
                        if (FPSController.GetComponent<PokemonManager>().pokemonArray[4] == null || FPSController.GetComponent<PokemonManager>().pokemonArray[4].GetComponent<BasePokemon>().isFainted())
                        {
                            if (FPSController.GetComponent<PokemonManager>().pokemonArray[5] == null || FPSController.GetComponent<PokemonManager>().pokemonArray[5].GetComponent<BasePokemon>().isFainted())
                            {

                                fightButton.SetActive(false);
                                runButton.SetActive(false);
                                myText.text = "You cannot fight with no Pokemon!";
                                yield return new WaitForSeconds(5f);
                                myText.text = " ";

                            }
                            else
                            {
                                Debug.Log("This pokemon has health!");
                                GetString();
                                runButton.SetActive(true);
                                fightButton.SetActive(true);
                                yield return new WaitForSeconds(5f);
                                runButton.SetActive(false);
                                fightButton.SetActive(false);
                                myText.text = " ";
                            }
                        }
                        else
                        {
                            Debug.Log("This pokemon has health!");
                            GetString();
                            runButton.SetActive(true);
                            fightButton.SetActive(true);
                            yield return new WaitForSeconds(5f);
                            runButton.SetActive(false);
                            fightButton.SetActive(false);
                            myText.text = " ";
                        }
                    }
                    else
                    {
                        Debug.Log("This pokemon has health!");
                        GetString();
                        runButton.SetActive(true);
                        fightButton.SetActive(true);
                        yield return new WaitForSeconds(5f);
                        runButton.SetActive(false);
                        fightButton.SetActive(false);
                        myText.text = " ";
                    }
                }
                else
                {
                    Debug.Log("This pokemon has health!");
                    GetString();
                    runButton.SetActive(true);
                    fightButton.SetActive(true);
                    yield return new WaitForSeconds(5f);
                    runButton.SetActive(false);
                    fightButton.SetActive(false);
                    myText.text = " ";
                }
            }
            else
            {
                Debug.Log("This pokemon has health!");
                GetString();
                runButton.SetActive(true);
                fightButton.SetActive(true);
                yield return new WaitForSeconds(5f);
                runButton.SetActive(false);
                fightButton.SetActive(false);
                myText.text = " ";
            }
        }
        else
        {
            Debug.Log("This pokemon has health!");
            GetString();
            runButton.SetActive(true);
            fightButton.SetActive(true);
            yield return new WaitForSeconds(5f);
            runButton.SetActive(false);
            fightButton.SetActive(false);
            myText.text = " ";
        }


    }

    public void doFadeText()
    {
        StartCoroutine(FadeText());
    }

    public void GetString()
    {
        myText.text = "Name: " + name;
        if (!trainer)
        {
            myText.text = myText.text + "\nLevel: " + GetComponent<BasePokemon>().returnLevel();
        }
    }

    public void buttonClick()
    {
        runButton.SetActive(false);
        fightButton.SetActive(false);

        myText.text = "";
    }

    public void buttonClickTwo()
    {
        myText.text = " ";
    }

    public void fightButtonClick()
    {
        playerPokemon = FPSController.GetComponent<PokemonManager>().returnPokemonNumber(0);
        Vector3 playerPosition = new Vector3(99, 0.5f, 24);
        Vector3 wildPosition = new Vector3(103, 0.5f, 24);
        Vector3 trainerPosition = new Vector3(-3, 0.5f, -5);
        wildPokemon[0].transform.position = wildPosition;



        if (trainer)
        {
            FPSController.GetComponent<PokemonManager>().setFightingTrainer(true);
            FPSController.GetComponent<PokemonManager>().setTrainerName(name);
            for (int i = 0; i < wildPokemon.Length; i++)
            {
                wildPokemon[i].transform.position = wildPosition;
                wildPokemon[i].SetActive(true);
                //DontDestroyOnLoad(wildPokemon[i]);
            }
            //this.transform.position = trainerPosition;
                DontDestroyOnLoad(this);
            
        }
        else if (!trainer)
        {
            GetComponent<AIII>().enabled = false;
            FPSController.GetComponent<PokemonManager>().setFightingTrainer(false);
        }

        if (FPSController.GetComponent<PokemonManager>().returnPokemonNumber(5) != null)
        {
            playerPokemonSix = FPSController.GetComponent<PokemonManager>().returnPokemonNumber(5);
            playerPokemonFive = FPSController.GetComponent<PokemonManager>().returnPokemonNumber(4);
            playerPokemonFour = FPSController.GetComponent<PokemonManager>().returnPokemonNumber(3);
            playerPokemonThree = FPSController.GetComponent<PokemonManager>().returnPokemonNumber(2);
            playerPokemonTwo = FPSController.GetComponent<PokemonManager>().returnPokemonNumber(1);
            playerPokemon = FPSController.GetComponent<PokemonManager>().returnPokemonNumber(0);

            playerPokemon.transform.position = playerPosition;
            playerPokemonTwo.transform.position = playerPosition;
            playerPokemonThree.transform.position = playerPosition;
            playerPokemonFour.transform.position = playerPosition;
            playerPokemonFive.transform.position = playerPosition;
            playerPokemonSix.transform.position = playerPosition;

            playerPokemon.SetActive(true);
            playerPokemonTwo.SetActive(true);
            playerPokemonThree.SetActive(true);
            playerPokemonFour.SetActive(true);
            playerPokemonFive.SetActive(true);
            playerPokemonSix.SetActive(true);

            SceneManager.LoadScene("FightScene");
            DontDestroyOnLoad(playerPokemon);
            DontDestroyOnLoad(playerPokemonTwo);
            DontDestroyOnLoad(playerPokemonThree);
            DontDestroyOnLoad(playerPokemonFour);
            DontDestroyOnLoad(playerPokemonFive);
            DontDestroyOnLoad(playerPokemonSix);

        }
        else if (FPSController.GetComponent<PokemonManager>().returnPokemonNumber(4) != null)
        {
            playerPokemonFive = FPSController.GetComponent<PokemonManager>().returnPokemonNumber(4);
            playerPokemonFour = FPSController.GetComponent<PokemonManager>().returnPokemonNumber(3);
            playerPokemonThree = FPSController.GetComponent<PokemonManager>().returnPokemonNumber(2);
            playerPokemonTwo = FPSController.GetComponent<PokemonManager>().returnPokemonNumber(1);
            playerPokemon = FPSController.GetComponent<PokemonManager>().returnPokemonNumber(0);

            playerPokemon.transform.position = playerPosition;
            playerPokemonTwo.transform.position = playerPosition;
            playerPokemonThree.transform.position = playerPosition;
            playerPokemonFour.transform.position = playerPosition;
            playerPokemonFive.transform.position = playerPosition;

            playerPokemon.SetActive(true);
            playerPokemonTwo.SetActive(true);
            playerPokemonThree.SetActive(true);
            playerPokemonFour.SetActive(true);
            playerPokemonFive.SetActive(true);

            SceneManager.LoadScene("FightScene");
            DontDestroyOnLoad(playerPokemon);
            DontDestroyOnLoad(playerPokemonTwo);
            DontDestroyOnLoad(playerPokemonThree);
            DontDestroyOnLoad(playerPokemonFour);
            DontDestroyOnLoad(playerPokemonFive);
        }
        else if (FPSController.GetComponent<PokemonManager>().returnPokemonNumber(3) != null)
        {
            playerPokemonFour = FPSController.GetComponent<PokemonManager>().returnPokemonNumber(3);
            playerPokemonThree = FPSController.GetComponent<PokemonManager>().returnPokemonNumber(2);
            playerPokemonTwo = FPSController.GetComponent<PokemonManager>().returnPokemonNumber(1);
            playerPokemon = FPSController.GetComponent<PokemonManager>().returnPokemonNumber(0);

            playerPokemon.transform.position = playerPosition;
            playerPokemonTwo.transform.position = playerPosition;
            playerPokemonThree.transform.position = playerPosition;
            playerPokemonFour.transform.position = playerPosition;

            playerPokemon.SetActive(true);
            playerPokemonTwo.SetActive(true);
            playerPokemonThree.SetActive(true);
            playerPokemonFour.SetActive(true);

            SceneManager.LoadScene("FightScene");
            DontDestroyOnLoad(playerPokemon);
            DontDestroyOnLoad(playerPokemonTwo);
            DontDestroyOnLoad(playerPokemonThree);
            DontDestroyOnLoad(playerPokemonFour);
        }
        else if (FPSController.GetComponent<PokemonManager>().returnPokemonNumber(2) != null)
        {
            playerPokemonThree = FPSController.GetComponent<PokemonManager>().returnPokemonNumber(2);
            playerPokemonTwo = FPSController.GetComponent<PokemonManager>().returnPokemonNumber(1);
            playerPokemon = FPSController.GetComponent<PokemonManager>().returnPokemonNumber(0);

            playerPokemon.transform.position = playerPosition;
            playerPokemonTwo.transform.position = playerPosition;
            playerPokemonThree.transform.position = playerPosition;

            playerPokemon.SetActive(true);
            playerPokemonTwo.SetActive(true);
            playerPokemonThree.SetActive(true);;

            SceneManager.LoadScene("FightScene");
            DontDestroyOnLoad(playerPokemon);
            DontDestroyOnLoad(playerPokemonTwo);
            DontDestroyOnLoad(playerPokemonThree);
        }
        else if (FPSController.GetComponent<PokemonManager>().returnPokemonNumber(1) != null)
        {
            playerPokemonTwo = FPSController.GetComponent<PokemonManager>().returnPokemonNumber(1);
            playerPokemon = FPSController.GetComponent<PokemonManager>().returnPokemonNumber(0);

            playerPokemon.transform.position = playerPosition;
            playerPokemonTwo.transform.position = playerPosition;

            playerPokemon.SetActive(true);
            playerPokemonTwo.SetActive(true);;

            SceneManager.LoadScene("FightScene");
            DontDestroyOnLoad(playerPokemon);
            DontDestroyOnLoad(playerPokemonTwo);
        }
        else
        {
            playerPokemon.transform.position = playerPosition;
            playerPokemon.SetActive(true);

            SceneManager.LoadScene("FightScene");
            DontDestroyOnLoad(playerPokemon);
        }

        if (!trainer)
        {
            DontDestroyOnLoad(wildPokemon[0].transform);
        }
            myText.text = " ";
            runButton.SetActive(false);
            fightButton.SetActive(false);
    }

    public void ifTrainerLost()
    {
        myText.text = "You can't fight me yet I need to heal.";
        runButton.SetActive(false);
        fightButton.SetActive(false);
    }



    public string getName()
    {
        return name;
    }
}

