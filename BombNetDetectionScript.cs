using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombNetDetectionScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] Transform fish;
    [SerializeField] Transform fish1;
    [SerializeField] Transform fish2;
    [SerializeField] Transform fish3;
    [SerializeField] Transform fish4;
    [SerializeField] Transform fish5;
    [SerializeField] Transform bomb1;
    [SerializeField] Transform bomb2;
    [SerializeField] Transform bomb3;
    [SerializeField] Transform bomb4;
    [SerializeField] Transform bomb5;
    [SerializeField] Text GameOver;
    [SerializeField] Text Score;

    public float RadNum = 0f;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        //the code for if the bomb is on top of the player
        if (PlayerCaught())
        {
            //moves all the fish and bombs off screen to indicate that the game is over
            fish1.transform.position = new Vector3(18, 6, 0);
            fish2.transform.position = new Vector3(20, 6, 0);
            fish3.transform.position = new Vector3(22, 6, 0);
            fish4.transform.position = new Vector3(24, 6, 0);
            fish5.transform.position = new Vector3(26, 6, 0);
            bomb1.transform.position = new Vector3(-18, 6, 0);
            bomb2.transform.position = new Vector3(-20, 6, 0);
            bomb3.transform.position = new Vector3(-22, 6, 0);
            bomb4.transform.position = new Vector3(-24, 6, 0);
            bomb5.transform.position = new Vector3(-26, 6, 0);
            //removes the score in the top right and displays your final score and the game over screen
            GameOver.text = "Game Over\nFinal " + Score.text;
            Score.text = "";
        }
        //the code for if the bomb hits the floor
        if (PlayerMissed())
        {
            //spawns the corresponding fish and returns the bomb back to the waiting area
            RadNum = Random.Range(-10, 10);
            fish.position = new Vector3(RadNum, 7, 0);
            rb.velocity = Vector3.zero;
            this.transform.position = new Vector3(-26, 7, 0);
        }
    }

    //creates a little circle below the bomb to detect the player net underneath
    private bool PlayerCaught()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, playerLayer);
    }

    //creates another little circle below the bomb to detect if the ground is underneath
    private bool PlayerMissed()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
