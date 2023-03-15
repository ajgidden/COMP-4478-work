using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetDetectorScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] Transform bomb;
    [SerializeField] Text scoreText;

    public float RadNum = 0f;
    public float bombOrFish = 0f;
    public static int score = 0;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        //code if the fish detects the player net right below it
        if(PlayerCaught())
        {
            //increases the score by one and updates the score text in the top right
            score++;
            scoreText.text = "Score: " + score;

            //finds the random values to spawn it at and decides if its spawning a bomb or a fish
            RadNum = Random.Range(-10, 10);
            bombOrFish = Random.Range(1, 5);
            //spawns a bomb and puts aside the fish in the waiting area
            if (bombOrFish == 3)
            {
                bomb.position = new Vector3(RadNum, 6, 0);
                this.transform.position = new Vector3(23 + RadNum, 3, 0);
            }
            //spawns a fish above
            else
            {
                this.transform.position = new Vector3(RadNum, 7, 0);
                rb.velocity = Vector3.zero;
            }
        }

        //triggers if it detects the ground right below the fish
        if (PlayerMissed())
        {
            //increases the score by one and updates the score text in the top right
            RadNum = Random.Range(-10, 10);
            bombOrFish = Random.Range(1, 10);
            //spawns a bomb and puts aside the fish in the waiting area
            if (bombOrFish == 7)
            {
                bomb.position = new Vector3(RadNum, 6, 0);
                this.transform.position = new Vector3(23 + RadNum, 3, 0);
            }
            //spawns a fish above
            else
            {
                this.transform.position = new Vector3(RadNum, 7, 0);
                rb.velocity = Vector3.zero;
            }

        }
    }

    //draws a small circle below the fish to detect the player.
    private bool PlayerCaught()
    {
        return Physics2D.OverlapCircle(groundCheck.position,0.2f,playerLayer);
    }

    //draws a small circle below the fish to detect the ground
    private bool PlayerMissed()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
