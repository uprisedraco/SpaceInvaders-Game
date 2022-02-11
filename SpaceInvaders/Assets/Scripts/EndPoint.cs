using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private bool hasCollide = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8 && !hasCollide)
        {
            hasCollide = true;
            GameManager.instance.LoseScreen();
        }
    }
}
