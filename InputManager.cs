﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;



public class InputManager : MonoBehaviour
{
    public Canvas escapeCanvas;
    public Canvas pokemonCanvas;
    public Canvas selectCanvas;
    public Canvas bagCanvas;
    public Canvas useCanvas;
    public Canvas summaryCanvas;

    public EventSystem eventSystem;

    GameObject currentSelectedObject;
    GameObject tempTextGameObject;
    GameObject moveFirstPokemonObject;
    GameObject moveSecondPokemonObject;
    
    public GameObject escapeSelectedObject;
    public GameObject pokemonSelectedObject;
    public GameObject selectSelectedObject;
    public GameObject bagSelectedObject;
    public GameObject useSelectedObject;

    public GameObject movePanel;

    public GameObject[] movePanelArray;
    public GameObject[] moveTypePanelArray;

    public Image summaryImage;

    Text numberText;

    public Text summaryNameText;
    public Text hp;
    public Text attack;
    public Text defense;
    public Text specialAttack;
    public Text specialDefense;
    public Text speed;
    public Text type1;
    public Text type2;

    public Text moveMessageText;

    public Text[] moveTypeTextArray;
    public Text[] moveNameTextArray;
    public Text[] moveCategoryTextArray;

    public Button pokemonButton;
    public Button bagButton;
    public Button saveButton;
    public Button loadButton;
    public Button exitButton;
    public Button useEventSystemButton;
    public Button selectEventSystemButton;

    public Button[] pokemonEventSystemButtonArray;
    public Button[] bagEventSystemButtonArray;

    public BasePokemonDataStorage[] basePokemonDataStorage = new BasePokemonDataStorage[6];

    //arrays of type names and corresponding type colors
    //dont touch the order
    static readonly string[] typeNames = {"Normal", "Fire", "Water", "Electric", "Grass", "Ice", "Fighting", "Poison", "Ground", "Flying", "Psychic", "Bug", "Rock", "Ghost", "Dragon", "Dark", "Steel", "Fairy" };
    static readonly Color[] typeColors = { new Color(.659F, .655F, .478F), new Color(.933F, .505F, .188F), new Color(.388F, .565F,.941F), new Color(.969F,.816F,.173F), new Color(.478F, .780F, .298F), new Color(.588F, .851F, .839F), new Color(.761F, .180F, .157F), new Color(.639F, .243F, .631F), new Color(.886F, .749F, .396F), new Color(.663F, .561F, .953F), new Color(.976F, .333F, .529F), new Color(.651F, .725F, .102F), new Color(.714F, .631F, .212F), new Color(.451F, .341F, .592F), new Color(.435F, .208F, .988F), new Color(.439F, .341F, .275F), new Color(.718F, .718F, .808F), new Color(.839F, .521F, .678F) };

    ColorBlock standardPokemonColorBlock;
    ColorBlock faintedPokemonColorBlock;
    
    bool escapeCanvasState;  
    bool pokemonCanvasState;
    bool selectCanvasState;
    bool bagCanvasState;
    bool useCanvasState;
    
    bool escapeSelectedObjectState;
    bool pokemonSelectedObjectState;
    bool selectSelectedObjectState;
    bool bagSelectedObjectState;
    bool useSelectedObjectState;

    int location;
    int pokemonPanelNum;

    Vector3 playerPosition;
    float x;
    float y;
    float z;

    public PokemonManager playerPokemonManager;
    public ItemManager playerItemManager;

    private GameObject[] trainers;
    //this method occurs every time the scene is loaded
    void Awake()
    {
        playerPokemonManager.pokemonArray = GameObject.Find("GameMaster").GetComponent<GlobalControl>().FPSController.GetComponent<PokemonManager>().pokemonArray;
        playerPokemonManager.setText();
        playerPokemonManager.updateHealthBars();
        playerItemManager.itemList = GameObject.Find("GameMaster").GetComponent<GlobalControl>().FPSController.GetComponent<ItemManager>().itemList;
        playerItemManager.setText(playerItemManager.itemList);
    }

    void Start()
    {

    }

