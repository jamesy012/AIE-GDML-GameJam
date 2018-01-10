using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Left/right movement
/// No jumping
/// Flip gravity (flip sprite??)
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

    private Rigidbody m_rigidbody;

    public float m_moveSpeed = 100;

    // Use this for initialization
    void Start() {
        m_rigidbody = GetComponent<Rigidbody>();

        Collider collider = GetComponent<Collider>();
        if(collider.material == null || collider.material.name.Contains("Default")) {
            Debug.LogError("The player colldier should have the FrictionLess Physics Material");
        }

        //set up the constraints
        RigidbodyConstraints desiredConstraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        if (m_rigidbody.constraints != desiredConstraints) {
            Debug.LogWarning("Changing Player Constraints - Freeze position Z | Freeze all rotation;");
            m_rigidbody.constraints = desiredConstraints;
        }
    }

    // Update is called once per frame
    void Update() {
        //get key inputs
        bool keyLeft = Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.LeftArrow);
        bool keyRight = Input.GetKey(KeyCode.D) | Input.GetKey(KeyCode.RightArrow);
        bool keyGravityFlip = Input.GetKeyDown(KeyCode.W) | Input.GetKeyDown(KeyCode.UpArrow) | Input.GetKeyDown(KeyCode.Space);

        //calc direction from key inputs
        int movement = 0;
        if (keyLeft) {
            movement += -1;
        }
        if (keyRight) {
            movement += 1;
        }

        //do movement
        SideMovement(movement);

        //go gravity flip
        if (keyGravityFlip) {
            FlipGravity();
        }

    }

    private void SideMovement(int a_movementDirection) {
        Vector3 velocity = m_rigidbody.velocity;
        velocity.x = a_movementDirection * m_moveSpeed * Time.deltaTime;
        m_rigidbody.velocity = velocity;
    }

    private void FlipGravity() {
        Vector3 gravity = Physics.gravity;
        gravity.y *= -1;
        Physics.gravity = gravity; 
    }


}
