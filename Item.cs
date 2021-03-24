using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item 
{
    [SerializeField]
    private string name;
    [SerializeField]
    private int number;
    [SerializeField]
    private string description;

    public string getName()
    {
        return name;
    }

    public string getDescription()
    {
        return description;
    }
    
    public int getNumber()
    {
        return number;
    }

    public void setNumber(int num)
    {
        number = num;
    }

    public void setNameText(string name, Text nText)
    {
        nText.text = name;
    }

    public void setNumberText(int? num, Text nText)
    {
        nText.text = num.ToString();
    }

    public void setXText(string x,Text xText)
    {
        xText.text = x;
    }
    public void setDescriptionText(string description, Text dText)
    {
        dText.text = description;
    }
}
