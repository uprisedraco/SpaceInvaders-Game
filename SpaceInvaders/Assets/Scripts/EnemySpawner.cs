using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> enemyPrefabs;

    private EnemyController enemyController;

    private int[,] shipsMatrix = new[,]
    {
        {0,0,0,0,0,0 },
        {0,0,0,0,1,1 },
        {0,0,0,0,0,0 },
        {0,0,0,0,1,1 },
        {0,0,0,0,0,0 },
        {0,0,0,0,0,0 }
    };

    private void Awake()
    {
        enemyController = GetComponent<EnemyController>();
        SpawnEnemies();
    }

    public void SpawnMotherShip()
    {
        Vector3 spawnPosition = new Vector3(-32f, transform.position.y, transform.position.z - 8f);
        Quaternion spawnRotation = Quaternion.Euler(0f, -90f, 0f);
        if(Random.value > 0.5f)
        {
            spawnPosition.x *= -1;
            spawnRotation = Quaternion.Inverse(spawnRotation);
        }
        GameObject ship = Instantiate(enemyPrefabs[2], spawnPosition, spawnRotation);
        ship.GetComponent<Enemy>().SetEnemyController(enemyController);
        enemyController.AddEnemyShip(ship);
    }

    private void SpawnEnemies()
    {
        int couter = 0;
        float z = transform.position.z;
        while (couter < 6)
        {
            float x = 2;
            for (int i = 1; i <= 6; i++)
            {
                GameObject ship = Instantiate(enemyPrefabs[shipsMatrix[couter, i - 1]], new Vector3(x, 0, z), Quaternion.identity);
                ship.GetComponent<Enemy>().SetEnemyController(enemyController);
                enemyController.AddEnemyShip(ship);
                ship.transform.parent = gameObject.transform;
                x *= -1;
                if (i % 2 == 0)
                    x += 4;
            }
            couter++;
            z -= 8;
        }
    }
}
