using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class IntroductionManager : MonoBehaviour 
{
    public Canvas introCanvas;
    public Canvas pokemonSelectionCanvas;

    public EventSystem eventSystem;

    public Image professorImage;
    public Image introPokeballImage;
    public Image nidoranFemaleImage;
    public Image introDialogueBoxImage;
    public Image introAdvanceDialogueImage;
    public Image introSelectionBoxImage;
    public Image boyCharacterImage;
    public Image girlCharacterImage;
    public Image rivalImage;
    public Image introConfirmationBoxImage;
    public Image bagImage;
    public Image pokemonSelectionDialogueBoxImage;
    public Image pokemonSelectionAdvanceDialogueImage;
    public Image bulbasaurPokeballImage;
    public Image charmanderPokeballImage;
    public Image squirtlePokeballImage;
    public Image bulbasaurImage;
    public Image charmanderImage;
    public Image squirtleImage;
    public Image bulbasaurSelectionIndicatorImage;
    public Image charmanderSelectionIndicatorImage;
    public Image squirtleSelectionIndicatorImage;
    public Image pokemonSelectionConfirmationBoxImage;

    public Button boyButton;
    public Button girlButton;
    public Button introYesButton;
    public Button introNoButton;
    public Button bulbasaurSelectionButton;
    public Button charmanderSelectionButton;
    public Button squirtleSelectionButton;
    public Button pokemonSelectionYesButton;
    public Button pokemonSelectionNoButton;

    public Text introDialogueText;
    public Text boyText;
    public Text girlText;
    public Text playerNamePlaceholderText;
    public Text playerNameText;
    public Text rivalNamePlaceholderText;
    public Text rivalNameText;
    public Text introYesText;
    public Text introNoText;
    public Text pokemonSelectionDialogueText;
    public Text pokemonSelectionYesText;
    public Text pokemonSelectionNoText;

    public InputField playerNameInputField;
    public InputField rivalNameInputField;

    bool isBoy;
    bool playerNameConfirmation;
    bool rivalNameConfirmation;
    bool selectedBulbasaur;
    bool selectedCharmander;
    bool selectedSquirtle;
    bool hasSelectedPokemon;

    string playerName = "";
    string rivalName = "";

    string professorPokemonSelectionDialogue = "Now it is time to choose your first Pokemon! Choose from one of the three Pokeballs.";
    string bulbasaurDialogue = "I see! So the grass type Pokemon Bulbasaur is your choice?";
    string bulbasaurConfirmationDialogue = "Congratulations! You have chosen Bulbasaur!";
    string charmanderDialogue = "I see! So the fire type Pokemon Charmander is your choice?";
    string charmanderConfirmationDialogue = "Congratulations! You have chosen Charmander!";
    string squirtleDialogue = "I see! So the water type Pokemon Squirtle is your choice?";
    string squirtleConfirmationDialogue = "Congratulations! You have chosen Squirtle!";

    List<string> dialogue  = new List<string> {"Hello, there! Glad to meet you!", "Welcome to the world of Pokemon!", "My name is Oak.", "People affectionately refer to me as the Pokemon Professor.", "This world...", "... is inhabited far and wide by creatures called Pokemon.", "For some people, Pokemon are pets. Others use them for battling.", "As for myself...", "I study Pokemon as a profession.", "But first, tell me a little about yourself.", "Now tell me. Are you a boy? Or are you a girl?", "Let's begin with your name. What is it?", "This is my grandson.", "He's been your rival since you both were babies.", "... Erm, what was his name now? ","Your very own Pokemon legend is about to unfold!", "A world of dreams and adventures with Pokemon awaits! Let's go!" };

    private GameObject gameMaster;

    void Awake()
    {
        gameMaster = GameObject.FindGameObjectWithTag("Master");
    }

    void Start()
    {
        StartCoroutine("intro");
    }

    IEnumerator intro()
    {
        //TODO: look into passing info across scenes (starter pokemon)
        //TODO: look into animations for nidoran coming out of the pokeball
        //TODO: see if the music is availible

        int dialogueLength = dialogue.Count;

        for (int i = 0; i < dialogueLength; i++)
        {
            introDialogueText.text = dialogue[i];

            if (i == 4)
            {
                introPokeballImage.enabled = true;
            }

            if (i == 5)
            {
                introPokeballImage.enabled = false;
                nidoranFemaleImage.enabled = true;
            }

            if (i == 9)
            {
                introPokeballImage.enabled = true;
                nidoranFemaleImage.enabled = false;
                introPokeballImage.enabled = false;
            }

            if (i == 10)
            {
                //this is the code that allows for the user to select their gender
                introSelectionBoxImage.enabled = true;
                boyButton.image.enabled = true;
                girlButton.image.enabled = true;
                boyText.enabled = true;
                girlText.enabled = true;

                while (!Input.GetKeyDown(KeyCode.Return))
                {
                    professorImage.enabled = false;
                    if (eventSystem.currentSelectedGameObject.GetComponent<Button>() == boyButton)
                    {
                        girlCharacterImage.enabled = false;
                        boyCharacterImage.enabled = true;
                        isBoy = true;
                    }

                    if (eventSystem.currentSelectedGameObject.GetComponent<Button>() == girlButton)
                    {
                        boyCharacterImage.enabled = false;
                        girlCharacterImage.enabled = true;
                        isBoy = false;
                    }

                    yield return null;
                }
                    yield return Input.GetKeyDown(KeyCode.Return);
                    boyCharacterImage.enabled = false;
                    girlCharacterImage.enabled = false;
                    i++;
           
            }
                

            if(i == 11)
            {
                //this is the code that allows the user to input their name
                introDialogueText.text = dialogue[i];
                professorImage.enabled = false;
                introSelectionBoxImage.enabled = false;
                boyButton.image.enabled = false;
                girlButton.image.enabled = false;
                boyText.enabled = false;
                girlText.enabled = false;

                if (isBoy == true)
                {
                    boyCharacterImage.enabled = true;
                }

                if (isBoy == false)
                {
                    girlCharacterImage.enabled = true;
                }

                playerNameInputFieldToggle(true);
               
                while (playerNameConfirmation == false)
                {
                    while (!Input.GetKeyDown(KeyCode.Return))
                    {
                        playerName = playerNameInputField.text.ToString();
                        yield return null;
                    }

                    playerNameInputFieldToggle(false);
                    yield return Input.GetKeyDown(KeyCode.Return);

                    introDialogueText.text = "Right... So your name is " + playerName + ".";
                    introConfirmationBoxToggle(true);
                    eventSystem.SetSelectedGameObject(introYesButton.gameObject);

                    while (!Input.GetKeyDown(KeyCode.Return))
                    {
                        yield return null;
                    }

                    yield return Input.GetKeyDown(KeyCode.Return);

                    if (eventSystem.currentSelectedGameObject.Equals(introYesButton.gameObject))
                    {
                        playerNameConfirmation = true;
                        introConfirmationBoxToggle(false);
                    }

                    if (eventSystem.currentSelectedGameObject.Equals(introNoButton.gameObject))
                    {
                        playerNameConfirmation = false;
                        introConfirmationBoxToggle(false);
                        playerNameInputFieldToggle(true);
                        introDialogueText.text = "So, what is your name?";
                    }
                }
                i++;
            }

            if (i == 12)
            {
                //this is the code that transitions from the player portion of the intro to the user portion of the intro
                introDialogueText.text = dialogue[i];
                boyCharacterImage.enabled = false;
                girlCharacterImage.enabled = false;
                rivalImage.enabled = true;
            }

            if (i == 14)
            {
                //allows user to enter their rival name and confirm it
                rivalNameInputFieldToggle(true);
                
                while (rivalNameConfirmation == false)
                {
                    while (!Input.GetKeyDown(KeyCode.Return))
                    {
                        rivalName = rivalNameInputField.text.ToString();
                        yield return null;
                    }

                    rivalNameInputFieldToggle(false);
                    yield return Input.GetKeyDown(KeyCode.Return);

                    introDialogueText.text = "... Er, was it " + rivalName + "?";
                    introConfirmationBoxToggle(true);
                    eventSystem.SetSelectedGameObject(introYesButton.gameObject);

                    while (!Input.GetKeyDown(KeyCode.Return))
                    {
                        yield return null;
                    }

                    yield return Input.GetKeyDown(KeyCode.Return);

                    if (eventSystem.currentSelectedGameObject.Equals(introYesButton.gameObject))
                    {
                        rivalNameConfirmation = true;
                        introConfirmationBoxToggle(false);
                    }

                    if (eventSystem.currentSelectedGameObject.Equals(introNoButton.gameObject))
                    {
                        rivalNameConfirmation = false;
                        introConfirmationBoxToggle(false);
                        rivalNameInputFieldToggle(true);
                        introDialogueText.text = "So, what is his name now?";
                    }
                    
                }
                
                addNameDialogue();
                dialogueLength = dialogue.Count;
                i++;
            }

            if (i == 15)
            {
                introDialogueText.text = dialogue[i];
                rivalImage.enabled = false;
                professorImage.enabled = true;
            }

            while(!Input.GetKeyDown(KeyCode.Space))
            {
                yield return null;
            }
            yield return Input.GetKeyDown(KeyCode.Space);

            if (i == (dialogueLength - 1))
            {
                //transitions to the pokemon selection portion of the opening sequence
                StopCoroutine("intro");
                StartCoroutine("pokemonSelection");
            }
        }
    }
    
    IEnumerator pokemonSelection()
    {
        introCanvas.enabled = false;
        pokemonSelectionCanvas.enabled = true;

        bulbasaurSelectionButton.enabled = true;
        charmanderSelectionButton.enabled = true;
        squirtleSelectionButton.enabled = true;

        pokemonSelectionDialogueText.text = professorPokemonSelectionDialogue;

        eventSystem.SetSelectedGameObject(charmanderSelectionButton.gameObject);

        while (hasSelectedPokemon == false)
        {
            //this is the first loop of the pokemon selection process
            //waits for the user to hit enter on a pokeball
            while (!Input.GetKeyDown(KeyCode.Return))
            {
                if (eventSystem.currentSelectedGameObject.Equals(bulbasaurSelectionButton.gameObject))
                {
                    bulbasaurSelectionIndicatorImage.enabled = true;
                    charmanderSelectionIndicatorImage.enabled = false;
                    squirtleSelectionIndicatorImage.enabled = false;
                    selectedBulbasaur = true;
                    selectedCharmander = false;
                    selectedSquirtle = false;
                }

                if (eventSystem.currentSelectedGameObject.Equals(charmanderSelectionButton.gameObject))
                {
                    bulbasaurSelectionIndicatorImage.enabled = false;
                    charmanderSelectionIndicatorImage.enabled = true;
                    squirtleSelectionIndicatorImage.enabled = false;
                    selectedBulbasaur = false;
                    selectedCharmander = true;
                    selectedSquirtle = false;
                }

                if (eventSystem.currentSelectedGameObject.Equals(squirtleSelectionButton.gameObject))
                {
                    bulbasaurSelectionIndicatorImage.enabled = false;
                    charmanderSelectionIndicatorImage.enabled = false;
                    squirtleSelectionIndicatorImage.enabled = true;
                    selectedBulbasaur = false;
                    selectedCharmander = false;
                    selectedSquirtle = true;
                }
                yield return null;
            }
            
            yield return Input.GetKeyDown(KeyCode.Return);

            //changes canvas to display the pokemon and changes text based on which pokeball is selected
            if (selectedBulbasaur == true)
            {
                bulbasaurImage.enabled = true;
                bulbasaurPokeballImage.enabled = false;
                bulbasaurSelectionIndicatorImage.enabled = false;
                selectedBulbasaur = true;
                pokemonSelectionDialogueText.text = bulbasaurDialogue;
            }

            if (selectedCharmander == true)
            {
                charmanderImage.enabled = true;
                charmanderPokeballImage.enabled = false;
                charmanderSelectionIndicatorImage.enabled = false;
                selectedCharmander = true;
                pokemonSelectionDialogueText.text = charmanderDialogue;
            }

            if (selectedSquirtle == true)
            {
                squirtleImage.enabled = true;
                squirtlePokeballImage.enabled = false;
                squirtleSelectionIndicatorImage.enabled = false;
                selectedSquirtle = true;
                pokemonSelectionDialogueText.text = squirtleDialogue;
            }

            pokemonSelectionConfirmationBoxToggle(true);

            //this is the second confirmation loop that lets the user select yes or no as to whether or not they want to keep the chosen pokemon
            while (!Input.GetKeyDown(KeyCode.Return))
            {
                yield return null;
            }

            yield return Input.GetKeyDown(KeyCode.Return);

            //if the user selected yes
            if (eventSystem.currentSelectedGameObject.Equals(pokemonSelectionYesButton.gameObject)) //Where I need to access the GlobalControl
            {
                hasSelectedPokemon = true;
                pokemonSelectionConfirmationBoxToggle(false);


                if (selectedBulbasaur == true)
                {
                    pokemonSelectionDialogueText.text = bulbasaurConfirmationDialogue;
                    gameMaster.GetComponent<GlobalControl>().loadScene(0);
                    SceneManager.LoadScene("Scene", LoadSceneMode.Single);
                }

                if (selectedCharmander == true)
                {
                    pokemonSelectionDialogueText.text = charmanderConfirmationDialogue;
                    gameMaster.GetComponent<GlobalControl>().loadScene(1);
                    SceneManager.LoadScene("Scene", LoadSceneMode.Single);
                }
                
                if (selectedSquirtle == true)
                {
                    pokemonSelectionDialogueText.text = squirtleConfirmationDialogue;
                    gameMaster.GetComponent<GlobalControl>().loadScene(2);
                    SceneManager.LoadScene("Scene", LoadSceneMode.Single);
                }

            }
            
            //if the user selected no
            if (eventSystem.currentSelectedGameObject.Equals(pokemonSelectionNoButton.gameObject))
            {
                hasSelectedPokemon = false;
                pokemonSelectionConfirmationBoxToggle(false);
                pokemonSelectionDialogueText.text = professorPokemonSelectionDialogue;

                if (selectedBulbasaur == true)
                {
                    bulbasaurImage.enabled = false;
                    bulbasaurPokeballImage.enabled = true;
                    eventSystem.SetSelectedGameObject(bulbasaurSelectionButton.gameObject);
                    bulbasaurSelectionIndicatorImage.enabled = true;
                    selectedBulbasaur = false;
                }

                if (selectedCharmander == true)
                {
                    charmanderImage.enabled = false;
                    charmanderPokeballImage.enabled = true;
                    eventSystem.SetSelectedGameObject(charmanderSelectionButton.gameObject);
                    charmanderSelectionIndicatorImage.enabled = true;
                    selectedCharmander = false;
                }

                if (selectedSquirtle == true)
                {
                    squirtleImage.enabled = false;
                    squirtlePokeballImage.enabled = true;
                    eventSystem.SetSelectedGameObject(squirtleSelectionButton.gameObject);
                    squirtleSelectionIndicatorImage.enabled = true;
                    selectedSquirtle = false;
                }
            }
        }
    }

    //updates the main dialouge array by adding the dialouge that changes based off of user input.
    void addNameDialogue()
    {
        dialogue.Insert(15, "Thats right! I remember now! His name is " + rivalName.ToString() + ".");
        dialogue.Insert(16, playerName.ToString() + "!");
    }

    //toggles the name input field for the player
    void playerNameInputFieldToggle(bool toggle)
    {
        playerNameInputField.enabled = toggle;
        playerNameInputField.image.enabled = toggle;
        playerNamePlaceholderText.enabled = toggle;
        playerNameText.enabled = toggle;

        if (toggle == true)
        {
            playerNameInputField.ActivateInputField();
        }
        else
        {
            playerNameInputField.DeactivateInputField();
        }
    }

    //toggles the name input field for the rival
    void rivalNameInputFieldToggle(bool toggle)
    {
        rivalNameInputField.enabled = toggle;
        rivalNameInputField.image.enabled = toggle;
        rivalNamePlaceholderText.enabled = toggle;
        rivalNameText.enabled = toggle;

        if (toggle == true)
        {
            rivalNameInputField.ActivateInputField();
        }
        else
        {
            rivalNameInputField.DeactivateInputField();
        }
    }

    //toggles the confirmation box in the introduction portion
    void introConfirmationBoxToggle(bool toggle)
    {
        introConfirmationBoxImage.enabled = toggle;
        introYesButton.image.enabled = toggle;
        introYesText.enabled = toggle;
        introNoButton.image.enabled = toggle;
        introNoText.enabled = toggle;
        eventSystem.SetSelectedGameObject(introYesButton.gameObject);
    }
    
    //toggles the confirmation box in the pokemon selection portion
    void pokemonSelectionConfirmationBoxToggle(bool toggle)
    {
        pokemonSelectionConfirmationBoxImage.enabled = toggle;
        pokemonSelectionYesButton.image.enabled = toggle;
        pokemonSelectionYesText.enabled = toggle;
        pokemonSelectionNoButton.image.enabled = toggle;
        pokemonSelectionNoText.enabled = toggle;
        eventSystem.SetSelectedGameObject(pokemonSelectionYesButton.gameObject);
    }
}