    void Update()
    {
        escapeCanvasState = escapeCanvas.enabled;
        pokemonCanvasState = pokemonCanvas.enabled;
        selectCanvasState = selectCanvas.enabled;
        bagCanvasState = bagCanvas.enabled;
        useCanvasState = useCanvas.enabled;

        if (bagCanvasState == true && useCanvasState == false)
        {

            //escape menu buttons gain the ability to navigate to the bag event system buttons via the right arrow key
            updateNavigation(bagEventSystemButtonArray[0]);

            currentSelectedObject = EventSystem.current.currentSelectedGameObject;

            //gets the index of the currentselectedobject, returns -1 if the currentselectedobject does not have a corresponding value in the item array;
            location = getLocation();
            //updates the description text based on the current selected button if that button has a location value
            if (location < playerItemManager.itemList.Count && location != -1)
            {
                playerItemManager.setDescriptionText(location);
            } 
        }

        if (bagCanvasState == true && useCanvasState == true)
        {
            updateNavigation(bagEventSystemButtonArray[0], useEventSystemButton, true);
        }

        if (pokemonCanvasState == true && selectCanvasState == false)
        {
            updateNavigation(pokemonEventSystemButtonArray[0]);
            currentSelectedObject = EventSystem.current.currentSelectedGameObject;
        }

        if (pokemonCanvasState == true && selectCanvasState == true)
        {
            updateNavigation(pokemonEventSystemButtonArray[0], selectEventSystemButton, false);
        }
       
        //escape key triggers escape menu canvas to appear
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (escapeCanvasState == true)
            {
                pokemonCanvas.enabled = false;
                bagCanvas.enabled = false;

                trainers = GameObject.FindGameObjectsWithTag("Trainer");
                for (int i = 0; i > trainers.Length; i++)
                {
                    trainers[i].SetActive(true);
                }
            }
            escapeCanvas.enabled = !escapeCanvasState;
        }

