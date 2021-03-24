using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[SerializeField]
public class PokemonManager : MonoBehaviour
{   
    public GameObject[] pokemonArray;
    public GameObject[] extraPokemonArray;

    [SerializeField]
    public GameObject[] pokemonPanelArray;
    [SerializeField]
    public GameObject[] outlineArray;
    [SerializeField]
    public Text[] nameTextArray;
    [SerializeField]
    public Image[] pokeballImageArray;
    [SerializeField]
    public Image[] pokemonImageArray;
    [SerializeField]
    public Text[] levelTextArray;
    [SerializeField]
    public GameObject[] faintPanelArray;
    [SerializeField]
    public Text[] faintTextArray;
    [SerializeField]
    public GameObject[] statusEffectPanelArray;
    [SerializeField]
    public Text[] statusEffectTextArray;
    [SerializeField]
    public Text[] healthTextArray;
    [SerializeField]
    public Image[] healthBarImageArray;
    [SerializeField]
    public GameObject[] healthBarArray;

    public InputManager myInputManager = new InputManager();

    Bar tempBar;
    Text tempNameText;
    Text tempHealthText;
    Image tempImage;
    Color pokemonPanelColor = new Color (.102f, .290f,.882f, .784f);

    ColorBlock faintedColorBlock;
    ColorBlock standardColorBlock;

    int tempHealth;
    int tempMaximumHealth;

    float tempFillAmount;

    string tempName;
    public string trainerName;

    public bool fightingTrainer;

    Color BRNCOLOR = Color.red;
    Color FRZCOLOR = Color.cyan;
    Color PARCOLOR = Color.yellow;
    Color PSNCOLOR = Color.magenta;
    Color SLPCOLOR = Color.gray;

    public Text messageText;

    //instantiate health bars to each specific Pokemon's health.
    //the reason this is breaking is because it is being loaded at the start instead of when the inventory scene is loaded
    void Start()
    {
        fightingTrainer = false;

        extraPokemonArray = new GameObject[150];

    }

    //updates health bars according to each health information for each pokemon
    //must remain public due to initialize method in inputManager
    public void updateHealthBars()
    {
        int length = pokemonArray.Length;
        int level;
        bool hasStatusEffect = false;

        for (int i = 0; i < length; i++)
        {
            if (pokemonArray[i] != null)
            {
                level = pokemonArray[i].GetComponent<BasePokemon>().level;
                levelTextArray[i].text = "LV. " + level.ToString();
                
                tempImage = healthBarImageArray[i];
                tempFillAmount = tempImage.fillAmount;
                tempBar = new Bar(tempImage, tempFillAmount);
                tempHealth = pokemonArray[i].GetComponent<BasePokemon>().returnCurrentHealth();
                tempMaximumHealth = pokemonArray[i].GetComponent<BasePokemon>().returnMaxHealth();
                tempBar.setHealthBar(tempHealth, tempMaximumHealth);
                tempHealthText = healthTextArray[i];
                pokemonArray[i].GetComponent<BasePokemon>().setHealthText(tempHealth, tempMaximumHealth, tempHealthText);

                //fainted based on health
                if (tempHealth <= 0)
                {
                    setFainted(i);
                }
                if (tempHealth > 0)
                {
                    removeFainted(i);
                }

                //status effect based on conditions 
                for (int x = 0; x < pokemonArray[i].GetComponent<BasePokemon>().conditions.Length; x++)
                {
                    if (pokemonArray[i].GetComponent<BasePokemon>().conditions[x] == true)
                    {
                        if (x == 0)
                        {
                            setStatusEffect(i, "PSN", PSNCOLOR);
                            hasStatusEffect = true;
                        }

                        if (x == 1)
                        {
                            setStatusEffect(i, "SLP", SLPCOLOR);
                            hasStatusEffect = true;
                        }

                        if (x == 2)
                        {
                            setStatusEffect(i, "PAR", PARCOLOR);
                            hasStatusEffect = true;
                        }

                        if (x == 3)
                        {
                            setStatusEffect(i, "FRZ", FRZCOLOR);
                            hasStatusEffect = true;
                        }

                        if (x == 4)
                        {
                            setStatusEffect(i, "BRN", BRNCOLOR);
                            hasStatusEffect = true;
                        }
                    }
                    
                    if (hasStatusEffect == false)
                    {
                        removeStatusEffect(i);
                    }
                }
            }
        }
             
    }
    
