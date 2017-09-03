using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public string currentColor;
    public float jumpForce = 9.8f;
    public Rigidbody2D circle;

    public SpriteRenderer spriteRenderer;

    public List<Color> colorsValues;

    public static int score = 0;
    public Text scoreText;

    public GameObject obstacle;
    public GameObject colorSwitch;

    public Camera cam;

    // Use this for initialization
    void Start()
    {
        setRandomColor();
    }


    void Update()
    {
        if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
            circle.velocity = Vector2.up * jumpForce;

        if (cam.WorldToScreenPoint(circle.position).y < 0)
            die();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "ColorChanger")
        {
            onColorChangerCollision();
            Destroy(collision.gameObject);
            return;
        }

        if (collision.tag == "Score")
        {
            incScore();
            Destroy(collision.gameObject);
            return;
        }

        if (collision.tag != currentColor)
            die();
    }

    public void onColorChangerCollision()
    {
        setRandomColor();
        int rand = UnityEngine.Random.Range(1, 4);

        Instantiate(colorSwitch, new Vector3(transform.position.x, transform.position.y + (8f * rand), transform.position.z), transform.rotation);

    }


    private void incScore()
    {
        score++;
        scoreText.text = score.ToString();

        Instantiate(obstacle, new Vector3(transform.position.x, transform.position.y + 8f, transform.position.z), transform.rotation);


    }

    private void die()
    {
        Debug.Log("Died");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        score = 0;
    }


    void setRandomColor()
    {
        int rand = UnityEngine.Random.Range(0, CSConstants.colors.Length);
        Debug.Log(rand);
        currentColor = CSConstants.colors[rand];
        spriteRenderer.color = colorsValues[rand];
    }
}
