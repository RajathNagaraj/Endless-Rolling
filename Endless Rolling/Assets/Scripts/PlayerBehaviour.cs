using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour
{
    /// <summary>
    /// A reference to the rigidbody component
    /// </summary>
    private Rigidbody rb;
    [Tooltip("How fast the ball moves left/right")]
    public float dodgeSpeed = 5f;
    [Tooltip("How fast the ball moves forward automatically")]
    [Range(0,10)]
    public float rollSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector3.zero;
        rb = GetComponent<Rigidbody>();
    }
           
    /// <summary>
    /// FixedUpdate is called at a fixed rate and is a prime place to put anything based on time
    /// </summary>
    private void FixedUpdate()
    {
        var horizontalSpeed = Input.GetAxis("Horizontal") * dodgeSpeed;
        rb.AddForce(horizontalSpeed, 0, rollSpeed);
    }
}