    //sets names at the start
    //must remain public due to initialize method in inputManager
    public void setText()
    {
       int length = pokemonArray.Length;
       int count = 0;

        for (int i = 0; i < length; i++)
        {
            if (pokemonArray[i] != null)
            {
                tempName = pokemonArray[i].GetComponent<BasePokemon>().name;
                tempNameText = nameTextArray[i];
                pokemonArray[i].GetComponent<BasePokemon>().setNameText(tempName, tempNameText);
                outlineArray[i].GetComponent<Image>().enabled = true;
                nameTextArray[i].enabled = true;
                healthTextArray[i].enabled = true;
                pokeballImageArray[i].GetComponent<Image>().enabled = true;
                pokemonImageArray[i].GetComponent<Image>().enabled = true;
                levelTextArray[i].enabled = true;
                pokemonImageArray[i].sprite = pokemonArray[i].GetComponent<BasePokemon>().pokemonSprite;
                faintTextArray[i].enabled = true;
                faintPanelArray[i].GetComponent<Image>().enabled = true;
                statusEffectTextArray[i].enabled = true;
                statusEffectPanelArray[i].GetComponent<Image>().enabled = true;
                healthBarImageArray[i].GetComponent<Image>().enabled = true;
                healthBarArray[i].SetActive(true);
                count = i;
            }
        }

        for (int i = count + 1; i < 6; i++)
        {
            outlineArray[i].GetComponent<Image>().enabled = false;
            nameTextArray[i].enabled = false;
            healthTextArray[i].enabled = false;
            pokeballImageArray[i].GetComponent<Image>().enabled = false;
            pokemonImageArray[i].GetComponent<Image>().enabled = false;
            levelTextArray[i].enabled = false;
            faintTextArray[i].enabled = false;
            faintPanelArray[i].GetComponent<Image>().enabled = false;
            statusEffectTextArray[i].enabled = false;
            statusEffectPanelArray[i].GetComponent<Image>().enabled = false;
            healthBarImageArray[i].GetComponent<Image>().enabled = false;
            healthBarArray[i].SetActive(false);
        }
    }

    void setFainted(int num)
    {
        Image tempImage;

        faintPanelArray[num].SetActive(true);
        tempImage = pokemonPanelArray[num].GetComponent<Image>();
        tempImage.color = new Color(.75f, 0f, 0f, 1f);
        faintTextArray[num].enabled = true;

        faintedColorBlock = myInputManager.pokemonEventSystemButtonArray[num].GetComponent<Button>().colors;
        faintedColorBlock.highlightedColor = Color.red;
        myInputManager.pokemonEventSystemButtonArray[num].GetComponent<Button>().colors = faintedColorBlock;
    }

    void removeFainted(int num)
    {
        Image tempImage;

        faintPanelArray[num].SetActive(false);
        tempImage = pokemonPanelArray[num].GetComponent<Image>();
        tempImage.color = pokemonPanelColor;
        faintTextArray[num].enabled = false;

        standardColorBlock = myInputManager.pokemonEventSystemButtonArray[num].GetComponent<Button>().colors;
        standardColorBlock.highlightedColor = new Color(0f, .239f, 1f, .882f);
        myInputManager.pokemonEventSystemButtonArray[num].GetComponent<Button>().colors = standardColorBlock;
    }

    void setStatusEffect(int num, string effect, Color color)
    {
        Image tempImage;

        statusEffectPanelArray[num].SetActive(true);
        tempImage = statusEffectPanelArray[num].GetComponent<Image>();
        tempImage.color = color;
        statusEffectTextArray[num].enabled = true;
        statusEffectTextArray[num].text = effect;
    }

    void removeStatusEffect(int num)
    {
        statusEffectPanelArray[num].SetActive(false);
        statusEffectTextArray[num].enabled = false;
    }
    
    public GameObject returnPokemonNumber(int pokeNumber) //Pass the method the spot in the array where u want that Pokemon
    {
        return pokemonArray[pokeNumber];
    }

    public void setPokemonNumber(int pokeNumber, GameObject pokemon)
    {
        pokemonArray[pokeNumber] = pokemon;
    }

    public void setFightingTrainer(bool trainer)
    {
        fightingTrainer = trainer;
    }

    public bool returnFightingTrainer()
    {
        return fightingTrainer;
    }

    public string returnTrainerName()
    {
        return trainerName;
    }

    public void setTrainerName(string name)
    {
        trainerName = name;
    }

    public void addToBox(GameObject pokemon)
    {
        for (int i = 0; i < 150; i++)
        {
            if (extraPokemonArray[i] == null)
            {
                extraPokemonArray[i] = pokemon;
                i = 150;
            }
        }
    }

    public void displayMessage(string message)
    {
        messageText.text = message;
        StartCoroutine(FadeTextToFullAlpha(0.3f, messageText));
        StartCoroutine(FadeTextToZeroAlpha(3.0f, messageText));
    }

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

}
