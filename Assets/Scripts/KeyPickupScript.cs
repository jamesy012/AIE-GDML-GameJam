using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickupScript : MonoBehaviour
{
    Collider playerCollider;
    AudioClip keyPickup;
    
    private void Start()
    {
        playerCollider = GameObject.Find("PlayerTrigger").GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == playerCollider)
        {
            SoundManager.PlaySFX(keyPickup);
            GameObject.Find("Player").GetComponent<Player>().m_currentLevel.GetComponent<Level>().AddKey();
            this.gameObject.SetActive(false);
        }
        
    }
}
