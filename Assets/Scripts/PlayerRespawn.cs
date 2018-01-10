using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour{


    public GameObject respawnPoint;
    Transform playerPosition;

    public void Respawn()
    {
        playerPosition = respawnPoint.transform;
        transform.position = playerPosition.transform.position;
        Physics.gravity = new Vector3(0, -9.81f, 0);
    }
}
