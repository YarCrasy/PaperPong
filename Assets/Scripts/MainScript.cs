using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//script para funciones generales y/o que no se tiene claro en que gameobject poner

public class MainScript : MonoBehaviour
{

    [SerializeField] PlayerScript player1, player2;
    [SerializeField] BallScript ball;
    [SerializeField] PowerUpsController puController;
    [SerializeField] Text modeDisplay;
    [SerializeField] GameObject[] othersObjects, menuObjects, winMsg, winMenuOptions;

    static string[] gameModes = {"Classic PvP", "Classic PvE", "Mirror Mode"};
    static string actualGameMode;

    private void Awake()
    {
        QualitySettings.vSyncCount= 0;
        Application.targetFrameRate = 45;
        player1.player = false;
        player2.player = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckWin();
    }

    public float getBallY()
    {
        return ball.transform.position.y;
    }

    public bool getBallLastPlayer()
    {
        return ball.getLastPlayer();
    }

    public string GetGameMode()
    {
        return actualGameMode;
    }

    public PlayerScript GetPlayer1()
    {
        return player1;
    }
    public PlayerScript GetPlayer2()
    {
        return player2;
    }

    public void addScoreP1()
    {
        player1.updateScore();
    }
    public void addScoreP2()
    {
        player2.updateScore();
    }

    public void setGameMode(int x)
    {
        modeDisplay.text = actualGameMode = gameModes[x];
    }

    public void SetActiveGameObjects(bool activate)
    {
        player1.gameObject.SetActive(activate);
        player2.gameObject.SetActive(activate);
        ball.gameObject.SetActive(activate);
        for (int i = 0; i < othersObjects.Length; i++)
        {
            othersObjects[i].SetActive(activate);
        }
        if (puController.PowerUpInScene())
        {
            Destroy(FindObjectOfType<PowerUpScript>().gameObject);
        }
    }

    public void SetActiveMenuObjects(bool activate)
    {
        for (int i = 0; i < menuObjects.Length; i++)
        {
            menuObjects[i].SetActive(activate);
        }
    }

    public void SetActiveWinMenu(bool activate)
    {
        if (!activate)
        {
            winMsg[0].SetActive(false);
            winMsg[1].SetActive(false);
        }
        for (int i = 0; i < winMenuOptions.Length; i++)
        {
            winMenuOptions[i].SetActive(activate);
        }
    }

    public void SetActivePowerUps(bool activate)
    {
        puController.enabled= activate;
    }

    void CheckWin()
    {
        if (player1.win())
        {
            winMsg[0].SetActive(true);
            SetActiveGameObjects(false);
            SetActiveWinMenu(true);
            RstGame();
        }
        else if (player2.win())
        {
            winMsg[1].SetActive(true);
            SetActiveGameObjects(false);
            SetActiveWinMenu(true);
            RstGame();
        }
    }

    public void RstGame()
    {
        RstBall();
        RstPlayer();
    }

    public void RstBall()
    {
        ball.resetBall();
    }

    void RstPlayer()
    {
        player1.RstPlayer();
        player2.RstPlayer();
    }


    public void changeText(Text t)
    {
        if (t.text == "Power-ups activated")
        {
            t.text = "Power-ups deactivated";
        }
        else if (t.text == "Power-ups deactivated")
        {
            t.text = "Power-ups activated";
        }
    }



}

/*
*****Consideraciones para el desarrollo*****
1) inputs: w,s para jugador 1 y flechas arriba y abajo para el 2                                        DONE
2) fisicas: detecta colisiones, pero el movimiento se realizará atraves de transform                    DONE
3) efectos de sonido: sacado de la pagina freesounds.org                                                DONE
4) condiciones de ganador: 10 puntos, menu para reiniciar                                               DONE
5) aumento de velocidad de la pelota por rebote                                                         DONE
6) la pelota aparece hacia el jugador que le hayan metido la pelota (recibir gol) cada respawn          DONE
7) power-ups: la pelota proporciona mejora al ultimo jugador que le tocó al chocar con un power-up
    7.1) alarga el jugador, se va acortando lentamente                                                  DONE
    7.2) aumentar ligeramente la velocidad del jugador                                                  DONE
    7.3) restar un punto al contrario                                                                   DONE
8) implementaciones extras
    8.1) Mirror mode: un jugador, controlar 2 raquetas un las flechas, una de ellas estará invertida    DONE
    8.2) Mostrar tutorial/guia de cada power up y control del jugador                                   DONE

*/