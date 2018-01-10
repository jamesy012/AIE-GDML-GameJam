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

        RigidbodyConstraints desiredConstraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        if (m_rigidbody.constraints != desiredConstraints) {
            Debug.LogWarning("Changing Player Constraints");
            m_rigidbody.constraints = desiredConstraints;
        }
    }

    // Update is called once per frame
    void Update() {
        //get key inputs
        bool keyLeft = Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.LeftArrow);
        bool keyRight = Input.GetKey(KeyCode.D) | Input.GetKey(KeyCode.RightArrow);

        float movement = 0;
        if (keyLeft) {
            movement += -1;
        }
        if (keyRight) {
            movement += 1;
        }
        Vector3 velocity = m_rigidbody.velocity;
        velocity.x = movement * m_moveSpeed * Time.deltaTime;
        m_rigidbody.velocity = velocity;
    }


}
