using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this class cannot be a monobehaviour
public class Bar
{
    [SerializeField]
    private float fillAmount;
    [SerializeField]
    private Image image;

    //constructor
    public Bar(Image tempImage, float tempFillAmount)
    {
        image = tempImage;
        fillAmount = tempFillAmount;
    }

    //converts health value into a number that can be understood by fillamount (only takes vales between 0 and 1)
    public void setHealthBar(float health, float maximumHealth)
    {
        fillAmount = health / maximumHealth;
        image.fillAmount = fillAmount;
    }
}
