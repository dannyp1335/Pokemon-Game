using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BasePokemon : MonoBehaviour
{
    public string name;
    public Stat maxHealth;
    public Stat currentHealth;
    public Stat attack;
    public Stat defense;
    public Stat spAttack;
    public Stat spDefense;
    public Stat speed;
    public string type;
    public string type2;
    public int catchRate;
    public int baseExperienceYield;
    
    public bool fainted;
    public bool wild;

    public bool[] conditions;

    private float spawnTimeLeft;
    public int minSpawnTime;
    public int maxSpawnTime;

    public int level;
    public int minLevel;
    public int maxLevel;

    public int currentExp;
    public int experienceToNextLevel;

    public int evolveLevel;
    public GameObject evolvePokemon;

    public GameObject fpsController;
    public Sprite pokemonSprite;

    public void Start()
    {
        fpsController = GameObject.FindWithTag("FPS");

        maxHealth.SetValue((int)(maxHealth.GetValue() * Random.Range(1.0f, 1.3f)));
        attack.SetValue((int)(attack.GetValue() * Random.Range(0.8f, 1.2f)));
        defense.SetValue((int)(defense.GetValue() * Random.Range(0.8f, 1.2f)));
        spAttack.SetValue((int)(spAttack.GetValue() * Random.Range(0.8f, 1.2f)));
        spDefense.SetValue((int)(spDefense.GetValue() * Random.Range(0.8f, 1.2f)));
        speed.SetValue((int)(speed.GetValue() * Random.Range(0.8f, 1.2f)));
        currentHealth.SetValue(maxHealth.GetValue());

        conditions = new bool[] { false, false, false, false, false, false }; //Poison(0), Sleep(1), Paralyzed(2), Freeze(3), Burn(4), Confusion(5).

        fainted = false;

        spawnTimeLeft = Random.Range(minSpawnTime, maxSpawnTime);

        if (wild)
        {
            level = (int)Random.Range(minLevel, maxLevel);
        }
    }

    public void TakeDamage(int totalDamage, bool hit)
    {
        if (hit == true)
        {
            currentHealth.SetValue(currentHealth.GetValue() - totalDamage);
            Debug.Log(transform.name + " takes " + totalDamage + " damage.");

            if (currentHealth.GetValue() <= 0)
            {
                currentHealth.SetValue(0);

                gameObject.SetActive(false);
                fainted = true;
            }
        }
    }
    //return methods
    public int returnAttack()
    {
        return attack.GetValue();
    }

    public int returnDefense()
    {
        return defense.GetValue();
    }

    public int returnSpecialAttack()
    {
        return spAttack.GetValue();
    }

    public int returnSpecialDefense()
    {
        return spDefense.GetValue();
    }

    public int returnSpeed()
    {
        return speed.GetValue();
    }

    public string returnType1()
    {
        return type;
    }

    public string returnType2()
    {
        return type2;
    }

    public int returnMaxHealth()
    {
        return maxHealth.GetValue();
    }

    public int returnCurrentHealth()
    {
        return currentHealth.GetValue();
    }

    public bool[] returnConditions()
    {
        return conditions;
    }

    public string returnName()
    {
        return name;
    }

    public int returnCatchRate()
    {
        return catchRate;
    }

    public int returnBaseExperienceYield()
    {
        return baseExperienceYield;
    }

    public int returnLevel()
    {
        return level;
    }

    //is methods
    public bool isWild()
    {
        return wild;
    }

    public bool isFainted()
    {
        return fainted;
    }

    public bool isNotFainted()
    {
        fainted = false;
        return fainted;
    }


    //set methods
    public void setCurrentHealth(int health)
    {
        if (health == 0)
        {
            fainted = true;
        }
        currentHealth.SetValue(health);
    }

    public void setMaximumHealth(int health)
    {
        maxHealth.SetValue(health);
    }
    
    public void setConditions(bool[] newConditions)
    {
        conditions = newConditions;
    }

    public void setSpawnTimeLeft(int newSpawnTime)
    {
        spawnTimeLeft = newSpawnTime;
    }

    public void setHealthText(int health, int maximumHealth, Text hbText)
    {
        hbText.text = health.ToString() + "/" + maximumHealth.ToString();
    }

    public void setNameText(string name, Text nText)
    {
        nText.text = name.ToString();
    }

    public void setWild(bool newWild)
    {
        wild = newWild;
    }

    public void FixedUpdate()
    {
        if (wild)
        {
            spawnTimeLeft -= Time.deltaTime;
            if (spawnTimeLeft < 0)
            {
                despawnPokemon();
            }
        }
    }

    public void stopDespawnTimer(bool stopTimer)
    {
        wild = stopTimer;
    }
    public void despawnPokemon()
    {
        Destroy(gameObject);
    }

    public void gainExperience(int gainedExperience)
    {
        Debug.Log("experience gained: " + gainedExperience);
        experienceToNextLevel = (level + 1);
        experienceToNextLevel = (int)Mathf.Pow(experienceToNextLevel, 3.0f);
        Debug.Log("exp to next lvl: " + experienceToNextLevel);

        currentExp = currentExp + gainedExperience;
        if (currentExp > experienceToNextLevel && level != 100)
        {
            levelUp();
            currentExp = currentExp - experienceToNextLevel;
        }
    }

    public void levelUp()
    {
        level++;
        Debug.Log("LevelUp");
    }

    private GameObject newPokemon;

    public void checkEvolve()
    {
        if (level >= evolveLevel)
        {
            Vector3 spawnPosition = new Vector3(0, 1, 0);
            DontDestroyOnLoad(Instantiate(evolvePokemon, spawnPosition + transform.TransformPoint(20, 0, 0), gameObject.transform.rotation));

            newPokemon = GameObject.FindWithTag("Evolve");

            for (int i = 0; i < fpsController.GetComponent<PokemonManager>().pokemonArray.Length; i++)
            {
                if (fpsController.GetComponent<PokemonManager>().pokemonArray[i] == this.gameObject)
                {
                    fpsController.GetComponent<PokemonManager>().pokemonArray[i] = newPokemon;
                    fpsController.GetComponent<PokemonManager>().pokemonArray[i].gameObject.tag = "Player";
                    fpsController.GetComponent<PokemonManager>().pokemonArray[i].SetActive(false);
                    fpsController.GetComponent<PokemonManager>().displayMessage("Your " + name + " has evolved into " + evolvePokemon.GetComponent<BasePokemon>().returnName() + "!");
                    fpsController.GetComponent<PokemonManager>().pokemonArray[i].GetComponent<BasePokemon>().setExperience(currentExp);
                    fpsController.GetComponent<PokemonManager>().pokemonArray[i].GetComponent<BasePokemon>().setLevel(level);
                    Destroy(this.gameObject);
                    Debug.Log("Your Pokemon Evolved");
                }
            }
        }
    }

    public int returnExperienceToNextLevel()
    {

        experienceToNextLevel = (level + 1);
        experienceToNextLevel = (int)Mathf.Pow(experienceToNextLevel, 3.0f);

        return experienceToNextLevel;
    }

    public int returnExperience()
    {
        return currentExp;
    }

    public void setExperience(int exp)
    {
        currentExp = exp;
    }

    public void setLevel(int newLevel)
    {
        level = newLevel;
    }
}
