using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    //text to display the score
    public Text turnText;

    //Displays the score in the text and turns on the game over screen
    public void Displayer(int turns)
    {
        gameObject.SetActive(true);
        turnText.text = ("You Won In " + turns + " Turns");
    }

    //When the restart button is clicked the games scene will be reloaded
    public void restart()
    {
        SceneManager.LoadScene("Assignment2");
    }
}
