using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemySpawner spawner;

    [SerializeField]
    private float enemySpeed = 5f;

    private float xDirection = 1f;

    private List<GameObject> enemies = new List<GameObject>();

    private bool spawnMothershipAllowed = false;

    private void Awake()
    {
        spawner = GetComponent<EnemySpawner>();
        spawnMothershipAllowed = true;
    }

    void Update()
    {
        Move();

        if(Time.timeScale != 0 && spawnMothershipAllowed)
        {
            float chance = Random.Range(0f, 10f);
            if (chance < 4f)
            {
                spawnMothershipAllowed = false;
                spawner.SpawnMotherShip();
            }
            else
            {
                spawnMothershipAllowed = false;
                StartCoroutine("Delay");
            }
        }
    }

    private void Move()
    {
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x + xDirection * enemySpeed * Time.deltaTime, -12.5f, 12.5f);
        transform.position = position;

        if (transform.position.x == -12.5f || transform.position.x == 12.5f)
        {
            MoveDown();
            ChangeSpeed();
            ChangeDirection();
        }
    }

    public void MothershipDead()
    {
        spawnMothershipAllowed = true;
    }

    private void ChangeDirection()
    {
        xDirection *= -1;
    }

    private void ChangeSpeed()
    {
        enemySpeed += 1.25f;
    }

    private void MoveDown()
    {
        transform.position = transform.position + new Vector3(0f, 0f, -8f);
    }

    public void AddEnemyShip(GameObject ship)
    {
        enemies.Add(ship);
    }

    public void RemoveShip(GameObject ship)
    {
        enemies.Remove(ship);
        if (enemies.Count == 0)
            GameManager.instance.WinScreen();
    }

    IEnumerator Delay()
    {
        yield return new WaitForSecondsRealtime(4f);
        spawnMothershipAllowed = true;
    }
}
