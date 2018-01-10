using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBoxScript : MonoBehaviour {

    public GameObject playerCollider;
    //public GameObject currentLevel;

    private void Start()
    {
        playerCollider = GameObject.Find("Player");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other == playerCollider.GetComponent<Collider>())
        {
            playerCollider.GetComponent<Player>().Restart();
        }
    }
}
