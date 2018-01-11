using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickupScript : MonoBehaviour
{
    Collider playerCollider;
    
    private void Start()
    {
        playerCollider = GameObject.Find("PlayerTrigger").GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == playerCollider)
        {
            GameObject.Find("Player").GetComponent<Player>().m_currentLevel.GetComponent<Level>().AddKey();
            this.gameObject.SetActive(false);
        }
        
    }
}
