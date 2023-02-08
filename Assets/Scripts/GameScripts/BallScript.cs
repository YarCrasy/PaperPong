using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField] AudioSource soundEffect;
    sbyte xDir, yDir;
    float vel = 3f, ang;
    bool firstBound = false, verticalPlayerBound = false, lastPlayer = false;

    private void Start()
    {
        setDir();
        setAngle();
    }

    private void Update()
    {
        BallMove();
    }

    void setDir()
    {
        int aux = Random.Range(0, 1);
        if (aux == 0) xDir = 1;
        else xDir = -1;
        aux = Random.Range(0, 1);
        if (aux == 0) yDir = 1;
        else yDir = -1;
    }

    void setAngle()
    {
        ang = Random.Range(0.1f, 10f);
    }

    float speedX()
    {
        return xDir * vel * Time.deltaTime;
    }
    float speedY()
    {
        return yDir * ang * Time.deltaTime;
    }

    void BallMove()
    {
        transform.Translate(new Vector2(speedX(), speedY()));
    }

    void addVel()
    {
        vel += 0.75f;
    }

    public float getY()
    {
        return transform.position.y;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        addVel();
        soundEffect.Play();
        int collisionLayer = collision.gameObject.layer;

        ang += Random.Range(0.25f, 2f);

        //rebote contra las paredes
        if (collisionLayer == 6) 
        {
            if (verticalPlayerBound) verticalPlayerBound = false;

            yDir *= -1;
        }

        //rebote contra jugadores
        else if (collisionLayer == 7)   
        {
            float collisionX = collision.gameObject.transform.position.x,
                  collisionY = collision.gameObject.transform.position.y;
            bool player = collision.gameObject.GetComponent<PlayerScript>().player;
            lastPlayer = player;

            if (!firstBound)
            {
                vel *= 2;
                firstBound = true;
            }

            if (!player)
            {
                if (collisionX < transform.position.x)
                {
                    xDir *= -1;
                }

            }
            else if (player)
            {
                if (collisionX > transform.position.x)
                {
                    xDir *= -1;
                }
            }
            else if (collisionY + 1 < transform.position.y)
            {
                yDir = 1;
                verticalPlayerBound = true;
            }
            else if (collisionY - 1 > transform.position.y)
            {
                yDir = +1;
                verticalPlayerBound = true;
            }

        }

        else if (collisionLayer == 7)
        {
            float collisionX = collision.gameObject.transform.position.x,
                  collisionY = collision.gameObject.transform.position.y,
                  collisionScalecaleY = collision.gameObject.transform.localScale.y;
            bool player = collision.gameObject.GetComponent<PlayerScript>().player;
            lastPlayer = player;

            if (!firstBound)
            {
                vel *= 2;
                firstBound = true;
            }

            if (!player)
            {
                if (collisionX < transform.position.x)
                {
                    xDir *= -1;
                }

            }
            else if (player)
            {
                if (collisionX > transform.position.x)
                {
                    xDir *= -1;
                }
            }
            else if (collisionY + collisionScalecaleY < transform.position.y)
            {
                yDir = 1;
                verticalPlayerBound = true;
            }
            else if (collisionY - collisionScalecaleY > transform.position.y)
            {
                yDir = +1;
                verticalPlayerBound = true;
            }

        }

    }

    public void resetBall()
    {
        firstBound = false;
        gameObject.transform.position = new Vector2(0, Random.Range(-3f, 3f));
        vel = 3f;
        setAngle();
        //xDir *= -1;   //el que recibe gol es el que le han metido la pelota, no hace falta invetir sentido
    }


    public bool getLastPlayer()
    {
        return lastPlayer;
    }

}
