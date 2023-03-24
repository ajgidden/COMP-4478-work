using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    //Sprite for the back of the cards
    [SerializeField] private Sprite background;

    //array for the pictures on each card as well as lists for the card buttons and their sprites
    public Sprite[] BackSides;
    public List<Sprite> CardBackSides = new List<Sprite>();
    public List<Button> cards = new List<Button>();

    //booleans to check if this is the second or first card clicked
    private bool firstClicked;
    private bool secondClicked;
    //in order counts the total turns, total correct turns, maximum number of correct turns
    private int counter = 0;
    private int Correct = 0;
    private int totalGuesses;

    //stores the indexes of the clicked cards
    private int firstIndex;
    private int secondIndex;

    //stores the names of the clicked cards
    private string firstName;
    private string secondName;

    //refrences the script for a gameover
    public GameOver GameOverScreen;

    void Awake()
    {
        //loads all the sprites into the array
        BackSides = Resources.LoadAll<Sprite>("Sprites/BackSides");
    }

    void Start()
    {
        //initilizes all the variables
        GetCards();
        AddListeners();
        AddBackSides();
        RandomCard(CardBackSides);
        totalGuesses = CardBackSides.Count / 2;
    }

    void GetCards()
    {
        //creates game objects for the buttons
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Card Button");

        //establishes them as buttons and adds the background card sprites to all of them
        for(int i = 0; i < objects.Length; i++)
        {
            cards.Add(objects[i].GetComponent<Button>());
            cards[i].image.sprite = background;
        }
    }

    void AddBackSides()
    {
        //establishes the index
        int index = 0;

        for(int i = 0; i < cards.Count; i++)
        {
            //restarts the idnex if you run out of cards
            if(index == cards.Count / 2)
            {
                index = 0;
            }

            //adds the sprites to the cardbacksides and then moves on
            CardBackSides.Add(BackSides[index]);
            index++;
        }
    }

    void AddListeners()
    {
        //for all the cards it adds listeners for if they are clicked
        for(int i = 0; i < cards.Count; i++)
        {
            cards[i].onClick.AddListener(() => PickACardAnyCard());
        }
    }

    public void PickACardAnyCard()
    {
        //takes the name from the linked button
        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        
        //if its the first one just flip the card to the picture and set the values for the name and index
        if(firstClicked != true)
        {
            firstClicked = true;

            firstIndex = int.Parse(name);

            firstName = CardBackSides[firstIndex].name;

            cards[firstIndex].image.sprite = CardBackSides[firstIndex];
        }
        //same as the first one except it will also check if they match at the end and increase the counter for turns
        else if(secondClicked != true)
        {
            secondClicked = true;

            secondIndex = int.Parse(name);

            secondName = CardBackSides[secondIndex].name;

            cards[secondIndex].image.sprite = CardBackSides[secondIndex];

            counter++;

            StartCoroutine(checkForMatch());
        }
    }

    IEnumerator checkForMatch()
    {
        //if the cards match
        if(firstName == secondName)
        {
            //sets both cards to non interactable so that the user can't clicke it again
            cards[firstIndex].interactable = false;
            cards[secondIndex].interactable = false;

            //makes the color more faded to indicate it can't be clicked
            cards[firstIndex].image.color = new Color(85, 85, 85, 1);
            cards[secondIndex].image.color = new Color(85, 85, 85, 1);

            //else call a check if finished
            CheckIfFinished();

        }
        else
        {
            //otherwise wait for 1 second so the suer can see both cards
            yield return new WaitForSeconds(1f);
            //reset both cards tot he blank card back
            cards[firstIndex].image.sprite = background;
            cards[secondIndex].image.sprite = background;
        }

        //either way reset the clicked variables to false
        firstClicked = false;
        secondClicked = false;
    }

    void CheckIfFinished()
    {
        //adds a correct guess
        Correct++;

        //if all correct guesses have happend pull up the game over screen
        if(Correct == totalGuesses)
        {
            GameOverScreen.Displayer(counter);
        }
    }

    void RandomCard(List<Sprite> list)
    {
        //generates a random order for the cards by reassigning each variable and name to a different slot
        for(int i = 0; i < list.Count; i++)
        {
            Sprite holder = list[i];
            int randomNum = Random.Range(i, list.Count);
            list[i] = list[randomNum];
            list[randomNum] = holder;
        }
    }
}
