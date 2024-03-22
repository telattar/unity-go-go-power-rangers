using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class collisionBehavior : MonoBehaviour
{
    public GameObject redOrb;
    public GameObject greenOrb;
    public GameObject blueOrb;
    public GameObject obstacle;
    private bool redFlag, greenFlag, blueFlag;
    private bool multiplier, shield;
    Behaviour halo;
    public gameOverScreen gameoverscreen;

    public static bool gameoverBoolean;
    public TMP_Text scoreText;
    public TMP_Text redText;
    public TMP_Text greenText;
    public TMP_Text blueText;
    public TMP_Text finalSc;

    public int score = 0;
    private int redPoints = 0;
    private int greenPoints = 0;
    private int bluePoints = 0;

    void Start()
    {
        gameoverBoolean = false;
        halo = (Behaviour)gameObject.GetComponent("Halo");
        scoreText.text = "score = 0";
        redFlag = false;
        greenFlag = false;
        blueFlag = false;
        multiplier = false;
        shield = false;
    }

    //the flags should be disabled in case the player changed their color from blue to red, for instance.
    void disableFlags()
    {
        redFlag = false;
        greenFlag = false;
        blueFlag = false;
    }

    void Update()
    {
        if (gameoverBoolean)
            return;
        //when in active form, go back to norm when corresponding pts = 0
        if ((redFlag && redPoints == 0) || (greenFlag && greenPoints == 0) || (blueFlag && bluePoints == 0))
        {
                disableFlags();
                GameObject.Find("player").GetComponent<Renderer>().material.color = Color.white;

        }

        //Switching to another form deactivates the multiplier
        if (!greenFlag)
            multiplier = false;

        //switching to another form deactivates the shield
        if (!blueFlag)
            shield = false;

        if (!shield)
            halo.enabled = false;

        //in case the player would like to change the color to red
        if (Input.GetKeyDown(KeyCode.J))
        {
            //MUST HAVE: 5 ENERGY POINTS OF THE COLOR THEY WANT
            if (redPoints != 5)
            {
                FindObjectOfType<soundManager>().playSound("buzzer");
                return;
            }
            FindObjectOfType<soundManager>().playSound("colorchange");

            GameObject.Find("player").GetComponent<Renderer>().material.color = Color.red;
            redPoints -= 1;

            disableFlags();
            redFlag = true;
        }

        //in case the player would like to change the color to green
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (greenPoints != 5)
            {
                FindObjectOfType<soundManager>().playSound("buzzer");
                return;
            }

            FindObjectOfType<soundManager>().playSound("colorchange");


            GameObject.Find("player").GetComponent<Renderer>().material.color = Color.green;
            greenPoints -= 1;

            disableFlags();
            greenFlag = true;
        }

        //in case the player would like to change the color to red
        if (Input.GetKeyDown(KeyCode.L))
        {

            if (bluePoints != 5)
            {
                FindObjectOfType<soundManager>().playSound("buzzer");
                return;
            }
            FindObjectOfType<soundManager>().playSound("colorchange");
            GameObject.Find("player").GetComponent<Renderer>().material.color = Color.blue;
            bluePoints -= 1;

            disableFlags();
            blueFlag = true;
        }

        //using the powers requires pressing the spacebar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!redFlag && !blueFlag && !greenFlag)
                FindObjectOfType<soundManager>().playSound("buzzer");

            //red power (nuke)
            if (redFlag)
            {
                FindObjectOfType<soundManager>().playSound("powerup");

                List<GameObject> obs = playerScript.prefabs;
                for (int i = 0; i < obs.Count; i++)
                {
                    //destroy all the obstacles
                    if (obs.ElementAt(i) && obs.ElementAt(i).tag == "box" && obs.ElementAt(i).transform.position.y < 1000) //do not distroy the original element
                    {
                        Destroy(obs[i]);
                        obs.RemoveAt(i);
                    }
                }
                //update the prefabs
                playerScript.prefabs = obs;
                redPoints -= 1;
            }

            //pressing the spacebar while the multiplier is active should not affect the state of the player
            if (greenFlag)
            {
                if (!multiplier)
                {
                    FindObjectOfType<soundManager>().playSound("powerup");
                    multiplier = true;
                    greenPoints -= 1;
                }
            }

            //blue power (shield)
            if (blueFlag)
            {
                if (!shield)
                {
                    FindObjectOfType<soundManager>().playSound("powerup");
                    halo.enabled = true;
                    shield = true;
                    bluePoints -= 1;
                }

            }

        }



        redText.text = "red pts = " + redPoints;
        greenText.text = "green pts = " + greenPoints;
        blueText.text = "blue pts = " + bluePoints;

    }

    void gameover()
    {
        gameoverscreen.setup(score);
    }
    private void OnCollisionEnter(Collision hit)
    {

        //colliding with a red orb
        if (hit.gameObject.tag == "red")
        {
            FindObjectOfType<soundManager>().playSound("collected");
            Destroy(hit.gameObject);

            if (redFlag)
                score += 2;
            else if (multiplier)
                score += 5;
            else
                score += 1;

            if (redPoints != 5 & !redFlag)
            {
                redPoints += 1;
                if (redPoints < 5 & multiplier) //total scored red points = 2
                    redPoints += 1;
            }

            scoreText.text = "score = " + score;
            redText.text = "red pts = " + redPoints;
        }

        //colliding with a green orb
        else if (hit.gameObject.tag == "green")
        {
            FindObjectOfType<soundManager>().playSound("collected");
            Destroy(hit.gameObject);

            if (greenFlag && multiplier)
                score += 10;
            else if (greenFlag)
                score += 2;
            else
                score += 1;


            if (greenPoints != 5 & !multiplier)
            {
                if (!greenFlag)
                    greenPoints += 1;
                if (greenPoints > 5)
                    greenPoints = 5;
            }


            scoreText.text = "score = " + score;
            greenText.text = "green pts = " + greenPoints;

        }

        //colliding with a blue orb
        else if (hit.gameObject.tag == "blue")
        {
            FindObjectOfType<soundManager>().playSound("collected");
            Destroy(hit.gameObject);

            if (blueFlag)
                score += 2;
            else if (multiplier)
                score += 5;
            else
                score += 1;

            if (bluePoints != 5 & !blueFlag)
            {
                bluePoints += 1;
                if (bluePoints < 5 & multiplier) //total scored red points = 2
                    bluePoints += 1;
            }

            scoreText.text = "score = " + score;
            blueText.text = "blue pts = " + bluePoints;

        }



        //colliding with an obstacle
        else if (hit.transform.tag == "box")
        {
            FindObjectOfType<soundManager>().playSound("hit");
            if (shield)
            {
                Destroy(hit.gameObject);
                shield = false;
            }

            // The player reverts back to normal form upon hitting an obstacle in coloured form.
            // this applies if the shield was not enabled in the first place
            else if (redFlag || greenFlag || blueFlag)
            {
                Destroy(hit.gameObject);
                disableFlags();
                GameObject.Find("player").GetComponent<Renderer>().material.color = Color.white;
            }
            else //game over
            {
                finalSc.text = "Your score is: " + score;
                Time.timeScale = 0;
                gameoverBoolean = true;
                gameover();

            }
        }

        //multiplier is automatically deactivated after hitting an orb
        if (multiplier)
            multiplier = false;
    }


}
