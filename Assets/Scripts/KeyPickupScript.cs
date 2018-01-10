using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickupScript : MonoBehaviour
{
    Collider playerCollider;
    
    private void Start()
    {
        playerCollider = GameObject.Find("Player").GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == playerCollider)
        {
            GameObject.Find("Player").GetComponent<PlayerRespawn>().currentLevel.GetComponent<Level>().AddKey();
            Destroy(gameObject);
        }
        
    }
}
