using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainerScript : MonoBehaviour
{

    public GameObject trainer;
    public bool trainerLife;


    // Use this for initialization
    void Start()
    {
        trainerLife = true;
        Debug.Log(trainerLife);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void trainerCoolDown()
    {
        trainerLife = BattleManager.trainerBool;
        Debug.Log(trainerLife);
        if (BattleManager.trainerBool == false)
        {
            Debug.Log(BattleManager.trainerBool);
            trainer.GetComponent<PokemonHoverOver>().ifTrainerLost();
            Invoke("trainerHealed", 10);
            trainerLife = true;
            BattleManager.trainerBool = true;
            Debug.Log(BattleManager.trainerBool);
            Debug.Log(trainerLife);
        }
    }
    public void trainerHealed()
    {
        trainer.GetComponent<PokemonHoverOver>().doFadeText();
    }

}
