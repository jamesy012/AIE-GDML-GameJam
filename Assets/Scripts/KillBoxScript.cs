using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBoxScript : MonoBehaviour {

    public GameObject playerCollider;
   
    private void Start()
    {
        playerCollider = GameObject.Find("PlayerCollider");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other == playerCollider.GetComponent<Collider>())
        {
            playerCollider.GetComponentInParent<Player>().Restart();
        }
    }
}
