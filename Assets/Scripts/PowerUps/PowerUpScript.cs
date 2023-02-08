using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    static MainScript main;
    static PlayerScript p1, p2;
    [SerializeField] uint typeOfPowerUp;
    /*
    si typeOfPowerUp = 0, alarga el jugador
    si typeOfPowerUp = 1, aumenta la velocidad del jugador
    si typeOfPowerUp = 2, resta punto al contrario
    */

    private void Awake()
    {
        main = FindObjectOfType<MainScript>();
        p1 = main.GetPlayer1();
        p2 = main.GetPlayer2();

    }
    
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int otherLayer = collision.gameObject.layer;

        if (otherLayer == 8)
        {
            bool player = collision.GetComponent<BallScript>().getLastPlayer();

            if (!player)
            {
                if (typeOfPowerUp == 0)
                {
                    p1.addScale();
                }
                else if (typeOfPowerUp == 1)
                {
                    p1.addSpeed();
                }
                else if (typeOfPowerUp == 2)
                {
                    p2.subtractScore();
                }
            }
            else
            {
                if (typeOfPowerUp == 0)
                {
                    p2.addScale();
                }
                else if (typeOfPowerUp == 1)
                {
                    p2.addSpeed();
                }
                else if (typeOfPowerUp == 2)
                {
                    p1.subtractScore();
                }
            }
            Destroy(gameObject);
        }
    }



}
