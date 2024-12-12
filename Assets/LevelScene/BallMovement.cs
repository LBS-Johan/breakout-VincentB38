using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Threading;


public class BallMovement : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D BallBody;
    public float speed = 4f;
    public TextMeshProUGUI ScoreText;
    public GameObject CloneObject;
    int Points = 0;
    int PointsNeeded; //  hur m�nga po�ng saker

    void Start()
    {
        Thread.Sleep(1000); // v�nta 1s innan spelet b�rjar
        BallBody = GetComponent<Rigidbody2D>();
        BallBody.velocity = new Vector2(0, -1);
        ScoreText.text = "Points: " + Points;

        foreach (GameObject obj in FindObjectsOfType<GameObject>()) //kolla hur m�nga po�ng spelaren beh�ver
        {
            if (obj.name == "PointsPart")
            {
                PointsNeeded++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        BallBody.velocity = BallBody.velocity.normalized * speed;

        Vector2 direction = BallBody.velocity.normalized;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //f� po�ng
        if (collision.gameObject.name == "PointsPart" && gameObject.name == "Ball")
        {
            int Chance = Random.Range(1, 5);

            if (Chance == 1)
            {
                GameObject newClone = Instantiate(CloneObject, collision.gameObject.transform.position, Quaternion.identity);
                newClone.name = "MoreBalls";

                Rigidbody2D CloneBody = newClone.GetComponent<Rigidbody2D>();
            }

            Destroy(collision.gameObject);
            Points += 1;
            ScoreText.text = "Points: " + Points;

            if (Points >= PointsNeeded)
            {
                SceneManager.LoadScene(2);
            }

        }
        //v�gg
        else if (collision.gameObject.name == "Wall")
        {
           BallBody.velocity += new Vector2(0, -1);
        }
        //f�rlora
        else if (collision.gameObject.name == "LosePart")
        {
            SceneManager.LoadScene(1);
        }
        //funktion f�r att r�ra spelae
        else if (collision.gameObject.name == "Player")
        {
            TouchedPlayer(collision);
        }

        //else if (collision.gameObject.name == "MoreBalls") -- blev inte f�rdig och hade lite problem
       // {
       //     GameObject newClone = Instantiate(gameObject, collision.gameObject.transform.position, Quaternion.identity);
        //    Destroy(collision.gameObject);
        //}
    }

    // olika direktioner baserade p� tr�ff
    void TouchedPlayer(Collision2D collision)
    {
        Vector2 contactPoint = collision.GetContact(0).point;
        Vector2 PlayerCenter = collision.transform.position;

        float offset = contactPoint.x - PlayerCenter.x;
        float paddleWidth = collision.collider.bounds.size.x / 2;
        float normalizedOffset = offset / paddleWidth;

        Vector2 newDirection = new Vector2(normalizedOffset, 1).normalized;

        BallBody.velocity = newDirection * speed;
    }
}
