using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Will adjust the camera to follow and face a target
/// </summary>
public class CameraBehaviour : MonoBehaviour
{
    [Tooltip("The object that the camera should be looking at")]
    public Transform target;
    [Tooltip("The offset from the target position")]
    public Vector3 offset = new Vector3( 0, 3, -6);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Set the camera to follow target
        transform.position = target.position + offset;
        //Set the camera to look at the target
        transform.LookAt(target);
    }
}
