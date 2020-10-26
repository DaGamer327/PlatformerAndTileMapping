using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

 
    public Text score;
    private int scoreValue = 0;
    public Text winText;

    public Image image1;
    public Image image2;
    public Image image3;

    private int enemy;

    public int lives;
    public Text livesText;

    public AudioSource BGM;
    public AudioSource victory;


    private Transform Playerpoint;
    public Transform nextlevel;



    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();

        SetScoreText();

        SetLives();


        scoreValue = 0;

        winText.text = "";

        enemy = 0;

        lives = 3;

        Playerpoint = GameObject.Find("Player").transform;


    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        
        if (scoreValue == 4)
        {
            Playerpoint.position = nextlevel.position;
            Destroy(nextlevel.gameObject);

            enemy = 0;
            lives = 3;
            livesText.text = "Lives: " + lives.ToString();

        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            SetScoreText();
            Destroy(collision.collider.gameObject);

        }

        if (collision.collider.tag == "enemy")
        {
            enemy = enemy + 1;
            Destroy(collision.collider.gameObject);
            livesText.text = lives.ToString();
            SetLife();
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }

            if (collision.collider.tag == "enemy")
            {
                enemy = enemy + 1;
                SetLife();
            }

        }

        if (collision.collider.tag == "enemy")
        {
            switch (enemy)
            {
                case 3:
                    winText.text = "You Lose! \nCreated by Joshua Fernandez";
                    Destroy(this.gameObject);
                    Destroy(image1.gameObject);
                    enemy = enemy + 1;
                    PauseGame();
                    break;
                case 2:
                    Destroy(image2.gameObject);
                    enemy = enemy + 1;

                    break;
                case 1:
                    Destroy(image3.gameObject);
                    enemy = enemy + 1;
                    break;

                default:
                    break;
            }
        }
    }

    void SetScoreText()
    {
        score.text = "Coins: " + scoreValue.ToString();

        if (scoreValue == 8)
        {
            Destroy(BGM.gameObject);
            victory.Play();
            winText.text = "You Win! \nCreated by Joshua Fernandez";
            PauseGame();
        }

        

    }

        void SetLives()
        {
            livesText.text = "Lives: " + lives.ToString();

            if (scoreValue == 4)
            {
                winText.text = "You Win! \nCreated by Joshua Fernandez";
                PauseGame();
            }

       

    }
    void SetLife()
    {
        switch (enemy)
        {
            case 3:
                lives = lives -= 1;
                livesText.text = "Lives: " + lives.ToString();
                winText.text = "You Lose! \nCreated by Joshua Fernandez";
                Destroy(this.gameObject);
                Destroy(image1.gameObject);
                PauseGame();
                break;
            case 2:
                lives = lives -= 1;
                livesText.text = "Lives: " + lives.ToString();
                Destroy(image2.gameObject);

                break;
            case 1:
                lives = lives -= 1;
                livesText.text = "Lives: " + lives.ToString();
                Destroy(image3.gameObject);
                break;

            default:
                break;
        }
    }

        void PauseGame()
    {
        Time.timeScale = 0;
    }

}