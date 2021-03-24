using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    [SerializeField]

    public MoveManager pokemonOne;
    public MoveManager pokemonTwo;
    private GameObject defendingPokemon;
    private GameObject playerPokemon;

    private GameObject[] playerPokemonArray;
    private GameObject[] trainerPokemonArray;
    
    private int currentTrainerPokemon;
    private int power;
    private int accuracy;

    private int speed;
    private int accuracyCheck;
    private bool hit;
    private bool stabBonus;
    private float damage;
    private float[,] effectivenessArray;

    private string name;
    private string pokemonName;
    private string type;

    private string playerPokemonName;

    private string trainerName;

    private bool showMoves;
    private bool skipAttack;

    static private int countAttacks;

    private string defendingType1;
    private string defendingType2;

    private bool[] wildStatusEffects;
    private bool[] playerStatusEffects;
    private int effectChance;

    private int statChance;

    private float currentAttack;
    private float currentDefense;
    private float currentSpeed;
    private float currentSpAttack;
    private float currentSpDefense;
    private float currentAccuracy;

    private float defendingPokemonSpeed;

    private int playerAttackStage;
    private int playerDefenseStage;
    private int playerSpeedStage;
    private int playerSpAttackStage;
    private int playerSpDefenseStage;
    private int playerAccuracyStage;

    private int wildAttackStage;
    private int wildDefenseStage;
    private int wildSpeedStage;
    private int wildSpAttackStage;
    private int wildSpDefenseStage;
    private int wildAccuracyStage;

    private int hitChance;
    private float waitTime;

    public Button moveButton1;
    public Button moveButton2;
    public Button moveButton3;
    public Button moveButton4;

    public Button returnButton;
    public Button returnSwitchPokemonButton;

    public Text displayText;
    public Text experienceDisplayText;

    public Button pokemonButton1;
    public Button pokemonButton2;
    public Button pokemonButton3;
    public Button pokemonButton4;
    public Button pokemonButton5;
    public Button pokemonButton6;
    public Text selectPokemonText;

    public Image defendingPokemonHealthBar;
    public Text defendingPokemonNameText;
    public Image playerPokemonHealthBar;
    public Text playerPokemonHealthText;
    public Text playerPokemonNameText;

    public GameObject fpsController;

    private bool defendingFainted;
    private bool playerFainted;
    private bool trainer;

    private int playerPokemonCount;
    private bool previousPokemonFainted;

    public GameObject moveButtons;
    public GameObject mainButtons;
    public GameObject selectPokemonButtons;
    public GameObject bagButtons;

    const int potion = 40;
    public int sPotion;
    public int mPotion;
    public int fHeal;
    public int pokeball;
    public int greatball;
    public int ultraball;
    public int itemDropRan;
    public int pokeballDrop;

    public Button superPotionButton;
    public Button maxPotionButton;
    public Button fullHealButton;
    public Button pokeBallButton;
    public Button greatBallButton;
    public Button ultraBallButton;

    public Image playerExperienceBar;
    private GameObject trainerObject;

    private bool caught;

    public Text playerStatusText;
    public Text wildStatusText;

    public static bool trainerBool = true; 

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    void Start()
    {
        sPotion = 1;
        mPotion = 1;
        fHeal = 1;
        pokeball = 1;
        greatball = 1;
        ultraball = 1;

        effectivenessArray = new float[,] { 
        { 1, 1, 1, 1, 1, 0.5f, 1, 0, 0.5f, 1, 1, 1, 1, 1, 1, 1, 1 }, //Normal
        { 2, 1, 0.5f, 0.5f, 1, 2, 0.5f, 0, 2, 1, 1, 1, 1, 0.5f, 2, 1, 2 }, //Fight
        { 1, 2, 1, 1, 1, 0.5f, 2, 1, 0.5f, 1, 1, 2, 0.5f, 1, 1, 1, 1 }, //Flying
        { 1, 1, 1, 0.5f, 0.5f, 0.5f, 1, 0.5f, 0, 1, 1, 2, 1, 1, 1, 1, 1 }, //Poison
        { 1, 1, 0, 2, 1, 2, 0.5f, 1, 2, 2, 1, 0.5f, 2, 1, 1, 1, 2 }, //Ground
        { 1, 0.5f, 2, 1, 0.5f, 1, 2, 1, 0.5f, 2, 1, 1, 1, 1, 2, 1, 1 },//Rock
        { 1, 0.5f, 0.5f, 0.5f, 1, 1, 1, 0.5f, 0.5f, 0.5f, 1, 2, 1, 2, 1, 1, 2 },//Bug
        { 0, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 2, 1, 1, 0.5f },//Ghost
        { 1, 1, 1, 1, 1, 2, 1, 1, 0.5f, 0.5f, 0.5f, 1, 0.5f, 1, 2, 1, 1 }, //Steel - 8
        { 1, 1, 1, 1, 1, 0.5f, 2, 1, 2, 0.5f, 0.5f, 2, 1, 1, 2, 0.5f, 1 },//Fire
        { 1, 1, 1, 1, 2, 2, 1, 1, 1, 2, 0.5f, 0.5f, 1, 1, 1, 0.5f, 2 }, //Water
        { 1, 1, 0.5f, 0.5f, 2, 2, 0.5f, 1, 0.5f, 0.5f, 2, 0.5f, 1, 1, 1, 0.5f, 1 }, //Grass
        { 1, 1, 2, 1, 0, 1, 1, 1, 1, 1, 2, 0.5f, 0.5f, 1, 1, 0.5f, 1 }, //Electric
        { 1, 2, 1, 2, 1, 1, 1, 1, 0.5f, 1, 1, 1, 1, 0.5f, 1, 1, 0 },//Psychic
        { 1, 1, 2, 1, 2, 1, 1, 1, 0.5f, 0.5f, 0.5f, 2, 1, 1, 0.5f, 2, 1 },//Ice
        { 1, 1, 1, 1, 1, 1, 1, 1, 0.5f, 1, 1, 1, 1, 1, 1, 2, 1 }, //Dragon
        { 1, 0.5f, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 2, 1, 1, 0.5f },}; //Dark

        skipAttack = false;
        countAttacks = 0;
        wildStatusEffects = new bool[] { false, false, false, false, false, false }; //Poison(0), Sleep(1), Paralyzed(2), Freeze(3), Burn(4), Confusion(5).
        effectChance = 0;

        playerAttackStage = 0;
        playerDefenseStage = 0;
        playerSpeedStage = 0;
        playerSpAttackStage = 0;
        playerSpDefenseStage = 0;
        playerAccuracyStage = 0;

        wildAttackStage = 0;
        wildDefenseStage = 0;
        wildSpeedStage = 0;
        wildSpAttackStage = 0;
        wildSpDefenseStage = 0;
        wildAccuracyStage = 0;

        fpsController = GameObject.FindWithTag("FPS"); 
        fpsController.SetActive(false);

        trainerObject = GameObject.FindWithTag("Trainer");

        defendingFainted = false;
        playerFainted = false;

        pokemonButton1.gameObject.SetActive(false);
        pokemonButton2.gameObject.SetActive(false);
        pokemonButton3.gameObject.SetActive(false);
        pokemonButton4.gameObject.SetActive(false);
        pokemonButton5.gameObject.SetActive(false);
        pokemonButton6.gameObject.SetActive(false);
        selectPokemonText.gameObject.SetActive(false);

        returnButton.gameObject.SetActive(false);
        returnSwitchPokemonButton.gameObject.SetActive(false);
        bagButtons.SetActive(false);

        trainer = fpsController.GetComponent<PokemonManager>().returnFightingTrainer();
        if (trainer)
        {
            trainerName = fpsController.GetComponent<PokemonManager>().returnTrainerName();
        }

        previousPokemonFainted = false;
        caught = false;

        startBattle();
    }

    public void setPlayerPokemon(GameObject playerPoke)
    {
        playerPokemon = playerPoke;
    }

    public void startBattle()
    {
        if (trainer)
        {
            //find objectswithtag takes a while but idk if theres a better way
            trainerPokemonArray = GameObject.FindGameObjectsWithTag("Wild");
            GetComponent<PokemonManager>().setPokemonNumber(0, trainerPokemonArray[0]);
            defendingPokemon = GetComponent<PokemonManager>().returnPokemonNumber(0);
            currentTrainerPokemon = 0;

            for (int i = 1; i < trainerPokemonArray.Length; i++)
            {
                trainerPokemonArray[i].SetActive(false);
            }
        }
        else
        {
            GetComponent<PokemonManager>().setPokemonNumber(0, GameObject.FindWithTag("Wild"));
            defendingPokemon = GetComponent<PokemonManager>().returnPokemonNumber(0);
        }

        playerPokemonArray = GameObject.FindGameObjectsWithTag("Player");

        int firstPokemonAlive = 0;

        for (int i = 0; i < playerPokemonArray.Length; i++)
        {
            if (!playerPokemonArray[i].GetComponent<BasePokemon>().isFainted())
            {
                firstPokemonAlive = i;
                i = 7;
            }
        }

        for (int i = 0; i < playerPokemonArray.Length; i++)
        {
            if (i != firstPokemonAlive)
            {
                playerPokemonArray[i].SetActive(false);
            }
        }   

        playerPokemon = playerPokemonArray[firstPokemonAlive];

        playerPokemon.SetActive(true);

        playerPokemonCount = playerPokemonArray.Length;

        GetComponent<PokemonManager>().setPokemonNumber(1, playerPokemon);

        pokemonOne = defendingPokemon.GetComponent<MoveManager>();
        pokemonTwo = playerPokemon.GetComponent<MoveManager>();
        defendingPokemon.GetComponent<BasePokemon>().stopDespawnTimer(false);

        pokemonName = defendingPokemon.GetComponent<BasePokemon>().returnName();

        playerPokemonName = playerPokemon.GetComponent<BasePokemon>().returnName();
        speed = playerPokemon.GetComponent<BasePokemon>().returnSpeed();

        playerStatusEffects = playerPokemon.GetComponent<BasePokemon>().returnConditions();
        
        currentAttack = defendingPokemon.GetComponent<BasePokemon>().returnAttack();
        currentDefense = defendingPokemon.GetComponent<BasePokemon>().returnDefense();
        currentSpeed = defendingPokemon.GetComponent<BasePokemon>().returnSpeed();
        currentSpAttack = defendingPokemon.GetComponent<BasePokemon>().returnSpecialAttack();
        currentSpDefense = defendingPokemon.GetComponent<BasePokemon>().returnSpecialDefense();

        if (!playerPokemon.GetComponent<BasePokemon>().isWild())
        {
            moveButton1.GetComponentInChildren<Text>().text = pokemonTwo.returnMoveName(0);
            moveButton2.GetComponentInChildren<Text>().text = pokemonTwo.returnMoveName(1);
            moveButton3.GetComponentInChildren<Text>().text = pokemonTwo.returnMoveName(2);
            moveButton4.GetComponentInChildren<Text>().text = pokemonTwo.returnMoveName(3);

            if (!trainer)
            {
                displayText.text = "A wild " + pokemonName + " appeared!";
            }
            else if (trainer)
            {
                displayText.text = trainerName + " sent out " + pokemonName + "!";
            }
        }

        defendingPokemon.transform.eulerAngles = new Vector3(0, 180, 0);
        playerPokemon.transform.eulerAngles = new Vector3(0, 180, 0);
        playerPokemonNameText.text = playerPokemonName + " lvl " + playerPokemon.GetComponent<BasePokemon>().returnLevel();
        defendingPokemonNameText.text = pokemonName + " lvl " + defendingPokemon.GetComponent<BasePokemon>().returnLevel();
        playerPokemonHealthText.text = playerPokemon.GetComponent<BasePokemon>().returnCurrentHealth() + "/" + playerPokemon.GetComponent<BasePokemon>().returnMaxHealth();
        updatePlayerPokemonHealthBar();
        updateExperienceBar();
        updateStatusTexts();
    }

    //-------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                      Start of Move One
    //-------------------------------------------------------------------------------------------------------------------------------------------------------
    public void useMoveOne()
    {
        calculateSpeed();

        if (currentSpeed > defendingPokemonSpeed)
        {
            StartCoroutine(moveStart(false, true, 0));
            StartCoroutine(otherPokemonUseMove(true));
        }
        else
        {
            StartCoroutine(otherPokemonUseMove(false));
            StartCoroutine(moveStart(true, true, 0)); 
        }
    }

    public void useMoveOneWild()
    {
        StartCoroutine(moveStart(false, false, 0));
    }

    IEnumerator moveStart(bool wait, bool playerPokemonAttack, int moveNum)
    {
        turnStarted();

        if (wait)
        {
            yield return new WaitForSeconds(6.0f);
            displayText.text = "";
            yield return new WaitForSeconds(2.0f);
        }

        skipAttack = false;

        if (playerPokemonAttack)
        {
            Debug.Log("COUNTATTACKS: " + countAttacks);
            yield return new WaitForSeconds(2.0f);
            if (!playerPokemon.GetComponent<BasePokemon>().isFainted())
            {
                power = pokemonTwo.returnMovePower(moveNum);
                accuracy = pokemonTwo.returnMoveAccuracy(moveNum);
                name = pokemonTwo.returnMoveName(moveNum);
                effectChance = pokemonTwo.returnEffectChance(moveNum);
                int random;

                if (playerStatusEffects[5])
                {
                    displayText.text = playerPokemonName + " is confused.";
                    yield return new WaitForSeconds(2.0f);
                }
                //Check the status effect to see if the Pokemon will attack this turn. 
                skipAttack = doStatusEffects(playerPokemonAttack);
                yield return new WaitForSeconds(2.0f);

                if (!skipAttack)
                {
                    displayText.text = playerPokemonName + " used " + name + "!";
                    yield return new WaitForSeconds(2.0f);

                    accuracyCheck = UnityEngine.Random.Range(0, 100);
                    hitChance = (int)(accuracy * accuracyValueToModifier(playerAccuracyStage));

                    hit = true;
                    if (accuracyCheck > hitChance)
                    {
                        hit = false;

                        displayText.text = name + " missed! ";
                        yield return new WaitForSeconds(2.0f);
                    }

                    type = pokemonTwo.returnMoveType(moveNum);
                    defendingType1 = defendingPokemon.GetComponent<BasePokemon>().returnType1();
                    defendingType2 = defendingPokemon.GetComponent<BasePokemon>().returnType2();
                    if (superEffective(type, defendingType1, defendingType2) > 1.0f && hit && pokemonTwo.returnEffectChance(moveNum) != 100 && !pokemonTwo.isStatChanger(moveNum))
                    {
                        displayText.text = "It was super effective!";
                        yield return new WaitForSeconds(2.0f);
                    }
                    else if (superEffective(type, defendingType1, defendingType2) < 1.0f && hit && pokemonTwo.returnEffectChance(moveNum) != 100 && !pokemonTwo.isStatChanger(moveNum))
                    {
                        displayText.text = "It was not very effective!";
                        yield return new WaitForSeconds(2.0f);
                    }

                    //Code to determine What the attack does (Physical/Special, and or Status Effect)
                    if (hit)
                    {
                        if (pokemonTwo.isSpecial(moveNum) || pokemonTwo.isPhysical(moveNum))
                        {
                            defendingPokemon.GetComponent<BasePokemon>().TakeDamage((int)totalDamageCalculator(moveNum, playerPokemonAttack), hit);
                            updateDefendingPokemonHealthBar();
                            random = UnityEngine.Random.Range(1, 100);
                            if (random < effectChance)
                            {
                                StartCoroutine(inflictStatusEffect(moveNum, playerPokemonAttack));
                                yield return new WaitForSeconds(2.0f);
                            }
                        }
                        else if (pokemonTwo.isStatusEffect(moveNum))
                        {
                            StartCoroutine(inflictStatusEffect(moveNum, playerPokemonAttack));
                            yield return new WaitForSeconds(2.0f);
                        }
                        else if (pokemonTwo.isStatChanger(moveNum))
                        {
                            StartCoroutine(inflictStatChange(moveNum, playerPokemonAttack));
                            yield return new WaitForSeconds(2.0f);
                        }
                    }
                }
                //The effect of the status effect for this turn
                doPoisonAndBurn(playerPokemonAttack);
                yield return new WaitForSeconds(2.0f);

                if (critMultiplier == 1.5f && hit)
                {
                    displayText.text = "Critical Hit!";
                    yield return new WaitForSeconds(2.0f);
                }

                countAttacks++;
                if (countAttacks == 2 && !defendingPokemon.GetComponent<BasePokemon>().isFainted())
                {
                    turnFinished();
                    displayText.text = "What will you do next?";
                }

                if (defendingPokemon.GetComponent<BasePokemon>().fainted)
                {
                    if (!trainer)
                    {
                        displayText.text = "The wild " + pokemonName + " has fainted!";
                    }
                    else if (trainer)
                    {
                        displayText.text = trainerName + "'s " + pokemonName + " has fainted!";
                    }

                    defendingFainted = true;
                    calculateExp();
                    experienceDisplayText.text = "Your " + playerPokemonName + " gained " + experience + " experience.";
                    playerPokemonNameText.text = playerPokemonName + " lvl " + playerPokemon.GetComponent<BasePokemon>().returnLevel();
                    StartCoroutine(FadeTextToFullAlpha(0.2f, experienceDisplayText));
                    StartCoroutine(FadeTextToZeroAlpha(1.8f, experienceDisplayText));
                    yield return new WaitForSeconds(1.5f);

                    if (!trainer)
                    {
                        endBattle(false);
                    }
                    else if (trainer)
                    {
                        if (currentTrainerPokemon == trainerPokemonArray.Length - 1)//When the trainer runs out of Pokemon
                        {
                            displayText.text = "You have defeated " + trainerName + "!";
                            yield return new WaitForSeconds(2.0f);
                            endBattle(false);
                        }
                        else
                        {
                            trainerSendNextPokemon();
                        }
                    }
                }
            }
        }
         else if (!playerPokemonAttack)           //THE WILD POKEMON ATTACK
        {
            yield return new WaitForSeconds(2.0f);
            if (!defendingPokemon.GetComponent<BasePokemon>().fainted) 
            {
                power = pokemonOne.returnMovePower(moveNum);
                accuracy = pokemonOne.returnMoveAccuracy(moveNum);
                name = pokemonOne.returnMoveName(moveNum);
                effectChance = pokemonOne.returnEffectChance(moveNum);
                int random;

                if (wildStatusEffects[5])
                {
                    displayText.text = pokemonName + " is confused.";
                    yield return new WaitForSeconds(2.0f);
                }

                skipAttack = doStatusEffects(playerPokemonAttack);
                yield return new WaitForSeconds(2.0f);

                if (!skipAttack)
                {
                    displayText.text = pokemonName + " used " + name + "!";
                    yield return new WaitForSeconds(2.0f);

                    accuracyCheck = UnityEngine.Random.Range(0, 100);
                    hitChance = (int)(accuracy * accuracyValueToModifier(wildAccuracyStage));

                    hit = true;
                    if (accuracyCheck > hitChance)
                    {
                        hit = false;
                        displayText.text = name + " missed! ";
                        yield return new WaitForSeconds(2.0f);
                    }

                    type = pokemonOne.returnMoveType(moveNum);
                    defendingType1 = playerPokemon.GetComponent<BasePokemon>().returnType1();
                    defendingType2 = playerPokemon.GetComponent<BasePokemon>().returnType2();

                    if (superEffective(type, defendingType1, defendingType2) > 1.0f && hit && pokemonOne.returnEffectChance(moveNum) != 100 && !pokemonOne.isStatChanger(moveNum))
                    {
                        displayText.text = "It was super effective!";
                        yield return new WaitForSeconds(2.0f);
                    }
                    else if (superEffective(type, defendingType1, defendingType2) < 1.0f && hit && pokemonOne.returnEffectChance(moveNum) != 100 && !pokemonOne.isStatChanger(moveNum))
                    {
                        displayText.text = "It was not very effective!";
                        yield return new WaitForSeconds(2.0f);
                    }

                    //Code to determine What the attack does (Physical/Special, and or Status Effect)
                    if (hit)
                    {
                        if (pokemonOne.isSpecial(moveNum) || pokemonOne.isPhysical(moveNum))
                        {
                            playerPokemon.GetComponent<BasePokemon>().TakeDamage((int)totalDamageCalculator(moveNum, playerPokemonAttack), hit);
                            updatePlayerPokemonHealthBar();
                            random = UnityEngine.Random.Range(1, 100);
                            if (random < effectChance)
                            {
                                StartCoroutine(inflictStatusEffect(moveNum, playerPokemonAttack));
                                yield return new WaitForSeconds(2.0f);
                            }
                        }
                        else if (pokemonOne.isStatusEffect(moveNum))
                        {
                            StartCoroutine(inflictStatusEffect(moveNum, playerPokemonAttack));
                            yield return new WaitForSeconds(2.0f);
                        }
                        else if (pokemonOne.isStatChanger(moveNum))
                        {
                            StartCoroutine(inflictStatChange(moveNum, playerPokemonAttack));
                            yield return new WaitForSeconds(2.0f);
                        }
                    }
                }
                //The effect of the status effect for this turn
                doPoisonAndBurn(playerPokemonAttack);
                yield return new WaitForSeconds(2.0f);

                if (critMultiplier == 1.5f && hit)
                {
                    displayText.text = "Critical Hit!";
                    yield return new WaitForSeconds(2.0f);
                }

                countAttacks++;
                if (countAttacks == 2 && !playerPokemon.GetComponent<BasePokemon>().fainted)
                {
                    turnFinished();
                    displayText.text = "What will you do next?";
                }

                if (playerPokemon.GetComponent<BasePokemon>().fainted)
                {
                    displayText.text = "Your " + playerPokemonName + " has fainted!";
                    playerFainted = true;
                    yield return new WaitForSeconds(6.0f); 
                    playerSelectNextPokemon();
                }
            }
        }
        yield break;
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                              Break for Move 2
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
    //Move Two Attack

    public void useMoveTwo()
    {
        calculateSpeed();

        if (currentSpeed > defendingPokemonSpeed)
        {
            StartCoroutine(moveStart(false, true,1));
            StartCoroutine(otherPokemonUseMove(true));
        }
        else
        {
            StartCoroutine(otherPokemonUseMove(false));
            StartCoroutine(moveStart(true, true,1));
        }
    }

    public void useMoveTwoWild()
    {
        StartCoroutine(moveStart(false, false,1));
    }

  
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                              Break for Move 3
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
    //The Third Move

    public void useMoveThree()
    {
        calculateSpeed();

        if (currentSpeed > defendingPokemonSpeed)
        {
            StartCoroutine(moveStart(false, true,2));
            StartCoroutine(otherPokemonUseMove(true));
        }
        else
        {
            StartCoroutine(otherPokemonUseMove(false));
            StartCoroutine(moveStart(true, true,2));
        }
    }

    public void useMoveThreeWild()
    {
        StartCoroutine(moveStart(false, false,2));
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                              Break for Move 4
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
    //Move Four Attack

       public void useMoveFour()
    {
        calculateSpeed();

        if (currentSpeed > defendingPokemonSpeed)
        {
            StartCoroutine(moveStart(false, true, 3));
            StartCoroutine(otherPokemonUseMove(true));
        }
        else
        {
            StartCoroutine(otherPokemonUseMove(false));
            StartCoroutine(moveStart(true, true, 3));
        }
    }

    public void useMoveFourWild()
    {
        StartCoroutine(moveStart(false, false, 3));
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                              End of Move 4
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------

    public bool checkStab()
    {
        return (type.Equals(playerPokemon.GetComponent<BasePokemon>().returnType1()) || type.Equals(playerPokemon.GetComponent<BasePokemon>().returnType2()));
    }

    int critRandomNum;
    float critMultiplier;
    float random;
    float sameTypeAttackBonus;
    float modifier;
    float superEffectiveMultiplier;

    public float totalDamageCalculator(int movenum, bool playerAttack)
    {
        critMultiplier = 1;
        sameTypeAttackBonus = 1;

        if (playerAttack)
        {
            currentAttack = playerPokemon.GetComponent<BasePokemon>().returnAttack() * stageValueToModifier(playerAttackStage);
            currentSpAttack = playerPokemon.GetComponent<BasePokemon>().returnSpecialAttack() * stageValueToModifier(playerSpAttackStage);
            currentDefense = defendingPokemon.GetComponent<BasePokemon>().returnDefense() * stageValueToModifier(wildDefenseStage);
            currentSpDefense = defendingPokemon.GetComponent<BasePokemon>().returnSpecialDefense() * stageValueToModifier(wildSpDefenseStage);

            if (playerStatusEffects[4])
            {
                currentAttack = currentAttack * 0.5f;
                Debug.Log("Burn worked");
            }

            type = pokemonTwo.returnMoveType(movenum);

            defendingType1 = defendingPokemon.GetComponent<BasePokemon>().returnType1();
            defendingType2 = defendingPokemon.GetComponent<BasePokemon>().returnType2();

            power = pokemonTwo.returnMovePower(movenum);
            if (pokemonTwo.isPhysical(movenum))
            {
                Debug.Log("power " + power + " base attack / base defense " + currentAttack + " / " + currentDefense);
                damage = 10 * power * ((currentAttack / currentDefense)) / 50 + 2;
            }
            else if (pokemonTwo.isSpecial(movenum))
            {
                Debug.Log("power " + power + " base attack / base defense " + currentSpAttack + " / " + currentSpDefense);
                damage = 10 * power * ((currentSpAttack / currentSpDefense)) / 50.0f + 2;
            }

        }
        else
        {
            currentAttack = defendingPokemon.GetComponent<BasePokemon>().returnAttack() * stageValueToModifier(wildAttackStage);
            currentSpAttack = defendingPokemon.GetComponent<BasePokemon>().returnSpecialAttack() * stageValueToModifier(wildSpAttackStage);
            currentDefense = playerPokemon.GetComponent<BasePokemon>().returnDefense() * stageValueToModifier(playerDefenseStage);
            currentSpDefense = playerPokemon.GetComponent<BasePokemon>().returnSpecialDefense() * stageValueToModifier(playerSpDefenseStage);

            if (wildStatusEffects[4])
            {
                currentAttack = currentAttack * 0.5f;
                Debug.Log("Burn worked");
            }

            type = pokemonOne.returnMoveType(movenum);

            defendingType1 = playerPokemon.GetComponent<BasePokemon>().returnType1();
            defendingType2 = playerPokemon.GetComponent<BasePokemon>().returnType2();

            power = pokemonOne.returnMovePower(movenum);
            if (pokemonOne.isPhysical(movenum))
            {
                Debug.Log("power " + power + " base attack / base defense " + currentAttack + " / " + currentDefense);
                damage = 10 * power * ((currentAttack / currentDefense)) / 50 + 2;
            }
            else if (pokemonOne.isSpecial(movenum))
            {
                Debug.Log("power " + power + " base attack / base defense " + currentSpAttack + " / " + currentSpDefense);
                damage = 10 * power * ((currentSpAttack / currentSpDefense)) / 50.0f + 2;
            }
        }

        //((2 * userPokemon.getLevel() ) /5)

        critRandomNum = UnityEngine.Random.Range(1, 20);
        if (critRandomNum == 1)
        {
            critMultiplier = 1.5f;
        }

        random = (UnityEngine.Random.Range(85, 100)) / 100f;

        if (checkStab())
        {
            sameTypeAttackBonus = 1.5f;
        }

        //Debug.Log("type: " + type + " dtype1: " + defendingType1 + " dtype2: " + defendingType2);

        modifier = critMultiplier * random * sameTypeAttackBonus * superEffective(type, defendingType1, defendingType2);
        Debug.Log("Crit " + critMultiplier + "random " + random + " stab " + sameTypeAttackBonus + " super effective " + superEffective(type, defendingType1, defendingType2));
        //Debug.Log("Damage before mod " + damage + "Mod val " + modifier);

        return damage * modifier;

    }

    public void turnStarted()
    {
        moveButtons.SetActive(false);
        returnButton.gameObject.SetActive(false);

        mainButtons.SetActive(false);
    }

    public void turnFinished()
    {
        moveButtons.SetActive(true);
        mainButtons.SetActive(true);
        countAttacks = 0;
    }

    IEnumerator otherPokemonUseMove(bool wait)
    {
        if (wait)
        {
            yield return new WaitForSeconds(8.0f);
        }
        int otherRandAttack;
        otherRandAttack = UnityEngine.Random.Range(0, 4);

        if (otherRandAttack == 0)
        {
            useMoveOneWild();
        }
        else if (otherRandAttack == 1)
        {
            useMoveTwoWild();
        }
        else if (otherRandAttack == 2)
        {
            useMoveThreeWild();
        }
        else
        {
            useMoveFourWild();
        }
        yield break;
    }

    //If return condition = -1 then there is no effect on the Pokemon
    public int returnCondition(bool player)
    {
        Debug.Log("player " + player);
        if (player)
        {
            for (int i = 0; i <= wildStatusEffects.Length - 1; i++)
            {
                if (wildStatusEffects[i])
                {
                    return i;
                }
            }
        }
        else if (!player)
        {
            for (int i = 0; i < playerStatusEffects.Length - 1; i++)
            {
                if (playerStatusEffects[i])
                {
                    return i;
                }
            }
        }
        return -1;
    }

    public bool doStatusEffects(bool player)
    {
        int random;
        if (player)
        {
            if (playerStatusEffects[1]) //Checks for sleep
            {
                random = UnityEngine.Random.Range(0, 100);
                if (random < 33)
                {
                    displayText.text = playerPokemonName + " woke up from its sleep!";
                    playerStatusEffects[1] = false;
                    playerPokemonNameText.text = playerPokemonName + " lvl " + playerPokemon.GetComponent<BasePokemon>().returnLevel();
                    updateStatusTexts();
                    return false;
                }
                else
                {
                    displayText.text = playerPokemonName + " is fast asleep!";
                    return true;
                }
            }
            else if (playerStatusEffects[2])//Check paralysis
            {
                random = UnityEngine.Random.Range(0, 100);
                if (random < 25)
                {
                    displayText.text = playerPokemonName + " was paralyzed. \nIt can't move!";
                    return true;
                }
            }
            else if (playerStatusEffects[3]) //Check Frozen
            {
                random = UnityEngine.Random.Range(0, 100);
                if (random < 33)
                {
                    displayText.text = playerPokemonName + " was thawed out!";
                    playerStatusEffects[3] = false;
                    playerPokemonNameText.text = playerPokemonName + " lvl " + playerPokemon.GetComponent<BasePokemon>().returnLevel();
                    updateStatusTexts();
                }
                else
                {
                    displayText.text = playerPokemonName + " was frozen solid!";
                    return true;
                }
            }
            else if (playerStatusEffects[5]) //Check confusion
            {
                random = UnityEngine.Random.Range(0, 100);
                if (random < 33)
                {
                    displayText.text = playerPokemonName + " snapped out of confusion!";
                    playerPokemonNameText.text = playerPokemonName + " lvl " + playerPokemon.GetComponent<BasePokemon>().returnLevel();
                    playerStatusEffects[5] = false;
                    updateStatusTexts();
                }
                random = UnityEngine.Random.Range(0, 100);
                if (random < 33)
                {
                    displayText.text = playerPokemonName + " hit itself in its \nconfusion!";
                    //(int)((1.0f / 16.0f) * defendingPokemon.GetComponent<BasePokemon>().returnMaxHealth()), true
                    playerPokemon.GetComponent<BasePokemon>().TakeDamage((int)((1.0f / 16.0f) * playerPokemon.GetComponent<BasePokemon>().returnMaxHealth()), true);
                    return true;
                }
            }
        }
        else if (!player)
        {
            if (wildStatusEffects[1]) //Checks for sleep
            {
                random = UnityEngine.Random.Range(0, 100);
                if (random < 33)
                {
                    displayText.text = pokemonName + " woke up from its sleep!";
                    wildStatusEffects[1] = false;
                    defendingPokemonNameText.text = pokemonName + defendingPokemon.GetComponent<BasePokemon>().returnLevel();
                    updateStatusTexts();
                    return false;
                }
                else
                {
                    displayText.text = pokemonName + " is fast asleep!";
                    return true;
                }
            }
            else if (wildStatusEffects[2])//Check paralysis
            {
                random = UnityEngine.Random.Range(0, 100);
                if (random < 25)
                {
                    displayText.text = pokemonName + " was paralyzed. \nIt can't move!";
                    return true;
                }
            }
            else if (wildStatusEffects[3]) //Check Frozen
            {
                random = UnityEngine.Random.Range(0, 100);
                if (random < 33)
                {
                    displayText.text = pokemonName + " was thawed out!";
                    defendingPokemonNameText.text = pokemonName + defendingPokemon.GetComponent<BasePokemon>().returnLevel();
                    wildStatusEffects[3] = false;
                    updateStatusTexts();
                }
                else
                {
                    displayText.text = pokemonName + " was frozen solid!";
                    return true;
                }
            }
            else if (wildStatusEffects[5]) //Check confusion
            {
                random = UnityEngine.Random.Range(0, 100);
                if (random < 33)
                {
                    displayText.text = pokemonName + " snapped out of confusion!";
                    defendingPokemonNameText.text = pokemonName + defendingPokemon.GetComponent<BasePokemon>().returnLevel();
                    wildStatusEffects[5] = false;
                    updateStatusTexts();

                }
                random = UnityEngine.Random.Range(0, 100);
                if (random < 33)
                {
                    displayText.text = pokemonName + " hit itself in its \nconfusion!";
                    defendingPokemon.GetComponent<BasePokemon>().TakeDamage((int)((1.0f / 16.0f) * defendingPokemon.GetComponent<BasePokemon>().returnMaxHealth()), true);
                    return true;
                }
            }

        }
        updateStatusTexts();
        return false;
    }

    IEnumerator inflictStatusEffect(int moveNumber, bool player) 
    {
        int currentStatusEffect;
        if (!player)
        {
            if (returnCondition(player) != -1) 
            {
                int statusValue = pokemonOne.returnStatusValue(moveNumber);
                currentStatusEffect = returnCondition(player);
                Debug.Log("status value: " + statusValue + "\ncurrentStatusEffect: " + currentStatusEffect);
                if (currentStatusEffect == 0 && statusValue == 0)
                {
                    displayText.text = playerPokemonName + " is already poisoned!";
                }
                else if (currentStatusEffect == 1 && statusValue == 1)
                {
                    displayText.text = playerPokemonName + " is already asleep!";
                }
                else if (currentStatusEffect == 2 && statusValue == 2)
                {
                    displayText.text = playerPokemonName + " is already paralyzed!";
                }
                else if (currentStatusEffect == 3 && statusValue == 3)
                {
                    displayText.text = playerPokemonName + " is already frozen!";
                }
                else if (currentStatusEffect == 4 && statusValue == 4)
                {
                    displayText.text = playerPokemonName + " is already burned!";
                }
                else if (currentStatusEffect == 5 && statusValue == 5)
                {
                    displayText.text = playerPokemonName + " is already confused!";
                }
                else if(!pokemonOne.isSpecial(moveNumber) || pokemonOne.isPhysical(moveNumber))
                {
                    displayText.text = name + " has failed!";
                }
            }
            else
            {
                defendingType1 = playerPokemon.GetComponent<BasePokemon>().returnType1();
                defendingType2 = playerPokemon.GetComponent<BasePokemon>().returnType2();

                int statusValue = pokemonOne.returnStatusValue(moveNumber);
                Debug.Log("The status value was + " + statusValue);
                playerPokemon.GetComponent<BasePokemon>().setConditions(playerStatusEffects);
                playerStatusEffects[statusValue] = true;
                if (statusValue == 0)  //Poison
                {
                    if (defendingType1.Equals("Poison") || defendingType2.Equals("Poison") || defendingType1.Equals("Steel") || defendingType2.Equals("Steel"))
                    {
                        displayText.text = name + " has failed!";
                        playerStatusEffects[0] = false;
                    }
                    else
                    {
                        displayText.text = playerPokemon.GetComponent<BasePokemon>().returnName() + " is now poisoned!";
                        playerPokemonNameText.text = playerPokemonName + " lvl " + playerPokemon.GetComponent<BasePokemon>().returnLevel();
                    }
                }
                else if (statusValue == 1) // Sleep
                {
                    displayText.text = playerPokemon.GetComponent<BasePokemon>().returnName() + " is now asleep!";
                    playerPokemonNameText.text = playerPokemonName + " lvl " + playerPokemon.GetComponent<BasePokemon>().returnLevel();
                }
                else if (statusValue == 2) //Paralysis
                {
                    displayText.text = playerPokemon.GetComponent<BasePokemon>().returnName() + " became paralyzed!";
                    playerPokemonNameText.text = playerPokemonName + " lvl " + playerPokemon.GetComponent<BasePokemon>().returnLevel();
                }
                else if (statusValue == 3) // Freeze
                {
                    displayText.text = playerPokemon.GetComponent<BasePokemon>().returnName() + " is frozen solid!";
                    playerPokemonNameText.text = playerPokemonName + " lvl " + playerPokemon.GetComponent<BasePokemon>().returnLevel();
                }
                else if (statusValue == 4) //Burn
                {
                    if (defendingType1.Equals("Fire") || defendingType2.Equals("Fire"))
                    {
                        displayText.text = name + " has failed!";
                        playerStatusEffects[4] = false;
                    }
                    else
                    {
                        displayText.text = playerPokemon.GetComponent<BasePokemon>().returnName() + " is now burned!";
                        playerPokemonNameText.text = playerPokemonName + " lvl " + playerPokemon.GetComponent<BasePokemon>().returnLevel();
                    }
                }
                else if (statusValue == 5) //Confusion
                {
                    displayText.text = playerPokemon.GetComponent<BasePokemon>().returnName() + " became confused!";
                    playerPokemonNameText.text = playerPokemonName + " lvl " + playerPokemon.GetComponent<BasePokemon>().returnLevel();
                }
            }
        }
        else if (player)
        {
            if (returnCondition(!player) != -1) 
            {
                int statusValue = pokemonTwo.returnStatusValue(moveNumber);
                currentStatusEffect = returnCondition(!player);
                if (currentStatusEffect == 0 && statusValue == 0)
                {
                    displayText.text = pokemonName + " is already poisoned!";
                }
                else if (currentStatusEffect == 1 && statusValue == 1)
                {
                    displayText.text = pokemonName + " is already asleep!";
                }
                else if (currentStatusEffect == 1 && statusValue == 1)
                {
                    displayText.text = pokemonName + " is already paralyzed!";
                }
                else if (currentStatusEffect == 3 && statusValue == 3)
                {
                    displayText.text = pokemonName + " is already frozen!";
                }
                else if (currentStatusEffect == 4 && statusValue == 4)
                {
                    displayText.text = pokemonName + " is already burned!";
                }
                else if (currentStatusEffect == 5 && statusValue == 5)
                {
                    displayText.text = pokemonName + " is already confused!";
                }
                else if(!pokemonTwo.isSpecial(moveNumber) || pokemonTwo.isPhysical(moveNumber))
                {
                    Debug.Log("IT GOT HERE");
                    displayText.text = name + " has failed!";
                }
            }
            else
            {
                defendingType1 = defendingPokemon.GetComponent<BasePokemon>().returnType1();
                defendingType2 = defendingPokemon.GetComponent<BasePokemon>().returnType2();

                int statusValue = pokemonTwo.returnStatusValue(moveNumber);
                Debug.Log("The status value was " + statusValue);
                wildStatusEffects[statusValue] = true;
                if (statusValue == 0)    //Poison
                {
                    if (defendingType1.Equals("Poison") || defendingType2.Equals("Poison") || defendingType1.Equals("Steel") || defendingType2.Equals("Steel"))
                    {
                        displayText.text = name + " has failed!";
                        playerStatusEffects[0] = false;
                       // yield return new WaitForSeconds(1.5f);
                    }
                    else
                    {
                        displayText.text = defendingPokemon.GetComponent<BasePokemon>().returnName() + " is now poisoned!";
                        defendingPokemonNameText.text = defendingPokemonNameText.text;
                      //  yield return new WaitForSeconds(1.5f);
                    }

                }
                else if (statusValue == 1) // Sleep
                {
                    displayText.text = defendingPokemon.GetComponent<BasePokemon>().returnName() + " is now asleep!";
                    defendingPokemonNameText.text = defendingPokemonNameText.text;
                  //  yield return new WaitForSeconds(1.5f);
                }
                else if (statusValue == 2) //Paralysis
                {
                    displayText.text = defendingPokemon.GetComponent<BasePokemon>().returnName() + " became paralyzed!";
                    defendingPokemonNameText.text = defendingPokemonNameText.text;
                  //  yield return new WaitForSeconds(1.5f);
                }
                else if (statusValue == 3) // Freeze
                {
                    displayText.text = defendingPokemon.GetComponent<BasePokemon>().returnName() + " is frozen solid!";
                    defendingPokemonNameText.text = defendingPokemonNameText.text;
                   // yield return new WaitForSeconds(1.5f);
                }
                else if (statusValue == 4) //Burn
                {
                    if (defendingType1.Equals("Fire") || defendingType2.Equals("Fire"))
                    {
                        displayText.text = name + " has failed!";
                        wildStatusEffects[4] = false;
                    //    yield return new WaitForSeconds(1.5f);
                    }
                    else
                    {
                        displayText.text = defendingPokemon.GetComponent<BasePokemon>().returnName() + " is now burned!";
                        defendingPokemonNameText.text = defendingPokemonNameText.text;
                      // yield return new WaitForSeconds(1.5f);
                    }
                }
                else if (statusValue == 5) //Confusion
                {
                  displayText.text = defendingPokemon.GetComponent<BasePokemon>().returnName() + " became confused!";
                  defendingPokemonNameText.text = defendingPokemonNameText.text;
                  waitTime += 2.0f;
                  //  yield return new WaitForSeconds(1.5f);
                }
            }
        }
        updateStatusTexts();
        yield break;
    }

    IEnumerator inflictStatChange(int moveNumber, bool player)  //Make sure that a pokemons stat stage cannot be past 6/-6
    {
        int stageChanger; 
        int statEffected; 
        bool self;

        if (player)
        {
            stageChanger = pokemonTwo.returnStageChanger(moveNumber);
            statEffected = pokemonTwo.returnStatValue(moveNumber);
            self = pokemonTwo.isAffectsSelf(moveNumber);
            Debug.Log("It got here\nstat effected = " + statEffected);
            if (statEffected == 0) //Attack
            {
                if (!(playerAttackStage + stageChanger > 6 || wildAttackStage - stageChanger < -6))       //Do this with every other stat
                {
                    if (self)
                    {
                        playerAttackStage += stageChanger;
                        displayText.text = playerPokemonName + "'s attack rose!";
                        yield return new WaitForSeconds(1.5f);
                    }
                    else
                    {
                        wildAttackStage -= stageChanger;
                        displayText.text = pokemonName + "'s attack fell!";
                        yield return new WaitForSeconds(1.5f);
                    }
                }
                else
                {
                    playerAttackStage = 6;
                    displayText.text = playerPokemonName + "'s attack cannot go any higher!";
                    yield return new WaitForSeconds(1.5f);
                }
            }
            else if (statEffected == 1) //Defense
            {
                if (!(playerDefenseStage + stageChanger > 6 || wildDefenseStage - stageChanger < -6))
                {
                    if (self)
                    {
                        playerDefenseStage += stageChanger;
                        displayText.text = playerPokemonName + "'s defense rose!";
                        yield return new WaitForSeconds(1.5f);
                    }
                    else
                    {
                        wildDefenseStage -= stageChanger;
                        displayText.text = pokemonName + "'s defense fell!";
                        yield return new WaitForSeconds(1.5f);
                    }
                }
            }
            else if (statEffected == 2) //Special Attack
            {
                if (!(playerSpAttackStage + stageChanger > 6 || wildSpAttackStage - stageChanger < -6))       
                {
                    if (self)
                    {
                        playerSpAttackStage += stageChanger;
                        displayText.text = playerPokemonName + "'s special attack rose!";
                        yield return new WaitForSeconds(1.5f);
                    }
                    else
                    {
                        wildSpAttackStage -= stageChanger;
                        displayText.text = pokemonName + "'s special attack fell!";
                        yield return new WaitForSeconds(1.5f);
                    }
                }
            }
            else if (statEffected == 3) //SpecialDefense
            {
                if (!(playerSpDefenseStage + stageChanger > 6 || wildSpDefenseStage - stageChanger < -6))
                {
                    if (self)
                    {
                        playerSpDefenseStage += stageChanger;
                        displayText.text = playerPokemonName + "'s special defense rose!";
                        yield return new WaitForSeconds(1.5f);
                    }
                    else
                    {
                        wildSpDefenseStage -= stageChanger;
                        displayText.text = pokemonName + "'s special defense fell!";
                        yield return new WaitForSeconds(1.5f);
                    }
                }
            }
            else if (statEffected == 4) //Speed
            {
                if (!(playerSpeedStage + stageChanger > 6 || wildSpeedStage - stageChanger < -6))
                {
                    if (self)
                    {
                        playerSpeedStage += stageChanger;
                        displayText.text = playerPokemonName + "'s speed rose!";
                        yield return new WaitForSeconds(1.5f);
                    }
                    else
                    {
                        wildSpeedStage -= stageChanger;
                        displayText.text = pokemonName + "'s speed fell!";
                        yield return new WaitForSeconds(1.5f);
                    }
                }
            }
            else if (statEffected == 5) //Accuracy
            {
                if (!(playerAccuracyStage + stageChanger > 6 || wildAccuracyStage - stageChanger < -6))
                {
                    if (self)
                    {
                        playerAccuracyStage += stageChanger;
                        displayText.text = playerPokemonName + "'s accuracy rose!";
                        yield return new WaitForSeconds(1.5f);
                    }
                    else
                    {
                        wildAccuracyStage -= stageChanger;
                        displayText.text = pokemonName + "'s accuracy fell!";
                        yield return new WaitForSeconds(1.5f);
                    }
                }
            }
        }
        else if (!player)
        {
            stageChanger = pokemonOne.returnStageChanger(moveNumber);
            statEffected = pokemonOne.returnStatValue(moveNumber);
            self = pokemonOne.isAffectsSelf(moveNumber);

            if (statEffected == 0) //Attack
            {
                if (!(wildAttackStage + stageChanger > 6 || playerAttackStage - stageChanger < -6))      
                {
                    if (self)
                    {
                        wildAttackStage += stageChanger;
                        displayText.text = pokemonName + "'s attack rose!";
                        yield return new WaitForSeconds(1.5f);
                    }
                    else
                    {
                        playerAttackStage -= stageChanger;
                        displayText.text = playerPokemonName + "'s attack fell!";
                        yield return new WaitForSeconds(1.5f);
                    }
                }
                else
                {
                    wildAttackStage = 6;
                    displayText.text = pokemonName + "'s attack cannot go any higher!";
                    yield return new WaitForSeconds(1.5f);
                }
            }
            else if (statEffected == 1) //Defense
            {
                if (!(wildDefenseStage + stageChanger > 6 || playerDefenseStage - stageChanger < -6))
                {
                    if (self)
                    {
                        wildDefenseStage += stageChanger;
                        displayText.text = pokemonName + "'s defense rose!";
                        yield return new WaitForSeconds(1.5f);
                    }
                    else
                    {
                        playerDefenseStage -= stageChanger;
                        displayText.text =playerPokemonName + "'s defense fell!";
                        yield return new WaitForSeconds(1.5f);
                    }
                }
            }
            else if (statEffected == 2) //Special Attack
            {
                if (!(wildSpAttackStage + stageChanger > 6 || playerSpAttackStage - stageChanger < -6))       
                {
                    if (self)
                    {
                        wildSpAttackStage += stageChanger;
                        displayText.text = pokemonName + "'s special attack rose!";
                        yield return new WaitForSeconds(1.5f);
                    }
                    else
                    {
                        playerAttackStage -= stageChanger;
                        displayText.text = playerPokemonName + "'s special attack fell!";
                        yield return new WaitForSeconds(1.5f);
                    }
                }
            }
            else if (statEffected == 3) //SpecialDefense
            {
                if (!(wildSpDefenseStage + stageChanger > 6 || playerSpDefenseStage - stageChanger < -6))
                {
                    if (self)
                    {
                        wildSpDefenseStage += stageChanger;
                        displayText.text = pokemonName + "'s special defense rose!";
                        yield return new WaitForSeconds(1.5f);
                    }
                    else
                    {
                        playerSpDefenseStage -= stageChanger;
                        displayText.text = playerPokemonName + "'s special defense fell!";
                        yield return new WaitForSeconds(1.5f);
                    }
                }
            }
            else if (statEffected == 4) //Speed
            {
                if (!(wildSpeedStage + stageChanger > 6 || playerSpeedStage - stageChanger < -6))
                {
                    if (self)
                    {
                        wildSpeedStage += stageChanger;
                        displayText.text = pokemonName + "'s speed rose!";
                        yield return new WaitForSeconds(1.5f);
                    }
                    else
                    {
                        playerSpeedStage -= stageChanger;
                        displayText.text = playerPokemonName + "'s speed fell!";
                        yield return new WaitForSeconds(1.5f);
                    }
                }
            }
            else if (statEffected == 5) //Accuracy
            {
                if (!(wildAccuracyStage + stageChanger > 6 || playerAccuracyStage - stageChanger < -6))
                {
                    if (self)
                    {
                        wildAccuracyStage += stageChanger;
                        displayText.text = pokemonName + "'s accuracy rose!";
                        yield return new WaitForSeconds(1.5f);
                    }
                    else
                    {
                        playerAccuracyStage -= stageChanger;
                        displayText.text = playerPokemonName + "'s accuracy fell!";
                        yield return new WaitForSeconds(1.5f);
                    }
                }
            }
        }

        yield break;
    }

    public float stageValueToModifier(int stageValue)
    {
        if (stageValue == -6)
        {
            return 0.25f;
        }
        else if (stageValue == -5)
        {
            return 0.28f;
        }
        else if (stageValue == -4)
        {
            return 0.33f;
        }
        else if (stageValue == -3)
        {
            return 0.4f;
        }
        else if (stageValue == -2)
        {
            return 0.5f;
        }
        else if (stageValue == -1)
        {
            return 0.66f;
        }
        else if (stageValue == 0)
        {
            return 1;
        }
        else if (stageValue == 1)
        {
            return 1.50f;
        }
        else if (stageValue == 2)
        {
            return 2.0f;
        }
        else if (stageValue == 3)
        {
            return 2.5f;
        }
        else if (stageValue == 4)
        {
            return 3.0f;
        }
        else if (stageValue == 5)
        {
            return 3.5f;
        }
        else if (stageValue == 6)
        {
            return 4.0f;
        }
        return 1;
    }

    public float accuracyValueToModifier(int stageValue)
    {
        if (stageValue == -6)
        {
            return 0.33f;
        }
        else if (stageValue == -5)
        {
            return 0.36f;
        }
        else if (stageValue == -4)
        {
            return 0.43f;
        }
        else if (stageValue == -3)
        {
            return 0.5f;
        }
        else if (stageValue == -2)
        {
            return 0.6f;
        }
        else if (stageValue == -1)
        {
            return 0.75f;
        }
        else if (stageValue == 0)
        {
            return 1;
        }
        else if (stageValue == 1)
        {
            return 1.33f;
        }
        else if (stageValue == 2)
        {
            return 1.66f;
        }
        else if (stageValue == 3)
        {
            return 2.0f;
        }
        else if (stageValue == 4)
        {
            return 2.33f;
        }
        else if (stageValue == 5)
        {
            return 2.66f;
        }
        else if (stageValue == 6)
        {
            return 3.0f;
        }
        return 1;
    }

    public float superEffective(string type, string defendingType1, string defendingType2)
    {
        int defendValue1 = 0, defendValue2 = -1, attackValue = 0;

        if (type.Equals("Normal"))
        {
            attackValue = 0;
        }
        else if (type.Equals("Fight"))
        {
            attackValue = 1;
        }
        else if (type.Equals("Flying"))
        {
            attackValue = 2;
        }
        else if (type.Equals("Poison"))
        {
            attackValue = 3;
        }
        else if (type.Equals("Ground"))
        {
            attackValue = 4;
        }
        else if (type.Equals("Rock"))
        {
            attackValue = 5;
        }
        else if (type.Equals("Bug"))
        {
            attackValue = 6;
        }
        else if (type.Equals("Ghost"))
        {
            attackValue = 7;
        }
        else if (type.Equals("Steel"))
        {
            attackValue = 8;
        }
        else if (type.Equals("Fire"))
        {
            attackValue = 9;
        }
        else if (type.Equals("Water"))
        {
            attackValue = 10;
        }
        else if (type.Equals("Grass"))
        {
            attackValue = 11;
        }
        else if (type.Equals("Electric"))
        {
            attackValue = 12;
        }
        else if (type.Equals("Psychic"))
        {
            attackValue = 13;
        }
        else if (type.Equals("Ice"))
        {
            attackValue = 14;
        }
        else if (type.Equals("Dragon"))
        {
            attackValue = 15;
        }
        else if (type.Equals("Dark"))
        {
            attackValue = 16;
        }

        if (defendingType1.Equals("Normal"))
        {
            defendValue1 = 0;
        }
        else if (defendingType1.Equals("Fight"))
        {
            defendValue1 = 1;
        }
        else if (defendingType1.Equals("Flying"))
        {
            defendValue1 = 2;
        }
        else if (defendingType1.Equals("Poison"))
        {
            defendValue1 = 3;
        }
        else if (defendingType1.Equals("Ground"))
        {
            defendValue1 = 4;
        }
        else if (defendingType1.Equals("Rock"))
        {
            defendValue1 = 5;
        }
        else if (defendingType1.Equals("Bug"))
        {
            defendValue1 = 6;
        }
        else if (defendingType1.Equals("Ghost"))
        {
            defendValue1 = 7;
        }
        else if (defendingType1.Equals("Steel"))
        {
            defendValue1 = 8;
        }
        else if (defendingType1.Equals("Fire"))
        {
            defendValue1 = 9;
        }
        else if (defendingType1.Equals("Water"))
        {
            defendValue1 = 10;
        }
        else if (defendingType1.Equals("Grass"))
        {
            defendValue1 = 11;
        }
        else if (defendingType1.Equals("Electric"))
        {
            defendValue1 = 12;
        }
        else if (defendingType1.Equals("Psychic"))
        {
            defendValue1 = 13;
        }
        else if (defendingType1.Equals("Ice"))
        {
            defendValue1 = 14;
        }
        else if (defendingType1.Equals("Dragon"))
        {
            defendValue1 = 15;
        }
        else if (defendingType1.Equals("Dark"))
        {
            defendValue1 = 16;
        }


        if (defendingType2.Equals("Normal"))
        {
            defendValue2 = 0;
        }
        else if (defendingType2.Equals("Fight"))
        {
            defendValue2 = 1;
        }
        else if (defendingType2.Equals("Flying"))
        {
            defendValue2 = 2;
        }
        else if (defendingType2.Equals("Poison"))
        {
            defendValue2 = 3;
        }
        else if (defendingType2.Equals("Ground"))
        {
            defendValue2 = 4;
        }
        else if (defendingType2.Equals("Rock"))
        {
            defendValue2 = 5;
        }
        else if (defendingType2.Equals("Bug"))
        {
            defendValue2 = 6;
        }
        else if (defendingType2.Equals("Ghost"))
        {
            defendValue2 = 7;
        }
        else if (defendingType2.Equals("Steel"))
        {
            defendValue2 = 8;
        }
        else if (defendingType2.Equals("Fire"))
        {
            defendValue2 = 9;
        }
        else if (defendingType2.Equals("Water"))
        {
            defendValue2 = 10;
        }
        else if (defendingType2.Equals("Grass"))
        {
            defendValue2 = 11;
        }
        else if (defendingType2.Equals("Electric"))
        {
            defendValue2 = 12;
        }
        else if (defendingType2.Equals("Psychic"))
        {
            defendValue2 = 13;
        }
        else if (defendingType2.Equals("Ice"))
        {
            defendValue2 = 14;
        }
        else if (defendingType2.Equals("Dragon"))
        {
            defendValue2 = 15;
        }
        else if (defendingType2.Equals("Dark"))
        {
            defendValue2 = 16;
        }

        if (defendValue2 == -1)
        {
            return effectivenessArray[attackValue, defendValue1];
        }
        else
        {
            return (effectivenessArray[attackValue, defendValue1] * effectivenessArray[attackValue, defendValue2]);
        }

    }

    //TEST METHOD

    public void nextPokemon()
    {
        playerPokemon.transform.eulerAngles = new Vector3(0, 180, 0);
        pokemonButton1.gameObject.SetActive(false);
        pokemonButton2.gameObject.SetActive(false);
        pokemonButton3.gameObject.SetActive(false);
        pokemonButton4.gameObject.SetActive(false);
        pokemonButton5.gameObject.SetActive(false);
        pokemonButton6.gameObject.SetActive(false);
        selectPokemonText.gameObject.SetActive(false);
        returnSwitchPokemonButton.gameObject.SetActive(false);

        playerAttackStage = 0;
        playerDefenseStage = 0;
        playerSpeedStage = 0;
        playerSpAttackStage = 0;
        playerSpDefenseStage = 0;
        playerAccuracyStage = 0;

        playerPokemonName = playerPokemon.GetComponent<BasePokemon>().returnName();
        speed = playerPokemon.GetComponent<BasePokemon>().returnSpeed();

        moveButton1.GetComponentInChildren<Text>().text = pokemonTwo.returnMoveName(0);
        moveButton2.GetComponentInChildren<Text>().text = pokemonTwo.returnMoveName(1);
        moveButton3.GetComponentInChildren<Text>().text = pokemonTwo.returnMoveName(2);
        moveButton4.GetComponentInChildren<Text>().text = pokemonTwo.returnMoveName(3);

        playerStatusEffects = playerPokemon.GetComponent<BasePokemon>().returnConditions();
        playerFainted = false;
        defendingFainted = false;

        displayText.text = "You sent out " + playerPokemonName + "!";

        updatePlayerPokemonHealthBar();
        updateExperienceBar();
        playerPokemonNameText.text = playerPokemonName + " Lvl: " + playerPokemon.GetComponent<BasePokemon>().returnLevel();   
            
        updateStatusTexts();

        if (previousPokemonFainted == false)
        {
            countAttacks++;
            StartCoroutine(otherPokemonUseMove(false));   //This is the line of code I added
        }
        else
        {
            turnFinished();
        }
    }

    public void playerSelectNextPokemon()
    {
        if (playerFainted)
        {
            Debug.Log("I got to the player Select Next Pokemon Method!");
            if (fpsController.GetComponent<PokemonManager>().pokemonArray[0] == null || fpsController.GetComponent<PokemonManager>().pokemonArray[0].GetComponent<BasePokemon>().isFainted())
            {
                Debug.Log("Pokemon One Has Died!");
                //Debug.Log(fpsController.GetComponent<PokemonManager>().pokemonArray[0].GetComponent<BasePokemon>().returnCurrentHealth());
                if (fpsController.GetComponent<PokemonManager>().pokemonArray[1] == null || fpsController.GetComponent<PokemonManager>().pokemonArray[1].GetComponent<BasePokemon>().isFainted())
                {
                    //Debug.Log(fpsController.GetComponent<PokemonManager>().pokemonArray[1].GetComponent<BasePokemon>().returnCurrentHealth());
                    if (fpsController.GetComponent<PokemonManager>().pokemonArray[2] == null || fpsController.GetComponent<PokemonManager>().pokemonArray[2].GetComponent<BasePokemon>().isFainted())
                    {
                        if (fpsController.GetComponent<PokemonManager>().pokemonArray[3] == null || fpsController.GetComponent<PokemonManager>().pokemonArray[3].GetComponent<BasePokemon>().isFainted())
                        {
                            
                            if (fpsController.GetComponent<PokemonManager>().pokemonArray[4] == null || fpsController.GetComponent<PokemonManager>().pokemonArray[4].GetComponent<BasePokemon>().isFainted())
                            {
                                
                                if (fpsController.GetComponent<PokemonManager>().pokemonArray[5] == null || fpsController.GetComponent<PokemonManager>().pokemonArray[5].GetComponent<BasePokemon>().isFainted())
                                {
                                    
                                    Debug.Log("all the pokemon have died.");
                                    fpsController.SetActive(true);
                                    if (!trainer)
                                    {
                                        Destroy(defendingPokemon);
                                    }
                                    SceneManager.LoadScene("Scene");

                                }
                                else
                                {
                                    returnSwitchPokemonButton.gameObject.SetActive(true);
                                }
                            }
                            else
                            {
                                returnSwitchPokemonButton.gameObject.SetActive(true);
                            }
                        }
                        else
                        {
                            returnSwitchPokemonButton.gameObject.SetActive(true);
                        }
                    }
                    else
                    {
                        returnSwitchPokemonButton.gameObject.SetActive(true);
                    }
                }
                else
                {
                    returnSwitchPokemonButton.gameObject.SetActive(true);
                }
            }
            else
            {
                returnSwitchPokemonButton.gameObject.SetActive(true);
            }


        }

        if (!playerFainted)
        {
            returnSwitchPokemonButton.gameObject.SetActive(true);
        }

        selectPokemonButtons.gameObject.SetActive(true);

        if (playerPokemonCount == 6)
        {
            pokemonButton1.gameObject.SetActive(true);
            pokemonButton2.gameObject.SetActive(true);
            pokemonButton3.gameObject.SetActive(true);
            pokemonButton4.gameObject.SetActive(true);
            pokemonButton5.gameObject.SetActive(true);
            pokemonButton6.gameObject.SetActive(true);

            pokemonButton1.GetComponentInChildren<Text>().text = playerPokemonArray[0].GetComponent<BasePokemon>().returnName() + " (" + playerPokemonArray[0].GetComponent<BasePokemon>().returnCurrentHealth() + "/" + playerPokemonArray[0].GetComponent<BasePokemon>().returnMaxHealth() + ")";
            pokemonButton2.GetComponentInChildren<Text>().text = playerPokemonArray[1].GetComponent<BasePokemon>().returnName() + " (" + playerPokemonArray[1].GetComponent<BasePokemon>().returnCurrentHealth() + "/" + playerPokemonArray[1].GetComponent<BasePokemon>().returnMaxHealth() + ")";
            pokemonButton3.GetComponentInChildren<Text>().text = playerPokemonArray[2].GetComponent<BasePokemon>().returnName() + " (" + playerPokemonArray[2].GetComponent<BasePokemon>().returnCurrentHealth() + "/" + playerPokemonArray[2].GetComponent<BasePokemon>().returnMaxHealth() + ")";
            pokemonButton4.GetComponentInChildren<Text>().text = playerPokemonArray[3].GetComponent<BasePokemon>().returnName() + " (" + playerPokemonArray[3].GetComponent<BasePokemon>().returnCurrentHealth() + "/" + playerPokemonArray[3].GetComponent<BasePokemon>().returnMaxHealth() + ")";
            pokemonButton5.GetComponentInChildren<Text>().text = playerPokemonArray[4].GetComponent<BasePokemon>().returnName() + " (" + playerPokemonArray[4].GetComponent<BasePokemon>().returnCurrentHealth() + "/" + playerPokemonArray[4].GetComponent<BasePokemon>().returnMaxHealth() + ")";
            pokemonButton6.GetComponentInChildren<Text>().text = playerPokemonArray[5].GetComponent<BasePokemon>().returnName() + " (" + playerPokemonArray[5].GetComponent<BasePokemon>().returnCurrentHealth() + "/" + playerPokemonArray[5].GetComponent<BasePokemon>().returnMaxHealth() + ")";
            //GetComponent<PokemonManager>().setPokemonNumber(1, GameObject.FindWithTag("Player"));
        }
        else if (playerPokemonCount == 5)
        {
            pokemonButton1.gameObject.SetActive(true);
            pokemonButton2.gameObject.SetActive(true);
            pokemonButton3.gameObject.SetActive(true);
            pokemonButton4.gameObject.SetActive(true);
            pokemonButton5.gameObject.SetActive(true);
            pokemonButton6.gameObject.SetActive(false);

            pokemonButton1.GetComponentInChildren<Text>().text = playerPokemonArray[0].GetComponent<BasePokemon>().returnName() + " (" + playerPokemonArray[0].GetComponent<BasePokemon>().returnCurrentHealth() + "/" + playerPokemonArray[0].GetComponent<BasePokemon>().returnMaxHealth() + ")";
            pokemonButton2.GetComponentInChildren<Text>().text = playerPokemonArray[1].GetComponent<BasePokemon>().returnName() + " (" + playerPokemonArray[1].GetComponent<BasePokemon>().returnCurrentHealth() + "/" + playerPokemonArray[1].GetComponent<BasePokemon>().returnMaxHealth() + ")";
            pokemonButton3.GetComponentInChildren<Text>().text = playerPokemonArray[2].GetComponent<BasePokemon>().returnName() + " (" + playerPokemonArray[2].GetComponent<BasePokemon>().returnCurrentHealth() + "/" + playerPokemonArray[2].GetComponent<BasePokemon>().returnMaxHealth() + ")";
            pokemonButton4.GetComponentInChildren<Text>().text = playerPokemonArray[3].GetComponent<BasePokemon>().returnName() + " (" + playerPokemonArray[3].GetComponent<BasePokemon>().returnCurrentHealth() + "/" + playerPokemonArray[3].GetComponent<BasePokemon>().returnMaxHealth() + ")";
            pokemonButton5.GetComponentInChildren<Text>().text = playerPokemonArray[4].GetComponent<BasePokemon>().returnName() + " (" + playerPokemonArray[4].GetComponent<BasePokemon>().returnCurrentHealth() + "/" + playerPokemonArray[4].GetComponent<BasePokemon>().returnMaxHealth() + ")";

        }
        else if (playerPokemonCount == 4)
        {
            pokemonButton1.gameObject.SetActive(true);
            pokemonButton2.gameObject.SetActive(true);
            pokemonButton3.gameObject.SetActive(true);
            pokemonButton4.gameObject.SetActive(true);
            pokemonButton5.gameObject.SetActive(false);
            pokemonButton6.gameObject.SetActive(false);

            pokemonButton1.GetComponentInChildren<Text>().text = playerPokemonArray[0].GetComponent<BasePokemon>().returnName() + " (" + playerPokemonArray[0].GetComponent<BasePokemon>().returnCurrentHealth() + "/" + playerPokemonArray[0].GetComponent<BasePokemon>().returnMaxHealth() + ")";
            pokemonButton2.GetComponentInChildren<Text>().text = playerPokemonArray[1].GetComponent<BasePokemon>().returnName() + " (" + playerPokemonArray[1].GetComponent<BasePokemon>().returnCurrentHealth() + "/" + playerPokemonArray[1].GetComponent<BasePokemon>().returnMaxHealth() + ")";
            pokemonButton3.GetComponentInChildren<Text>().text = playerPokemonArray[2].GetComponent<BasePokemon>().returnName() + " (" + playerPokemonArray[2].GetComponent<BasePokemon>().returnCurrentHealth() + "/" + playerPokemonArray[2].GetComponent<BasePokemon>().returnMaxHealth() + ")";
            pokemonButton4.GetComponentInChildren<Text>().text = playerPokemonArray[3].GetComponent<BasePokemon>().returnName() + " (" + playerPokemonArray[3].GetComponent<BasePokemon>().returnCurrentHealth() + "/" + playerPokemonArray[3].GetComponent<BasePokemon>().returnMaxHealth() + ")";
        }
        else if (playerPokemonCount == 3)
        {
            pokemonButton1.gameObject.SetActive(true);
            pokemonButton2.gameObject.SetActive(true);
            pokemonButton3.gameObject.SetActive(true);
            pokemonButton4.gameObject.SetActive(false);
            pokemonButton5.gameObject.SetActive(false);
            pokemonButton6.gameObject.SetActive(false);

            pokemonButton1.GetComponentInChildren<Text>().text = playerPokemonArray[0].GetComponent<BasePokemon>().returnName() + " (" + playerPokemonArray[0].GetComponent<BasePokemon>().returnCurrentHealth() + "/" + playerPokemonArray[0].GetComponent<BasePokemon>().returnMaxHealth() + ")";
            pokemonButton2.GetComponentInChildren<Text>().text = playerPokemonArray[1].GetComponent<BasePokemon>().returnName() + " (" + playerPokemonArray[1].GetComponent<BasePokemon>().returnCurrentHealth() + "/" + playerPokemonArray[1].GetComponent<BasePokemon>().returnMaxHealth() + ")";
            pokemonButton3.GetComponentInChildren<Text>().text = playerPokemonArray[2].GetComponent<BasePokemon>().returnName() + " (" + playerPokemonArray[2].GetComponent<BasePokemon>().returnCurrentHealth() + "/" + playerPokemonArray[2].GetComponent<BasePokemon>().returnMaxHealth() + ")";
        }
        else if (playerPokemonCount == 2)
        {
            pokemonButton1.gameObject.SetActive(true);
            pokemonButton2.gameObject.SetActive(true);
            pokemonButton3.gameObject.SetActive(false);
            pokemonButton4.gameObject.SetActive(false);
            pokemonButton5.gameObject.SetActive(false);
            pokemonButton6.gameObject.SetActive(false);

            pokemonButton1.GetComponentInChildren<Text>().text = playerPokemonArray[0].GetComponent<BasePokemon>().returnName() + " (" + playerPokemonArray[0].GetComponent<BasePokemon>().returnCurrentHealth() + "/" + playerPokemonArray[0].GetComponent<BasePokemon>().returnMaxHealth() + ")";
            pokemonButton2.GetComponentInChildren<Text>().text = playerPokemonArray[1].GetComponent<BasePokemon>().returnName() + " (" + playerPokemonArray[1].GetComponent<BasePokemon>().returnCurrentHealth() + "/" + playerPokemonArray[1].GetComponent<BasePokemon>().returnMaxHealth() + ")";
        }
        else
        {
            pokemonButton1.gameObject.SetActive(true);
            pokemonButton2.gameObject.SetActive(false);
            pokemonButton3.gameObject.SetActive(false);
            pokemonButton4.gameObject.SetActive(false);
            pokemonButton5.gameObject.SetActive(false);
            pokemonButton6.gameObject.SetActive(false);

            pokemonButton1.GetComponentInChildren<Text>().text = playerPokemonArray[0].GetComponent<BasePokemon>().returnName() + " (" + playerPokemonArray[0].GetComponent<BasePokemon>().returnCurrentHealth() + "/" + playerPokemonArray[0].GetComponent<BasePokemon>().returnMaxHealth() + ")";
        }
        selectPokemonText.gameObject.SetActive(true);
    }

    public void pokemonOneButtonClick()
    {
        if (!playerPokemonArray[0].GetComponent<BasePokemon>().isFainted())
        {
            previousPokemonFainted = playerPokemon.GetComponent<BasePokemon>().isFainted();
            playerPokemon.SetActive(false);
            playerPokemon = playerPokemonArray[0];
            playerPokemon.SetActive(true);
            GetComponent<PokemonManager>().setPokemonNumber(1, playerPokemon);
            pokemonTwo = playerPokemon.GetComponent<MoveManager>();

            nextPokemon();
        }
        else
        {
            displayText.text = "You cannot send out a fainted Pokemon!";
        }
                                                                                  
    }

    public void pokemonTwoButtonClick()
    {
        if (!playerPokemonArray[1].GetComponent<BasePokemon>().isFainted())
        {
            previousPokemonFainted = playerPokemon.GetComponent<BasePokemon>().isFainted();
            playerPokemon.SetActive(false);
            playerPokemon = playerPokemonArray[1];
            playerPokemon.SetActive(true);
            GetComponent<PokemonManager>().setPokemonNumber(1, playerPokemon);
            pokemonTwo = playerPokemon.GetComponent<MoveManager>();

            nextPokemon();
        }
        else
        {
            displayText.text = "You cannot send out a fainted Pokemon!";
        }
    }

    public void pokemonThreeButtonClick()
    {
        if (!playerPokemonArray[2].GetComponent<BasePokemon>().isFainted())
        {
            previousPokemonFainted = playerPokemon.GetComponent<BasePokemon>().isFainted();
            playerPokemon.SetActive(false);
            playerPokemon = playerPokemonArray[2];
            playerPokemon.SetActive(true);
            GetComponent<PokemonManager>().setPokemonNumber(1, playerPokemon);
            pokemonTwo = playerPokemon.GetComponent<MoveManager>();

            nextPokemon();
        }
        else
        {
            displayText.text = "You cannot send out a fainted Pokemon!";
        }
    }

    public void pokemonFourButtonClick()
    {
        if (!playerPokemonArray[3].GetComponent<BasePokemon>().isFainted())
        {
            previousPokemonFainted = playerPokemon.GetComponent<BasePokemon>().isFainted();
            playerPokemon.SetActive(false);
            playerPokemon = playerPokemonArray[3];
            playerPokemon.SetActive(true);
            GetComponent<PokemonManager>().setPokemonNumber(1, playerPokemon);
            pokemonTwo = playerPokemon.GetComponent<MoveManager>();

            nextPokemon();
        }
        else
        {
            displayText.text = "You cannot send out a fainted Pokemon!";
        }
    }

    public void pokemonFiveButtonClick()
    {
        if (!playerPokemonArray[4].GetComponent<BasePokemon>().isFainted())
        {
            previousPokemonFainted = playerPokemon.GetComponent<BasePokemon>().isFainted();
            playerPokemon.SetActive(false);
            playerPokemon = playerPokemonArray[4];
            playerPokemon.SetActive(true);
            GetComponent<PokemonManager>().setPokemonNumber(1, playerPokemon);
            pokemonTwo = playerPokemon.GetComponent<MoveManager>();

            nextPokemon();
        }
        else
        {
            displayText.text = "You cannot send out a fainted Pokemon!";
        }
    }

    public void pokemonSixButtonClick()
    {
        if (!playerPokemonArray[5].GetComponent<BasePokemon>().isFainted())
        {
            previousPokemonFainted = playerPokemon.GetComponent<BasePokemon>().isFainted();
            playerPokemon.SetActive(false);
            playerPokemon = playerPokemonArray[5];
            playerPokemon.SetActive(true);
            GetComponent<PokemonManager>().setPokemonNumber(1, playerPokemon);
            pokemonTwo = playerPokemon.GetComponent<MoveManager>();

            nextPokemon();
        }
        else
        {
            displayText.text = "You cannot send out a fainted Pokemon!";
        }
    }

    public void updateDefendingPokemonHealthBar()
    {
        defendingPokemonHealthBar.transform.localScale = new Vector3((float)((float)defendingPokemon.GetComponent<BasePokemon>().returnCurrentHealth() / (float)defendingPokemon.GetComponent<BasePokemon>().returnMaxHealth()), 1.0f, 1.0f);
    }

    public void updatePlayerPokemonHealthBar()
    {
        playerPokemonHealthBar.transform.localScale = new Vector3((float)((float)playerPokemon.GetComponent<BasePokemon>().returnCurrentHealth() / (float)playerPokemon.GetComponent<BasePokemon>().returnMaxHealth()), 1.0f, 1.0f);
        playerPokemonHealthText.text = playerPokemon.GetComponent<BasePokemon>().returnCurrentHealth() + "/" + playerPokemon.GetComponent<BasePokemon>().returnMaxHealth();
    }

    public void doPoisonAndBurn(bool player)
    {
        if (!player)
        {
            if (wildStatusEffects[0]) //If poisoned
            {
                defendingPokemon.GetComponent<BasePokemon>().TakeDamage((int)((1.0f / 16.0f) * defendingPokemon.GetComponent<BasePokemon>().returnMaxHealth()), true);
                displayText.text = defendingPokemon.GetComponent<BasePokemon>().returnName() + " is hurt from the poison!";
            }
            else if (wildStatusEffects[4])
            {
                defendingPokemon.GetComponent<BasePokemon>().TakeDamage((int)((1.0f / 16.0f) * defendingPokemon.GetComponent<BasePokemon>().returnMaxHealth()), true);
                displayText.text = defendingPokemon.GetComponent<BasePokemon>().returnName() + " is hurt from its burn!";
            }
            updateDefendingPokemonHealthBar();
        }
        else if (player)
        {
            if (playerStatusEffects[0]) //If poisoned
            {
                playerPokemon.GetComponent<BasePokemon>().TakeDamage((int)((1.0f / 16.0f) * playerPokemon.GetComponent<BasePokemon>().returnMaxHealth()), true);
                displayText.text = playerPokemon.GetComponent<BasePokemon>().returnName() + " is hurt from the poison!";
            }
            else if (playerStatusEffects[4])
            {
                playerPokemon.GetComponent<BasePokemon>().TakeDamage((int)((1.0f / 16.0f) * playerPokemon.GetComponent<BasePokemon>().returnMaxHealth()), true);
                displayText.text = playerPokemon.GetComponent<BasePokemon>().returnName() + " is hurt from its burn!";
            }
            updatePlayerPokemonHealthBar();
        }

    }

    public void runButtonPressed()
    {
        if (!trainer)
        {
            StartCoroutine(runBattle());
        }
        else if (trainer) 
        {
            displayText.text = "You cannot run from a trainer battle!";
        }
    }

    IEnumerator runBattle()
    {
        displayText.text = "You fled the battle!";

        yield return new WaitForSeconds(2.0f);

        Destroy(defendingPokemon);
        endBattle(true);
    }

    public void fightButtonPressed()
    {
        mainButtons.SetActive(false);
        returnButton.gameObject.SetActive(true);
    }

    public void backButtonPressed()
    {
        mainButtons.SetActive(true);
        returnButton.gameObject.SetActive(false);
    }

    public void switchPokemonButtonPressed()
    {
        playerSelectNextPokemon();

        mainButtons.SetActive(false);
        moveButtons.SetActive(false);
    }

    public void returnSwitchPokemonButtonPressed()
    {
        mainButtons.SetActive(true);
        moveButtons.SetActive(true);

        selectPokemonButtons.SetActive(false);
        selectPokemonText.gameObject.SetActive(false);
        returnSwitchPokemonButton.gameObject.SetActive(false);
    }

    public void trainerSendNextPokemon()
    {
        currentTrainerPokemon++;
        defendingPokemon = trainerPokemonArray[currentTrainerPokemon];
        defendingPokemon.transform.eulerAngles = new Vector3(0, 180, 0);
        defendingPokemon.SetActive(true);
        GetComponent<PokemonManager>().setPokemonNumber(0, defendingPokemon);
        pokemonOne = defendingPokemon.GetComponent<MoveManager>();
        defendingPokemon.GetComponent<BasePokemon>().setWild(false);
        pokemonName = defendingPokemon.GetComponent<BasePokemon>().returnName();
        displayText.text = trainerName + " sent out " + pokemonName + "!";
        //defendingFainted = false;       //Recent line of code added.
        updateDefendingPokemonHealthBar();
        defendingPokemonNameText.text = pokemonName + " Lvl: " + defendingPokemon.GetComponent<BasePokemon>().returnLevel();

        for (int i = 0; i <= 5; i++)
        {
            wildStatusEffects[i] = false;
        }

        wildAttackStage = 0;
        wildDefenseStage = 0;
        wildSpAttackStage = 0;
        wildSpDefenseStage = 0;
        wildSpeedStage = 0;
        wildAccuracyStage = 0;

        turnFinished();
    }

    public void calculateSpeed()
    {
        currentSpeed = speed * stageValueToModifier(playerSpeedStage);
        defendingPokemonSpeed = defendingPokemon.GetComponent<BasePokemon>().returnSpeed() * stageValueToModifier(wildSpeedStage);

        if (playerStatusEffects[2])
        {
            currentSpeed = currentSpeed * 0.5f;
        }
        if (wildStatusEffects[2])
        {
            defendingPokemonSpeed = defendingPokemonSpeed * 0.5f;
        }
    }


   public void endBattle(bool ran)
    {
        for (int i = 0; i < playerPokemonArray.Length; i++)
        {
            playerPokemonArray[i].SetActive(false);
            playerPokemonArray[i] = null;
        }
        fpsController.SetActive(true);
        playerPokemon.GetComponent<BasePokemon>().checkEvolve();
        if (!ran)
        {
            itemDrop();
        }
        SceneManager.LoadScene("Scene");
        if (!trainer && !caught)
        {
            Destroy(defendingPokemon);
        }
        if(trainer)
        {
            trainerBool = false;
            Debug.Log(trainerObject);
        }
        Destroy(gameObject);
    }

   public void itemDrop()
   {
       itemDropRan = UnityEngine.Random.Range(0, 100);
       if (itemDropRan > 50 && itemDropRan <= 70)
       {
           fpsController.GetComponent<ItemManager>().itemList[0].setNumber(fpsController.GetComponent<ItemManager>().itemList[0].getNumber() + 1); 
       }
       else if (itemDropRan > 70 && itemDropRan <= 85)
       {
           fpsController.GetComponent<ItemManager>().itemList[1].setNumber(fpsController.GetComponent<ItemManager>().itemList[1].getNumber() + 1); 
       }
       else if (itemDropRan > 85 && itemDropRan <= 100)
       {
           fpsController.GetComponent<ItemManager>().itemList[2].setNumber(fpsController.GetComponent<ItemManager>().itemList[2].getNumber() + 1); 
       }

       pokeballDrop = UnityEngine.Random.Range(0, 100);
       if (pokeballDrop > 50 && pokeballDrop <= 70)
       {
           fpsController.GetComponent<ItemManager>().itemList[3].setNumber(fpsController.GetComponent<ItemManager>().itemList[3].getNumber() + 1); 
       }
       else if (pokeballDrop > 70 && pokeballDrop <= 90)
       {
           fpsController.GetComponent<ItemManager>().itemList[4].setNumber(fpsController.GetComponent<ItemManager>().itemList[4].getNumber() + 1); 
       }
       else if (pokeballDrop > 90 && pokeballDrop <= 100)
       {
           fpsController.GetComponent<ItemManager>().itemList[5].setNumber(fpsController.GetComponent<ItemManager>().itemList[5].getNumber() + 1); 
       }
   }


    private float a;
    private int b;

   
    public void bagButtonClicked()
    {
        moveButtons.SetActive(false);
        mainButtons.SetActive(false);

        
        if(sPotion == 0)
        {
            superPotionButton.gameObject.SetActive(false);

        }
        else
        {
            superPotionButton.gameObject.SetActive(true);
        }
        if(mPotion == 0)
        {
            maxPotionButton.gameObject.SetActive(false);
        }
        else
        {
            maxPotionButton.gameObject.SetActive(true);
        }
        if(fHeal == 0)
        {
            fullHealButton.gameObject.SetActive(false);
        }
        else
        {
            fullHealButton.gameObject.SetActive(true);
        }
        if(pokeball == 0)
        {
            pokeBallButton.gameObject.SetActive(false);
        }
        else
        {
            pokeBallButton.gameObject.SetActive(true);
        }
        if(greatball == 0)
        {
            greatBallButton.gameObject.SetActive(false);
        }
        else
        {
            greatBallButton.gameObject.SetActive(true);
        }
        if(ultraball == 0)
        {
            ultraBallButton.gameObject.SetActive(false);
        }
        else
        {
            ultraBallButton.gameObject.SetActive(true);
        }

        bagButtons.SetActive(true);
        returnButton.gameObject.SetActive(true);


    }

    public void itemBackButton()
    {
        bagButtons.SetActive(false);
        returnButton.gameObject.SetActive(false);
        moveButtons.SetActive(true);
        mainButtons.SetActive(true);
    }

    public void throwPokeball()
    {
        if (fpsController.GetComponent<ItemManager>().itemList[3].getNumber() != 0)
        {

            if (!trainer)
            {
                fpsController.GetComponent<ItemManager>().itemList[3].setNumber(fpsController.GetComponent<ItemManager>().itemList[3].getNumber() - 1);
                StartCoroutine(throwPokeBallStart(1, " Pokeball"));
            }
            else if (trainer)
            {
                displayText.text = "You cannot catch a trainer's Pokemon!";
            }
        }
        else
        {
            displayText.text = "You have no Pokeballs.";
        }
    }

    public void throwGreatBall()
    {
        if (fpsController.GetComponent<ItemManager>().itemList[4].getNumber() != 0)
        {
            if (!trainer)
            {
                fpsController.GetComponent<ItemManager>().itemList[4].setNumber(fpsController.GetComponent<ItemManager>().itemList[4].getNumber() - 1);
                StartCoroutine(throwPokeBallStart(1.5f, " Great Ball"));
            }
            else if (trainer)
            {
                displayText.text = "You cannot catch a trainer's Pokemon!";
            }
        }
        else
        {
            displayText.text = "You have no Great balls.";
        }
    }

    public void throwUltraball()
    {
        if (fpsController.GetComponent<ItemManager>().itemList[5].getNumber() != 0)
        {
            if (!trainer)
            {
                fpsController.GetComponent<ItemManager>().itemList[5].setNumber(fpsController.GetComponent<ItemManager>().itemList[5].getNumber() - 1);
                StartCoroutine(throwPokeBallStart(2, "n Ultra Ball"));
            }
            else if (trainer)
            {
                displayText.text = "You cannot catch a trainer's Pokemon!";
            }
        }
        else
        {
            displayText.text = "You have no Ultra balls.";
        }
    }

    public float returnBonusStatus()
    {
        if (wildStatusEffects[0] || wildStatusEffects[2] || wildStatusEffects[4])
        {
            return 1.5f;
        }
        else if (wildStatusEffects[1] || wildStatusEffects[3])
        {
            return 2;
        }

        return 1;
    }

    IEnumerator throwPokeBallStart(float ball, string ballText)
    {
        mainButtons.SetActive(false);
        moveButtons.SetActive(false);
        bagButtons.SetActive(false);
        returnButton.gameObject.SetActive(false);

        displayText.text = "You threw a" + ballText;
        defendingPokemon.SetActive(false);

        yield return new WaitForSeconds(2.0f);

        int random;

        int maxHealth = defendingPokemon.GetComponent<BasePokemon>().returnMaxHealth();
        int currentHealth = defendingPokemon.GetComponent<BasePokemon>().returnCurrentHealth();
        int rate = defendingPokemon.GetComponent<BasePokemon>().returnCatchRate();
        float bonusBall = ball;
        float bonusStatus = 1;

        countAttacks++;

        a = (((3 * maxHealth - 2 * currentHealth) * rate * bonusBall) / (3 * maxHealth)) * bonusStatus;

        if (a > 255)
        {
            displayText.text = "You captured the wild " + pokemonName + "!";
            yield return new WaitForSeconds(2.0f);
            capturePokemon();
        }
        else
        {
            b = 1048560 / ((int)Math.Sqrt(Math.Sqrt(16711680 / a)));
            random = UnityEngine.Random.Range(0, 65535);

            if (random > b)
            {
                Debug.Log("Failed on first shake");
                defendingPokemon.SetActive(true);
                StartCoroutine(otherPokemonUseMove(true));
                displayText.text = "Oh no, the " + pokemonName + " broke free!";
                yield return new WaitForSeconds(2.0f);
            }
            else
            {
                random = UnityEngine.Random.Range(0, 65535);
                if (random > b)
                {
                    Debug.Log("Failed on second shake");
                    defendingPokemon.SetActive(true);
                    StartCoroutine(otherPokemonUseMove(true));
                    displayText.text = "Oh no, the " + pokemonName + " broke free!";
                    yield return new WaitForSeconds(2.0f);
                }
                else
                {
                    random = UnityEngine.Random.Range(0, 65535);
                    if (random > b)
                    {
                        Debug.Log("Failed on third shake");
                        defendingPokemon.SetActive(true);
                        StartCoroutine(otherPokemonUseMove(true));
                        displayText.text = "Oh no, the " + pokemonName + " broke free!\nShoot! It was so close too.";
                        yield return new WaitForSeconds(2.0f);
                    }
                    else
                    {
                        displayText.text = "You captured the wild " + pokemonName + "!";
                        Debug.Log("You captured the Pokemon!");
                        yield return new WaitForSeconds(2.0f);
                        capturePokemon();
                    }
                }
            }

        }

        yield break;

    }

    public void capturePokemon()
    {
        caught = true;
        defendingPokemon.tag = "Player";

        if (fpsController.GetComponent<PokemonManager>().returnPokemonNumber(5) == null)
        {
            for (int i = 1; i <= 5; i++)
            {
                if (fpsController.GetComponent<PokemonManager>().returnPokemonNumber(i) == null)
                {
                    fpsController.GetComponent<PokemonManager>().setPokemonNumber(i, defendingPokemon);
                    Debug.Log("Your Pokemon went into the " + i + "th spot");
                    i = 6;
                }
            }
        }
        else
        {
            Debug.Log("Put it in the other array");
            fpsController.GetComponent<PokemonManager>().addToBox(defendingPokemon);
        }
        endBattle(false);
    }

    public void maxPotion()
    {
        if (fpsController.GetComponent<ItemManager>().itemList[1].getNumber() != 0)
        {
            playerPokemon.GetComponent<BasePokemon>().setCurrentHealth(playerPokemon.GetComponent<BasePokemon>().returnMaxHealth());
            Debug.Log(playerPokemon.GetComponent<BasePokemon>().returnCurrentHealth());
            updatePlayerPokemonHealthBar();
            fpsController.GetComponent<ItemManager>().itemList[1].setNumber(fpsController.GetComponent<ItemManager>().itemList[1].getNumber() - 1);
        }
        else
        {
            displayText.text = "You have no Max Potions.";
        }
    }
    public void backButton()
    {
        bagButtons.SetActive(false);
    }

    public void fullHeal()
    {
        if (fpsController.GetComponent<ItemManager>().itemList[2].getNumber() != 0)
        {
            for (int i = 0; i < playerStatusEffects.Length; i++)
            {
                playerStatusEffects[i] = false;
            }
            Debug.Log("All the status effects have been cleared!");
            playerPokemonNameText.text = playerPokemonName;
            fpsController.GetComponent<ItemManager>().itemList[2].setNumber(fpsController.GetComponent<ItemManager>().itemList[2].getNumber() - 1);
        }
        else
        {
            displayText.text = "You have no Full Heals.";
        }
    }

    IEnumerator maxPotionText()
    {
        if (fpsController.GetComponent<ItemManager>().itemList[1].getNumber() != 0)
        {
            countAttacks++;
            StartCoroutine(otherPokemonUseMove(false));
            returnButton.gameObject.SetActive(false);
            displayText.text = playerPokemonName + " has been fully healed!";
            bagButtons.SetActive(false);

            yield return new WaitForSeconds(2.0f);
        }
        

    }

    public void sayMaxPotion()
    {
        StartCoroutine(maxPotionText());
    }

    public void sayFullHeal()
    {
        StartCoroutine(fullHealText());
    }


    IEnumerator fullHealText()
    {
        if (fpsController.GetComponent<ItemManager>().itemList[2].getNumber() != 0)
        {
            countAttacks++;
            StartCoroutine(otherPokemonUseMove(false));
            returnButton.gameObject.SetActive(false);
            displayText.text = playerPokemonName + " has been cleared off all status effects!";
            bagButtons.SetActive(false);

            yield return new WaitForSeconds(2.0f);
        }

    }

    public void superPotion()
    {
        if (fpsController.GetComponent<ItemManager>().itemList[0].getNumber() != 0)
        {
            playerPokemon.GetComponent<BasePokemon>().setCurrentHealth(playerPokemon.GetComponent<BasePokemon>().returnCurrentHealth() + potion);
            if (playerPokemon.GetComponent<BasePokemon>().returnCurrentHealth() > playerPokemon.GetComponent<BasePokemon>().returnMaxHealth())
            {
                playerPokemon.GetComponent<BasePokemon>().setCurrentHealth(playerPokemon.GetComponent<BasePokemon>().returnMaxHealth());
            }
            Debug.Log(playerPokemon.GetComponent<BasePokemon>().returnCurrentHealth());
            updatePlayerPokemonHealthBar();
            fpsController.GetComponent<ItemManager>().itemList[0].setNumber(fpsController.GetComponent<ItemManager>().itemList[0].getNumber() - 1);
        }
        else
        {
            displayText.text = "You have no super potions!";
        }

    }
    IEnumerator superPotionText()
    {
        if (fpsController.GetComponent<ItemManager>().itemList[0].getNumber() != 0)
        {
            countAttacks++;
            StartCoroutine(otherPokemonUseMove(false));
            returnButton.gameObject.SetActive(false);
            displayText.text = playerPokemonName + "'s health has been restored!";
            bagButtons.SetActive(false);

            yield return new WaitForSeconds(2.0f);
        }

    }
    public void saySuperpotionText()
    {
        StartCoroutine(superPotionText());
    }

    //STARTING LEVELING UP
    private int experience;

    public void calculateExp()
    {
        float a;
        float b;
        float L;

        if (trainer)
        {
            a = 1.5f;
        }
        else
        {
            a = 1;
        }

        b = defendingPokemon.GetComponent<BasePokemon>().returnBaseExperienceYield();
        L = defendingPokemon.GetComponent<BasePokemon>().returnLevel();

        experience = (int)((a * b * L) / 7);


        playerPokemon.GetComponent<BasePokemon>().gainExperience((int)experience);
        updateExperienceBar();
    }

    //Code to fade text;
    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }

    public void updateExperienceBar()
    {
        Debug.Log(playerPokemon.GetComponent<BasePokemon>().returnExperienceToNextLevel());
        playerExperienceBar.transform.localScale = new Vector3((float)((float)playerPokemon.GetComponent<BasePokemon>().returnExperience() / (float)playerPokemon.GetComponent<BasePokemon>().returnExperienceToNextLevel()), 1.0f, 1.0f);
    }

    public void updateStatusTexts()
    {
        if (playerStatusEffects[0])
        {
            playerStatusText.text = "(Poisoned)";
        }
        else if (playerStatusEffects[1])
        {
            playerStatusText.text = "(Asleep)";
        }
        else if (playerStatusEffects[2])
        {
            playerStatusText.text = "(Paralyzed)";
        }
        else if (playerStatusEffects[3])
        {
            playerStatusText.text = "(Frozen)";
        }
        else if (playerStatusEffects[4])
        {
            playerStatusText.text = "(Burned)";
        }
        else if (playerStatusEffects[5])
        {
            playerStatusText.text = "(Confused)";
        }
        else
        {
            playerStatusText.text = "";
        }

        //WildPokemon

        if (wildStatusEffects[0])
        {
            wildStatusText.text = "(Poisoned)";
        }
        else if (wildStatusEffects[1])
        {
            wildStatusText.text = "(Asleep)";
        }
        else if (wildStatusEffects[2])
        {
            wildStatusText.text = "(Paralyzed)";
        }
        else if (wildStatusEffects[3])
        {
            wildStatusText.text = "(Frozen)";
        }
        else if (wildStatusEffects[4])
        {
            wildStatusText.text = "(Burned)";
        }
        else if (wildStatusEffects[5])
        {
            wildStatusText.text = "(Confused)";
        }
        else
        {
            wildStatusText.text = "";
        }
    }

}