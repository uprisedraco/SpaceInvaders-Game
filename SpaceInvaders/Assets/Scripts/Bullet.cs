using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 25f;

    [SerializeField]
    private Vector3 direction = new Vector3(0f, 0f, 1f);

    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += direction * bulletSpeed * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
