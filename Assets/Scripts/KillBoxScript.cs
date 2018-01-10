using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBoxScript : MonoBehaviour {

    public Collider playerCollider;

    private void Start()
    {
        playerCollider = GameObject.Find("Player").GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other == playerCollider)
        {
            GameObject.Find("Player").GetComponent<PlayerRespawn>().Respawn();
        }
    }
}
