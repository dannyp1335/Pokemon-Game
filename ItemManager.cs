﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    public List<Item> itemList;
    [SerializeField]
    public Text[] itemNameTextArray;
    [SerializeField]
    public Text[] itemNumberTextArray;
    [SerializeField]
    public Text[] itemXTextArray;
    [SerializeField]
    Text descriptionText;
    [SerializeField]
    Image descriptionImage;
    [SerializeField]
    InputManager inputManager;

    List<Item> tempList = new List<Item>();
    Text tempNameText;
    Text tempNumberText;
    Text tempXText;
    string x;
    string tempName;
    string tempDescription;
    int pageNumber = 1;
    int location;
    int position;
    int tempNumber;
    int initialLength;
    
    void Awake()
    {
        x = "X";
        setText(itemList);
        initialLength = itemList.Count;

        if (initialLength > 7)
            position = 7;
        else
            position = initialLength;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && inputManager.getEscapeCanvasState() == true)
        {
            turnPageForwards();
        }

        if (Input.GetKeyDown(KeyCode.Z) && inputManager.getEscapeCanvasState() == true)
        {
            turnPageBackwards();
        }
    }

    public void checkZero()
    {
        List<Item> tempList = new List<Item>();
        int start = (pageNumber - 1) * 7;
        int end;

        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].getNumber() == 0)
            {
                itemList.Remove(itemList[i]);

                if ((start + 7) >= itemList.Count)
                {
                    end = itemList.Count;
                }
                else
                {
                    end = start + 7;
                }

                for (int x = start; x < end; x++)
                {
                  tempList.Add(itemList[x]);
                }
                setText(tempList);
                i = itemList.Count;
            }
        }
    }

    public int getPageNumber()
    {
        return pageNumber;
    }

    void turnPageForwards()
    {
        int count = 0;
        int length = itemList.Count;
        int max;

        if (length > 7 && position != itemList.Count)
        {

            if (length > (position + 7))
            {
                max = position + 7;
            }
            else
            {
                max = length;
            }

            if (length > 7 && length > position)
            {
                for (int i = position; i < max; i++)
                {
                    if (itemList[i] != null)
                    {
                        tempList.Add(itemList[i]);
                        count++;
                    }
                }

                position = position + tempList.Count;
                pageNumber++;
                setText(tempList);
                tempList.Clear();
            }
        }
    }

    void turnPageBackwards()
    {
        int start;
        int end;
        int mod;

        if (position > 7)
        {
            mod = position % 7;
            position = position - mod;
            if (mod == 0)
            {
                start = position - 14;
                end = position - 7;
                position = position - 7;
            }
            else
            {
                start = position - 7;
                end = position;
            }
            
                for (int i = start; i < end; i++)
            {
                tempList.Add(itemList[i]);
            }

            pageNumber--;
            setText(tempList);
            tempList.Clear();
        }
    }

    public void setText(List<Item> list)
    {
        int start = 0;
        int currentMax;
        int totalPageNumber = (list.Count / 7) + 1;

        if (list.Count % 7 != 0 && pageNumber != 1)
        {
            currentMax = list.Count % 7;
        }
        else if (totalPageNumber != 1 && pageNumber == 1)
        {
            currentMax = 7;
        }
        else
        {
            currentMax = list.Count;
        }

        for (int i = start; i < currentMax ; i++)
        {
            tempName = list[i].getName();
            tempNameText = itemNameTextArray[i];
            itemList[i].setNameText(tempName, tempNameText);
            tempNumber = list[i].getNumber();
            tempNumberText = itemNumberTextArray[i];
            itemList[i].setNumberText(tempNumber, tempNumberText);
            tempXText = itemXTextArray[i];
            itemList[i].setXText(x, tempXText);
        }

        //setname/setnumber/setxText(s) are defined in an item but it doesnt matter which one is updated so it calls itemList[0]
        for (int i = currentMax; i < 7; i++)
        {
            itemList[0].setNameText(null, itemNameTextArray[i]);
            itemList[0].setNumberText(null, itemNumberTextArray[i]);
            itemList[0].setXText(null, itemXTextArray[i]);
        }
    }

    public void setDescriptionText(int location)
    {
        descriptionText.text = itemList[location].getDescription();
        //descriptionImage = itemList[location].getImage();
    }
}
