using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickupScript : MonoBehaviour
{
    public Collider playerCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other == playerCollider)
        {
            Destroy(gameObject);
        }
        
    }
}
