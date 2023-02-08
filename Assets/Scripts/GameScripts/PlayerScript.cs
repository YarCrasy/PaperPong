using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] MainScript mainScript;
    float speed = 10f, iaSpped = 6.5f, originalSpeed, originalScale;    //la velocidad de la maquina será más lenta para balancear la dificultad
    int score = 0;
    [SerializeField] Text scoreDisplay;
    public bool player; //player1 = false // player2 = true
    bool canMoveUp = true, canMoveDown = true;
    static bool canUseControl1 = true, canUseControl2 = true;

    private void Awake()
    {
        originalSpeed = speed;
        originalScale = transform.localScale.y;

        scoreToText();
    }

    private void Update()
    {
        movePlayer();

        if (transform.localScale.y > originalScale)
        {
            transform.localScale -= new Vector3(0, 0.25f * Time.deltaTime, 0);
        }
        else
        {
            transform.localScale = new Vector2(transform.localScale.x, originalScale);
        }

        if (speed > originalSpeed)
        {
            speed -= 0.01f * Time.deltaTime;
        }
        else
        {
            speed = originalSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            if (collision.gameObject.transform.position.y > transform.position.y) canMoveUp = false;
            else if (collision.gameObject.transform.position.y < transform.position.y) canMoveDown = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            if (collision.gameObject.transform.position.y > transform.position.y) canMoveUp = true;
            else if (collision.gameObject.transform.position.y < transform.position.y) canMoveDown = true;
        }
    }

    void movePlayer()
    {
        if (!player)
        {
            if (mainScript.GetGameMode() == "Classic PvP" || mainScript.GetGameMode() == "Classic PvE")
            {
                if (doublePress1())
                {
                    //no se mueve
                }
                else if (Input.GetKey(KeyCode.W) && canMoveUp)
                {
                    transform.position += new Vector3(0, speed * Time.deltaTime, 0);
                }
                else if (Input.GetKey(KeyCode.S) && canMoveDown)
                {
                    transform.position += new Vector3(0, -speed * Time.deltaTime, 0);
                }
            }
            else if(mainScript.GetGameMode() == "Mirror Mode")
            {
                if (canUseControl1)
                {
                    if (doublePress1())
                    {
                        //no se mueve
                    }
                    else if (Input.GetKey(KeyCode.W) && canMoveUp)
                    {
                        transform.position += new Vector3(0, speed * Time.deltaTime, 0);
                        canUseControl2 = false;
                    }
                    else if (Input.GetKey(KeyCode.S) && canMoveDown)
                    {
                        transform.position += new Vector3(0, -speed * Time.deltaTime, 0);
                        canUseControl2 = false;
                    }
                    else if (Input.GetKeyUp(KeyCode.W) && Input.GetKeyUp(KeyCode.S))
                        canUseControl2 = true;
                }
                else
                {
                    if (doublePress1() || doublePress2())
                    {
                        //no se mueve
                    }
                    else if (Input.GetKey(KeyCode.DownArrow) && canMoveUp)
                    {
                        transform.position += new Vector3(0, speed * Time.deltaTime, 0);
                    }
                    else if (Input.GetKey(KeyCode.UpArrow) && canMoveDown)
                    {
                        transform.position += new Vector3(0, -speed * Time.deltaTime, 0);
                    }
                }
            }

        }
        else
        {
            if (mainScript.GetGameMode() == "Classic PvP")
            {
                if (doublePress2())
                {
                    //no se mueve
                }
                else if (Input.GetKey(KeyCode.UpArrow) && canMoveUp)
                {
                    transform.position += new Vector3(0, speed * Time.deltaTime, 0);
                }
                else if (Input.GetKey(KeyCode.DownArrow) && canMoveDown)
                {
                    transform.position += new Vector3(0, -speed * Time.deltaTime, 0);
                }
            }
            else if (mainScript.GetGameMode() == "Classic PvE")
            {
                followBall();
            }
            else if (mainScript.GetGameMode() == "Mirror Mode")
            {
                if (canUseControl2)
                {
                    if (doublePress2())
                    {
                        //no se mueve
                    }
                    else if (Input.GetKey(KeyCode.UpArrow) && canMoveUp)
                    {
                        transform.position += new Vector3(0, speed * Time.deltaTime, 0);
                        canUseControl1 = false;
                    }
                    else if (Input.GetKey(KeyCode.DownArrow)  && canMoveDown)
                    {
                        transform.position += new Vector3(0, -speed * Time.deltaTime, 0);
                        canUseControl1 = false;
                    }
                    else if (Input.GetKeyUp(KeyCode.DownArrow) && Input.GetKeyUp(KeyCode.UpArrow))
                        canUseControl1 = true;
                }
                else
                {
                    if (doublePress1() || doublePress2())
                    {
                        //no se mueve
                    }
                    else if ( Input.GetKey(KeyCode.S) && canMoveUp)
                    {
                        transform.position += new Vector3(0, speed * Time.deltaTime, 0);
                    }
                    else if (Input.GetKey(KeyCode.W) && canMoveDown)
                    {
                        transform.position += new Vector3(0, -speed * Time.deltaTime, 0);
                    }
                }
            }
        }
    }

    //comprueba que no está subiendo y bajando a la vez
    bool doublePress1()
    {
        return Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S);
    }

    bool doublePress2()
    {
        return Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.UpArrow);
    }

    void scoreToText()
    {
        scoreDisplay.text = score + "";
    }

    public void updateScore()
    {
        score++;
        scoreToText();
    }

    public void SetIASpeed()
    {
        originalSpeed = iaSpped;
    }
    public void SetSpeed()
    {
        originalSpeed = speed;
    }

    void followBall()
    {
        //if para "suavizar" el seguimiento de la pelota
        if (!((transform.position.y < mainScript.getBallY() + 0.01f) && (transform.position.y > mainScript.getBallY() - 0.01f))) 
        {
            if (mainScript.getBallY() > transform.position.y && canMoveUp)
            {
                transform.position += new Vector3(0, iaSpped * Time.deltaTime * (mainScript.getBallY() - transform.position.y), 0);
            }
            else if(mainScript.getBallY() < transform.position.y && canMoveDown)
            {
                transform.position += new Vector3(0, -iaSpped * Time.deltaTime * (transform.position.y - mainScript.getBallY()), 0);
            }
        }
    }

    public bool win()
    {
        if (score == 10) return true;
        else return false;
    }

    void RstScore()
    {
        score = 0;
        scoreToText();
    }

    void RstPosition()
    {
        if (!player)
        {
            transform.position = new Vector2(-6.1f, 0);
        }
        else
        {
            transform.position = new Vector2(6.1f, 0);
        }
    }

    public void RstPlayer()
    {
        RstScore();
        RstPosition();
    }

    public void addScale()
    {
        transform.localScale += new Vector3(0, 1, 0);
    }

    public void addSpeed()
    {
        speed += 3;
    }

    public void subtractScore()
    {
        score--;
        scoreToText();
    }

}
