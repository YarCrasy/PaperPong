using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsController : MonoBehaviour
{
    [SerializeField] GameObject[] powerUps;
    GameObject objectInGame;
    float spawnTimer = 0;

    private void Update()
    {
        if (objectInGame == null)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer > 5)
            {
                spawnTimer = 0;
                float x = Random.Range(-4.5f, 4.5f),
                      y = Random.Range(-3.5f, 3.5f);
                int i = Random.Range(0, powerUps.Length);
                objectInGame = Instantiate(powerUps[i], new Vector3(x, y, 0), Quaternion.identity);
            }
        }
    }

    public bool PowerUpInScene()
    {
        if (objectInGame == null) return false;
        else return true;
    }

}
