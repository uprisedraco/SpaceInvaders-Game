using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyBullet;

    [SerializeField]
    private bool shooter = false;

    private bool canShoot = false;

    [SerializeField]
    private bool motherhip = false;

    [SerializeField]
    private float reloadSpeed = 2f;

    [SerializeField]
    private Vector3 bulletOffset = new Vector3(0f, 0f, -5f);

    [SerializeField]
    private float mothershipSpeed = 2f;

    [SerializeField]
    private int enemyScore = 1;

    [SerializeField]
    private GameObject floatingText;

    private EnemyController enemyController;

    private void Awake()
    {
        canShoot = true;
    }

    private void Update()
    {
        if (motherhip)
            Move();

        if (shooter && canShoot)
            Shoot();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(motherhip)
            enemyController.MothershipDead();
        enemyController.RemoveShip(gameObject);
        GameManager.instance.UpdateScore(enemyScore);
        GameObject text = Instantiate(floatingText, new Vector3(transform.position.x, transform.position.y + 10f, transform.position.z), Quaternion.Euler(90f, 0f, 0f));
        text.GetComponent<FloatingText>().SetScore(enemyScore);
        Destroy(gameObject);
    }

    private void Move()
    {
        Vector3 position = transform.position;
        position.x += -transform.forward.x * mothershipSpeed * Time.deltaTime;
        transform.position = position;
    }

    private void OnBecameInvisible()
    {
        if(motherhip)
            enemyController.MothershipDead();
        enemyController.RemoveShip(gameObject);
        Destroy(gameObject);
    }

    private void Shoot()
    {
        if (Time.timeScale != 0)
        {
            Instantiate(enemyBullet, transform.position + bulletOffset, Quaternion.identity);
            canShoot = false;
            StartCoroutine("Reload", reloadSpeed);
        }
    }

    public void SetEnemyController(EnemyController enemyController)
    {
        this.enemyController = enemyController;
    }

    IEnumerator Reload(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        canShoot = true;
    }
}