        keyboardInputMovement();
    }
    
    //allows navigation via keyboard input under different scenarios
    void keyboardInputMovement()
    {
        if (Input.GetAxisRaw("Vertical") != 0 && escapeSelectedObjectState == false && escapeCanvasState == true && pokemonCanvasState == false && bagCanvasState == false && useCanvasState == false)
        {
            eventSystem.SetSelectedGameObject(escapeSelectedObject);
            escapeSelectedObjectState = true;
        }

        if (Input.GetAxis("Vertical") != 0 && pokemonSelectedObjectState == false && pokemonCanvasState == true && selectCanvasState == false)
        {
            eventSystem.SetSelectedGameObject(pokemonSelectedObject);
            pokemonSelectedObjectState = true;
        }
        if (Input.GetAxis("Vertical") != 0 && selectSelectedObjectState == false && selectCanvasState == true)
        {
            eventSystem.SetSelectedGameObject(selectSelectedObject);
            selectSelectedObjectState = true;
        }
        if (Input.GetAxisRaw("Vertical") != 0 && bagSelectedObjectState == false && bagCanvasState == true && useCanvasState == false)
        {
            eventSystem.SetSelectedGameObject(bagSelectedObject);
            bagSelectedObjectState = true;
        }

        if (Input.GetAxisRaw("Vertical") != 0 && useSelectedObjectState == false && useCanvasState == true)
        {
            eventSystem.SetSelectedGameObject(useSelectedObject);
            useSelectedObjectState = true;
        }
    }

    public bool getEscapeCanvasState()
    {
        return escapeCanvasState;
    }

    void onDisable()
    {
        escapeSelectedObjectState = false;
        pokemonSelectedObjectState = false;
        selectSelectedObjectState = false;
        bagSelectedObjectState = false;
        useSelectedObjectState = false;
    }

    //updates the navigation of the escape menu canvas to add functionality to the right arrow key
    void updateNavigation(Button button)
    {
        var pokemonNavigation = pokemonButton.navigation;
        var bagNavigation = bagButton.navigation;
        var saveNavigation = saveButton.navigation;
        var loadNavigation = loadButton.navigation;
        var exitNavigation = exitButton.navigation;

        pokemonNavigation.selectOnRight = button;
        bagNavigation.selectOnRight = button;
        saveNavigation.selectOnRight = button;
        loadNavigation.selectOnRight = button;
        exitNavigation.selectOnRight = button;

        pokemonButton.navigation = pokemonNavigation;
        bagButton.navigation = bagNavigation;
        saveButton.navigation = saveNavigation;
        loadButton.navigation = loadNavigation;
        exitButton.navigation = exitNavigation;
    }

    //updates the navigation of either the pokemon canvas or the bag canvas to have their respective event system buttons be able to access the left and right menus via the arrow keys
    void updateNavigation(Button rightButton, Button leftButton, bool isBag)
    {
        var pokemonNavigation = pokemonButton.navigation;
        var bagNavigation = bagButton.navigation;
        var saveNavigation = saveButton.navigation;
        var loadNavigation = loadButton.navigation;
        var exitNavigation = exitButton.navigation;

        pokemonNavigation.selectOnRight = rightButton;
        bagNavigation.selectOnRight = rightButton;
        saveNavigation.selectOnRight = rightButton;
        loadNavigation.selectOnRight = rightButton;
        exitNavigation.selectOnRight = rightButton;

        pokemonNavigation.selectOnLeft = leftButton;
        bagNavigation.selectOnLeft = leftButton;
        saveNavigation.selectOnLeft = leftButton;
        loadNavigation.selectOnLeft = leftButton;
        exitNavigation.selectOnLeft = leftButton;

        pokemonButton.navigation = pokemonNavigation;
        bagButton.navigation = bagNavigation;
        saveButton.navigation = saveNavigation;
        loadButton.navigation = loadNavigation;
        exitButton.navigation = exitNavigation;

        if (isBag == true)
        {
            for (int i = 0; i < bagEventSystemButtonArray.Length; i++)
            {
                var tempBagNavigation = bagEventSystemButtonArray[i].navigation;
                tempBagNavigation.selectOnRight = leftButton;
                bagEventSystemButtonArray[i].navigation = tempBagNavigation;
            }
        }
        else
        {

            if (selectCanvasState == true)
            {
                for (int i = 1; i < pokemonEventSystemButtonArray.Length; i = i + 2)
                {
                    var tempPokemonNavigation = pokemonEventSystemButtonArray[i].navigation;
                    tempPokemonNavigation.selectOnRight = leftButton;
                    pokemonEventSystemButtonArray[i].navigation = tempPokemonNavigation;
                }
            }
        }
    }

    //enables or disables the pokemon canvas based on the current state and disables canvases that may be enabled but should not if the pokemon is enabled
    public void pokeCanvasToggle()
    {
        pokemonCanvas.enabled = !pokemonCanvasState;
        playerPokemonManager.updateHealthBars();
        playerPokemonManager.setText();
        bagCanvas.enabled = false;
        useCanvas.enabled = false;
        movePanel.GetComponent<Image>().enabled = false;
    }

    //enables or disables the select canvas based on the current state and disables canvases that may be enabled but should not if the select is enabled
    public void selectCanvasToggle()
    {
        selectCanvas.enabled = !selectCanvasState;
        movePanel.GetComponent<Image>().enabled = false;
    }

    //enables or disables the bag canvas based on the current state and disables canvases that may be enabled but should not if the bag is enabled
    public void bagCanvasToggle()
    {
        bagCanvas.enabled = !bagCanvasState;
        pokemonCanvas.enabled = false;
        useCanvas.enabled = false;
        summaryCanvas.enabled = false;
        selectCanvas.enabled = false;
        movePanel.GetComponent<Image>().enabled = false;
    }

    //enables or disables the use canvas based on the current state
    public void useCanvasToggle()
    {
        useCanvas.enabled = !useCanvasState;
    }
    
    //closes everything
    public void exit()
    {
        float x;
        float z;
        
        pokemonCanvas.enabled = false;
        selectCanvas.enabled = false;
        escapeCanvas.enabled = false;
        bagCanvas.enabled = false;
        useCanvas.enabled = false;
        summaryCanvas.enabled = false;
        selectCanvas.enabled = false;

        if (movePanel.activeSelf == true)
        {
            StopCoroutine(selectFirstPokemon());
            StopCoroutine(selectSecondPokemon());
        }

        movePanel.GetComponent<Image>().enabled = false;

        x = playerPosition.x;
        z = playerPosition.z;

        setPlayerPosition(x, 5.0f, z);

        GameObject.Find("GameMaster").GetComponent<GlobalControl>().FPSController.GetComponent<PokemonManager>().pokemonArray = playerPokemonManager.pokemonArray;
        GameObject.Find("GameMaster").GetComponent<GlobalControl>().FPSController.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
        SceneManager.LoadScene("Scene", LoadSceneMode.Single);
        
    }

    public bool checkIfPokemonExists(string pokemonPanelString)
    {
        for (int i = 1; i <= playerPokemonManager.pokemonArray.Length; i++)
        {
            if (pokemonPanelString.Contains(i.ToString()))
            {
                return true;
            }
        }
        return false;
    }

    //enabled the summary canvas and fills the interface with information based off of the current selected pokemon
    public void summary()
    {
        GameObject parentSelectedObjectTransform;
        GameObject type1ParentPanel;
        GameObject type2ParentPanel;

        int index = 0;
        int movesArrayLength;
        int typeColorIndex;
        
        parentSelectedObjectTransform = currentSelectedObject.transform.parent.gameObject;
        
        for (int i = 1; i <= 6; i++)
        {
            if (parentSelectedObjectTransform.ToString().Contains(i.ToString()))
            {
                index = i -1;
            }
        }

        summaryNameText.text = playerPokemonManager.pokemonArray[index].GetComponent<BasePokemon>().returnName().ToString();
        summaryImage.GetComponent<Image>().sprite = playerPokemonManager.pokemonArray[index].GetComponent<BasePokemon>().pokemonSprite;
        hp.text = playerPokemonManager.pokemonArray[index].GetComponent<BasePokemon>().returnCurrentHealth().ToString() + " / " + playerPokemonManager.pokemonArray[index].GetComponent<BasePokemon>().returnMaxHealth().ToString();
        attack.text = playerPokemonManager.pokemonArray[index].GetComponent<BasePokemon>().returnAttack().ToString();
        defense.text = playerPokemonManager.pokemonArray[index].GetComponent<BasePokemon>().returnDefense().ToString();
        specialAttack.text = playerPokemonManager.pokemonArray[index].GetComponent<BasePokemon>().returnSpecialAttack().ToString();
        specialDefense.text = playerPokemonManager.pokemonArray[index].GetComponent<BasePokemon>().returnSpecialDefense().ToString();
        speed.text = playerPokemonManager.pokemonArray[index].GetComponent<BasePokemon>().returnSpeed().ToString();
        type1.text = playerPokemonManager.pokemonArray[index].GetComponent<BasePokemon>().returnType1().ToString();
        type2.text = playerPokemonManager.pokemonArray[index].GetComponent<BasePokemon>().returnType2().ToString();

        movePanel.SetActive(false);

        type1ParentPanel = type1.transform.parent.gameObject;
        type2ParentPanel = type2.transform.parent.gameObject;


        movesArrayLength = playerPokemonManager.pokemonArray[index].GetComponent<MoveManager>().moves.Length;

        for (int i = 0; i < movesArrayLength; i++)
        {
            movePanelArray[i].SetActive(true);
            moveTypePanelArray[i].SetActive(true);
            moveTypeTextArray[i].gameObject.SetActive(true);
            moveNameTextArray[i].gameObject.SetActive(true);
            moveCategoryTextArray[i].gameObject.SetActive(true);

            moveTypeTextArray[i].text = playerPokemonManager.pokemonArray[index].GetComponent<MoveManager>().moves[i].returnType().ToString();
            moveNameTextArray[i].text = playerPokemonManager.pokemonArray[index].GetComponent<MoveManager>().moves[i].returnName().ToString();

            //fills out info based on if the move is physical, special or status
            if (playerPokemonManager.pokemonArray[index].GetComponent<MoveManager>().moves[i].isPhysicalMove() == true)
            {
                moveCategoryTextArray[i].text = "PHYS";
            }
            else if (playerPokemonManager.pokemonArray[index].GetComponent<MoveManager>().moves[i].isSpecialMove() == true)
            {
                moveCategoryTextArray[i].text = "SPEC";
            }
            else
            {
                moveCategoryTextArray[i].text = "STAT";
            }

            for (int x = 0; x < typeNames.Length;  x++)
            {
                if (typeNames[x].ToString().Equals(moveTypeTextArray[i].text.ToString()) == true)
                {
                    typeColorIndex = x;
                    moveTypePanelArray[i].GetComponent<Image>().color = typeColors[typeColorIndex];
                    x = typeNames.Length + 1;
                }

            }

            
        }

        if (movesArrayLength < 4)
        {
            for (int i = movesArrayLength; i < 4; i++)
            {
                moveTypePanelArray[i].gameObject.SetActive(false);
                moveTypeTextArray[i].gameObject.SetActive(false);
                moveNameTextArray[i].gameObject.SetActive(false);
                moveCategoryTextArray[i].gameObject.SetActive(false);
            }
        }

        //finds first type
        for (int i = 0; i < 18; i++)
        {
            if (type1.text.Equals(typeNames[i]))
            {
                type1ParentPanel.GetComponentInChildren<Text>().Equals(typeNames[i]);
                type1ParentPanel.GetComponent<Image>().color = typeColors[i];
                i = 18;
            }
        }

        //check if there is a second type, if false it disables the second panel and moves the first over so that it is centered
        if (type2.text.ToString() == "None")
        {
            type2ParentPanel.SetActive(false);
            type1ParentPanel.transform.Translate(50, 0, 0);
        }
        else
        {
            for (int i = 0; i < 18; i++)
            {
                if (type2.text.Equals(typeNames[i]))
                {
                    type2ParentPanel.GetComponentInChildren<Text>().Equals(typeNames[i]);
                    type2ParentPanel.GetComponent<Image>().color = typeColors[i];
                    i = 18;
                }
            }
        }

        pokemonCanvas.enabled = false;
        summaryCanvas.enabled = true;
    }

    //allows the user to select two pokemon to swap
    public void move()
    {
        int firstPokemonLocation = 0;
        int secondPokemonLocation = 0;
        ColorBlock tempColorBlock;
        ColorBlock resetColorBlock;
        GameObject tempPokemon;
        
        summaryCanvas.enabled = false;
        pokemonCanvas.enabled = true;
        
        //enables move message panel
        moveMessageText.transform.parent.gameObject.GetComponent<Image>().enabled = true;
        moveMessageText.transform.gameObject.GetComponent<Text>().enabled = true;

        //runs on first time through
        if (moveFirstPokemonObject == null)
        {
            moveMessageText.text = "Please Select a Pokemon to Move ('M' Key)";
            StartCoroutine(selectFirstPokemon());
        }

        //runs after the first pokemon is selected
        //highlights the first pokemon and starts the second coroutine
        if (moveFirstPokemonObject != null && moveSecondPokemonObject == null)
        {
            tempColorBlock = moveFirstPokemonObject.GetComponentInChildren<Button>().colors;
            tempColorBlock.normalColor = moveFirstPokemonObject.GetComponentInChildren<Button>().colors.highlightedColor;
            moveFirstPokemonObject.GetComponentInChildren<Button>().colors = tempColorBlock;
            StopCoroutine(selectFirstPokemon());

            moveMessageText.text = "Please Select a Pokemon to Swap ('M' Key)";
            StartCoroutine(selectSecondPokemon());
        }

        //runs once a second pokemon is selected
        //swaps the two pokemon, ends the second coroutine, changes the color of the first pokemon back to normal and closes the move panel
        if (moveSecondPokemonObject != null)
        {
            StopCoroutine(selectSecondPokemon());
            
            for (int i = 1; i <= playerPokemonManager.pokemonArray.Length; i++)
            {
                if (moveFirstPokemonObject.ToString().Contains(i.ToString()))
                {
                    firstPokemonLocation = i - 1;
                    i = playerPokemonManager.pokemonArray.Length + 1;
                }
            }

            for (int x = 1; x <= playerPokemonManager.pokemonArray.Length; x++)
            {
                if (moveSecondPokemonObject.ToString().Contains(x.ToString()))
                {
                    secondPokemonLocation = x - 1;
                    x = playerPokemonManager.pokemonArray.Length + 1;
                }
            }

            tempPokemon = playerPokemonManager.pokemonArray[firstPokemonLocation];
            playerPokemonManager.pokemonArray[firstPokemonLocation] = playerPokemonManager.pokemonArray[secondPokemonLocation];
            playerPokemonManager.pokemonArray[secondPokemonLocation] = tempPokemon;

            playerPokemonManager.setText();
            playerPokemonManager.updateHealthBars();

            resetColorBlock = moveFirstPokemonObject.GetComponentInChildren<Button>().colors;
            resetColorBlock.normalColor = new Color(0f, 0f, 0f, 0f);
            moveFirstPokemonObject.GetComponentInChildren<Button>().colors = resetColorBlock;
            
            moveFirstPokemonObject = null;
            moveSecondPokemonObject = null;
            moveMessageText.enabled = false;
            movePanel.GetComponent<Image>().enabled = false;
            currentSelectedObject = selectEventSystemButton.gameObject;
        }
    }
   
    //first pokemon coroutine
    //waits for user to select a first pokemon before recursively calling the move method
    IEnumerator selectFirstPokemon()
    {
        bool wait;

        wait = true;

        while (wait)
        {
            keyboardInputMovement();

            if (Input.GetKeyDown(KeyCode.M) && EventSystem.current.currentSelectedGameObject.transform.parent.ToString().Contains("PokemonPanel") == true && checkIfPokemonExists(EventSystem.current.currentSelectedGameObject.transform.parent.ToString()) == true)
            {
                moveFirstPokemonObject = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
                wait = false;
                yield return null;
                move();
            }
            yield return null;
        }
    }

    //second pokemon coroutine
    //same concept as first but with added condition that the two selected pokemon are different
    IEnumerator selectSecondPokemon()
    {
        bool wait;

        wait = true;

        while (wait)
        {
            keyboardInputMovement();

            if (Input.GetKeyDown(KeyCode.M) && EventSystem.current.currentSelectedGameObject.transform.parent.ToString().Contains("PokemonPanel") == true && EventSystem.current.currentSelectedGameObject.transform.parent != moveFirstPokemonObject && checkIfPokemonExists(EventSystem.current.currentSelectedGameObject.transform.parent.ToString()) == true)
            {
                moveSecondPokemonObject = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
                wait = false;
                yield return null;
                move();
            }
            yield return null;
        }
    }

    //closes the select panel
    public void selectBack()
    {
        GameObject type1ParentPanel;
        GameObject type2ParentPanel;

        selectCanvas.enabled = false;
        summaryCanvas.enabled = false;
        pokemonCanvas.enabled = true;
        movePanel.SetActive(false);

        type1ParentPanel = type1.transform.parent.gameObject;
        type2ParentPanel = type2.transform.parent.gameObject;

        if (type2ParentPanel.activeSelf == false)
        {
            type2ParentPanel.SetActive(true);
            type1ParentPanel.transform.Translate(-50, 0, 0);
        }

        if (movePanel.activeSelf == true)
        {
            StopCoroutine(selectFirstPokemon());
            StopCoroutine(selectSecondPokemon());
        }
    }
    
    //allows the user to use an item when in the bag canvas
    //uses an item right now is the same as discarding it
    public void use()
    {
        //TODO: add different types of items through an extensive hierarchy so that each item can have a specific function
        //TODO: potentially look into adding different lists based off of the item type or ways to sort the list based on a sort button;
        //the sort button would make several different buttons appear in a panel that each call functions which sort the list in different ways

        string gameObjectString;
        string nameString = "";
        int tempNum = 0;
        int pageNumber = playerItemManager.getPageNumber();
        int start = (pageNumber - 1) * 7;
        int end;
        Text nameText;
        GameObject tempTextGameObject;
        List<Item> tempList = new List<Item>();

        if ((start + 7) >= playerItemManager.itemList.Count)
        {
            end = playerItemManager.itemList.Count;
        }
        else
        {
            end = start + 7;
        }

        gameObjectString = currentSelectedObject.ToString();

        for (int i = 1; i <= playerItemManager.itemList.Count; i++)
        {
            if (gameObjectString.Contains(i.ToString()) == true)
            {
                tempTextGameObject = GameObject.Find("ItemNameText " + i.ToString());
                nameText = tempTextGameObject.GetComponent<Text>();
                nameString = nameText.text;
            }
        }

        for (int i = 0; i < playerItemManager.itemList.Count; i++)
        {
            if (nameString.Equals(playerItemManager.itemList[i].getName()))
            {
                tempNum = playerItemManager.itemList[i].getNumber();
                tempNum -= 1;
                playerItemManager.itemList[i].setNumber(tempNum);
                for (int x = start; x < end; x++)
                {
                    tempList.Add(playerItemManager.itemList[x]);
                }
                playerItemManager.setText(tempList);
            }
        }

        playerItemManager.checkZero();
    }

    //allows the user to discard an item
    public void discard()
    {
        string gameObjectString;
        string nameString = "";
        int tempNum = 0;
        int pageNumber = playerItemManager.getPageNumber();
        int start = (pageNumber - 1) * 7;
        int end;
        Text nameText;
        GameObject tempTextGameObject;
        List<Item> tempList = new List<Item>();

        if ((start + 7) >= playerItemManager.itemList.Count)
        {
            end = playerItemManager.itemList.Count;
        }
        else
        {
            end = start + 7;
        }

        gameObjectString = currentSelectedObject.ToString();

        for (int i = 1; i <= playerItemManager.itemList.Count; i++)
        {
            if (gameObjectString.Contains(i.ToString()) == true)
            {
                tempTextGameObject = GameObject.Find("ItemNameText" + i.ToString());
                nameText = tempTextGameObject.GetComponent<Text>();
                nameString = nameText.text;
            }
        }

        for (int i = 0; i < playerItemManager.itemList.Count; i++)
        {
            if (nameString.Equals(playerItemManager.itemList[i].getName()))
            {
                tempNum = playerItemManager.itemList[i].getNumber();
                tempNum -= 1;
                playerItemManager.itemList[i].setNumber(tempNum);
                for (int x = start; x < end; x++)
                {
                    tempList.Add(playerItemManager.itemList[x]);
                }
                playerItemManager.setText(tempList);
            }
        }

        playerItemManager.checkZero();

    }

    public void useBack()
    {
        useCanvas.enabled = false;
    }


    void setPlayerPosition(float x, float y, float z)
    {
        GlobalControl.Instance.FPSController.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().transform.position.Set(x, y, z);
        Debug.Log("The PlayerPosition is: " + playerPosition.ToString());
    }

    int getLocation()
    {
        string gameObjectString = "";
        int pageNumber = playerItemManager.getPageNumber();
        int add;

        if (pageNumber == 1)
        {
            add = 0;
        }
        else
        {
            add = (pageNumber - 1) * 7;      
        }

        if (currentSelectedObject != null)
        {
            gameObjectString = currentSelectedObject.ToString();
        }

        for(int i = 1; i <= 7; i++)
        {
            if (gameObjectString.Contains(i.ToString()) == true)
            {
                return (i - 1) + add;
            }
        }
        return -1;
    }

    public Vector3 getPlayerPosition()
    {
        return playerPosition;
    }

    //initializes the game state with the data recovered from the load method
    void initialize()
    {
        playerPokemonManager.setText();
        playerPokemonManager.updateHealthBars();
        playerItemManager.setText(playerItemManager.itemList);
    }

    public void save()
    {
        BasePokemonDataStorage[] tempBasePokemonDataStorageArray = new BasePokemonDataStorage[6]; 
        
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/PlayerInformation.dat");
        Data data = new Data();
        
        for(int i = 0; i < 6; i ++)
        {
            if (playerPokemonManager.pokemonArray[i] != null)
            {
                //This is the code that made all of the data disappear for some reason
                //this should derive from global instance, not playerPokemonManager

                
                tempBasePokemonDataStorageArray[i].name = playerPokemonManager.pokemonArray[i].GetComponent<BasePokemon>().name;
                tempBasePokemonDataStorageArray[i].maxHealth = playerPokemonManager.pokemonArray[i].GetComponent<BasePokemon>().maxHealth;
                tempBasePokemonDataStorageArray[i].currentHealth = playerPokemonManager.pokemonArray[i].GetComponent<BasePokemon>().currentHealth;
                tempBasePokemonDataStorageArray[i].attack = playerPokemonManager.pokemonArray[i].GetComponent<BasePokemon>().attack;
                tempBasePokemonDataStorageArray[i].defense = playerPokemonManager.pokemonArray[i].GetComponent<BasePokemon>().defense;
                tempBasePokemonDataStorageArray[i].spAttack = playerPokemonManager.pokemonArray[i].GetComponent<BasePokemon>().spAttack;
                tempBasePokemonDataStorageArray[i].spDefense = playerPokemonManager.pokemonArray[i].GetComponent<BasePokemon>().spDefense;
                tempBasePokemonDataStorageArray[i].speed = playerPokemonManager.pokemonArray[i].GetComponent<BasePokemon>().speed;
                tempBasePokemonDataStorageArray[i].type = playerPokemonManager.pokemonArray[i].GetComponent<BasePokemon>().type;
                tempBasePokemonDataStorageArray[i].type2 = playerPokemonManager.pokemonArray[i].GetComponent<BasePokemon>().type2;
                tempBasePokemonDataStorageArray[i].baseExperienceYield = playerPokemonManager.pokemonArray[i].GetComponent<BasePokemon>().baseExperienceYield;
                tempBasePokemonDataStorageArray[i].fainted = playerPokemonManager.pokemonArray[i].GetComponent<BasePokemon>().fainted;
                tempBasePokemonDataStorageArray[i].conditions = playerPokemonManager.pokemonArray[i].GetComponent<BasePokemon>().conditions;
                tempBasePokemonDataStorageArray[i].currentExp = playerPokemonManager.pokemonArray[i].GetComponent<BasePokemon>().currentExp;
                tempBasePokemonDataStorageArray[i].experienceToNextLevel = playerPokemonManager.pokemonArray[i].GetComponent<BasePokemon>().experienceToNextLevel;
                tempBasePokemonDataStorageArray[i].evolveLevel = playerPokemonManager.pokemonArray[i].GetComponent<BasePokemon>().evolveLevel;
                

            }
        }

        data.basePokemonDataStorageArray = tempBasePokemonDataStorageArray;

        //resets the tempBasePokemonDataStorageArray to null for future save attempts.
        for (int i = 0; i < 6; i++)
        {
            tempBasePokemonDataStorageArray[i] = null;
        }


        for (int i = 0; i < playerItemManager.itemList.Count; i++)
        {
            data.itemList.Add(playerItemManager.itemList[i]);
        }
        
        x = playerPosition.x;
        z = playerPosition.z;

        data.x = x;
        data.z = z;

        bf.Serialize(file, data);
        file.Close();
    }


    public void load()
    {
        BasePokemonDataStorage[] tempBasePokemonDataStorageArray = new BasePokemonDataStorage[6];

        
        if (File.Exists(Application.persistentDataPath + "/PlayerInformation.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/PlayerInformation.dat", FileMode.Open);
            Data data = (Data)bf.Deserialize(file);

            file.Close();

            tempBasePokemonDataStorageArray = data.basePokemonDataStorageArray;

            for (int i = 0; i < 6; i++)
            {
                if (tempBasePokemonDataStorageArray[i] != null)
                {
                    playerPokemonManager.pokemonArray[i].GetComponent<BasePokemon>().name = tempBasePokemonDataStorageArray[i].name;
                    playerPokemonManager.pokemonArray[i].GetComponent<BasePokemon>().maxHealth = tempBasePokemonDataStorageArray[i].maxHealth;
                    playerPokemonManager.pokemonArray[i].GetComponent<BasePokemon>().currentHealth = tempBasePokemonDataStorageArray[i].currentHealth;
                    playerPokemonManager.pokemonArray[i].GetComponent<BasePokemon>().attack = tempBasePokemonDataStorageArray[i].attack;
                    playerPokemonManager.pokemonArray[i].GetComponent<BasePokemon>().defense = tempBasePokemonDataStorageArray[i].defense;
                    playerPokemonManager.pokemonArray[i].GetComponent<BasePokemon>().spAttack = tempBasePokemonDataStorageArray[i].spAttack;
                    playerPokemonManager.pokemonArray[i].GetComponent<BasePokemon>().spDefense = tempBasePokemonDataStorageArray[i].spDefense;
                    playerPokemonManager.pokemonArray[i].GetComponent<BasePokemon>().speed = tempBasePokemonDataStorageArray[i].speed;
                    playerPokemonManager.pokemonArray[i].GetComponent<BasePokemon>().type = tempBasePokemonDataStorageArray[i].type;
                    playerPokemonManager.pokemonArray[i].GetComponent<BasePokemon>().type2 = tempBasePokemonDataStorageArray[i].type2;
                    playerPokemonManager.pokemonArray[i].GetComponent<BasePokemon>().baseExperienceYield = tempBasePokemonDataStorageArray[i].baseExperienceYield;
                    playerPokemonManager.pokemonArray[i].GetComponent<BasePokemon>().conditions = tempBasePokemonDataStorageArray[i].conditions;
                    playerPokemonManager.pokemonArray[i].GetComponent<BasePokemon>().currentExp = tempBasePokemonDataStorageArray[i].currentExp;
                    playerPokemonManager.pokemonArray[i].GetComponent<BasePokemon>().experienceToNextLevel = tempBasePokemonDataStorageArray[i].experienceToNextLevel;
                    playerPokemonManager.pokemonArray[i].GetComponent<BasePokemon>().evolveLevel = tempBasePokemonDataStorageArray[i].evolveLevel;
                }
            }

            GlobalControl.Instance.FPSController.GetComponent<PokemonManager>().pokemonArray = playerPokemonManager.pokemonArray;

            //this is the loading code for the items
            playerItemManager.itemList.Clear();

            for (int i = 0; i < data.itemList.Count; i++)
            {
                playerItemManager.itemList.Add(data.itemList[i]);
            }


            //this is the loading code for the playerposition
            //NOTE: There is a bug with the y axis where the player will continue to freefall when transitioning to the inventory scene
            x = data.x;
            z = data.z;

            setPlayerPosition(x, 5.0f, z);

            initialize();
        }
    }
}

[System.Serializable]
//this is a storage class
//any data that needs to be stored must have a variable here
//storing pokemon should be classified as cruel and unusual punishment
public class Data
{
    public float x;
    public float z;
    public List<Item> itemList = new List<Item>();
    public BasePokemonDataStorage[] basePokemonDataStorageArray = new BasePokemonDataStorage [6];
    
}
