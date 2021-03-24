﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectSearcher : MonoBehaviour
{
    public string searchTag;
    public List<GameObject> actors = new List<GameObject>();

    public GameObject[] temp;


    void Start()
    {
        if (searchTag != null)
        {
            FindObjectwithTag(searchTag);
        }
    }

    public void FindObjectwithTag(string _tag)
    {
        actors.Clear();
        Transform parent = transform;
        GetChildObject(parent, _tag);
    }

    public void GetChildObject(Transform parent, string _tag)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.tag == _tag)
            {
                actors.Add(child.gameObject);
            }
            if (child.childCount > 0)
            {
                GetChildObject(child, _tag);
            }
        }
    }

    public GameObject[] returnPokemonList()
    {
         temp = new GameObject[actors.Count];

        actors.CopyTo(temp);

        return temp;
    }
}