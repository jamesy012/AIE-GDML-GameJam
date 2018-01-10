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

    /// <summary>
    /// reference to the rigidbody
    /// </summary>
    private Rigidbody m_rigidbody;

    /// <summary>
    /// movement speed of the player
    /// </summary>
    public float m_moveSpeed = 100;

    /// <summary>
    /// flag to check if the player is grounded
    /// </summary>
    public bool m_isGrounded = false;

    private LayerMask m_playerLayerMask;

    // Use this for initialization
    void Start() {
        m_rigidbody = GetComponent<Rigidbody>();
        m_playerLayerMask = LayerMask.NameToLayer("Player");

        if(gameObject.layer != m_playerLayerMask) {
            Debug.LogError("Please make the players layer to be 'Player'");
        }

        Collider collider = GetComponent<Collider>();
        if (collider != null) {
            if (collider.material == null || collider.material.name.Contains("Default")) {
                Debug.LogError("The player colldier should have the FrictionLess Physics Material");
            }
        } else {
            Debug.LogError("The player does not have a collider");
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
        //calc grounded
        //work out which way the gravity is going
        float isGravityUp = Mathf.Sign(Physics.gravity.y);
        //the character is offset from being centered, it's anchored to the bottom side of the player
        Vector3 playerCharacterOffset = new Vector3(0, Mathf.Clamp01(Physics.gravity.y) * 2, 0);
        //draw a debug ray to show off where the ray is
        Debug.DrawRay(transform.position + playerCharacterOffset, Vector3.up * isGravityUp * 2);
        //finally work out if the player is on the ground
        m_isGrounded = Physics.Raycast(transform.position + playerCharacterOffset, Vector3.up * isGravityUp, 2, ~m_playerLayerMask.value);
        //m_isGrounded = Physics.Raycast(transform.position + offset, Vector3.up * isGravityUp, out hit, 2, ~m_playerLayerMask.value);

        //if the player is not grounded dont run the movement/gravity flip code
        if (!m_isGrounded) {
            return;
        }

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
