using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCards : MonoBehaviour
{
    //the grid the cards will be added to
    [SerializeField] private Transform grid;

    //the card prefab
    [SerializeField] private GameObject card;

    void Awake()
    {
        //adds 16 cards to the grid with each name being a number
        for(int i = 0; i < 16; i++)
        {
            GameObject cardButton = Instantiate(card);
            cardButton.name = "" + i;
            cardButton.transform.SetParent(grid, false);
        }

    }
}
