using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject playerBullet;

    [SerializeField]
    private Vector3 bulletOffset = new Vector3(0f, 0f, 5f);

    [SerializeField]
    private float reloadSpeed = 1f;

    [SerializeField]
    private float movementSpeed = 40f;

    private bool canShoot = true;

    void Update()
    {
        Move();

        if (Input.GetKey(KeyCode.Space))
            Shoot();
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x + x * movementSpeed * Time.deltaTime, -25f, 25f);
        transform.position = position;
    }

    private void Shoot()
    {
        if (canShoot && Time.timeScale != 0)
        {
            Instantiate(playerBullet, transform.position + bulletOffset, Quaternion.identity);
            canShoot = false;
            StartCoroutine("Reload", reloadSpeed);
        }
    }

    IEnumerator Reload(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        canShoot = true;

    }

    private void OnCollisionEnter(Collision collision)
    {
        GameManager.instance.RemoveHealth();
        Destroy(gameObject);
    }
}
