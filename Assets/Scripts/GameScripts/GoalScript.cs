using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    static MainScript mainScript;

    private void Awake()
    {
        mainScript = FindObjectOfType<MainScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            if (transform.position.x > 0) mainScript.addScoreP1();
            else mainScript.addScoreP2();

            mainScript.RstBall();
        }
    }

}
