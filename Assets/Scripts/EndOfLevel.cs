using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevel : MonoBehaviour
{
    Collider playerCollider;

    public GameObject newLevel;

    private void Start()
    {
        playerCollider = GameObject.Find("Player").GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == playerCollider)
        {
            GameObject.Find("Player").GetComponent<PlayerRespawn>().currentLevel = newLevel;
        }
    }
}
