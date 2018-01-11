using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBoxScript : MonoBehaviour {

    public GameObject playerCollider;
    public AudioClip deathSound;

    private void Start()
    {
        playerCollider = GameObject.Find("PlayerTrigger");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other == playerCollider.GetComponent<Collider>())
        {
            playerCollider.GetComponentInParent<Player>().Restart();
            SoundManager.PlaySFX(deathSound);
        }
    }
}
