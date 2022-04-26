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

    public enum MobileHorizontalMovement
    {
        Accelerometer,
        ScreenTouch,
        SwipeGesture
    }

    [Tooltip("How you want to implement horizontal movement")]
    public MobileHorizontalMovement horizontalMovement = MobileHorizontalMovement.Accelerometer;

    [Header("Swipe Properties")]
    [Tooltip("How far we want the Player to move upon swiping")]
    public float swipeMove = 2f;
    [Tooltip("How far the User has to swipe(in inches) to execute Action")]
    public float minSwipeDistance = .25f;
    /// <summary>
    /// Used to hold the value of minSwipeDistance converted to Pixels
    /// </summary>
    private float minSwipeDistancePixels;
    /// <summary>
    /// Stores the starting position of mobile touch events
    /// </summary>
    private Vector2 touchStart;



    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector3.zero;
        rb = GetComponent<Rigidbody>();
        minSwipeDistancePixels = minSwipeDistance * Screen.dpi;
    }

    private float CalculateMovement(Vector3 pixelPos)
    {
        var worldPos = Camera.main.ScreenToViewportPoint(pixelPos);
        float xMove = 0f;
        if (worldPos.x < 0.5f)
        {
            xMove = -1;
        }
        else
        {
            xMove = 1;
        }

        return xMove * dodgeSpeed;
    }
           
    /// <summary>
    /// FixedUpdate is called at a fixed rate and is a prime place to put anything based on time
    /// </summary>
    private void FixedUpdate()
    {
        var horizontalSpeed = 0f;

#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
         horizontalSpeed = Input.GetAxis("Horizontal") * dodgeSpeed;

        if (Input.GetMouseButton(0))
        {           
            horizontalSpeed = CalculateMovement(Input.mousePosition);
        }

#elif UNITY_IOS || UNITY_ANDROID
        
        if(horizontalMovement == MobileHorizontalMovement.Accelerometer)
        {
            //Move Player based on direction of Accelerometer
            horizontalSpeed = Input.acceleration.x * dodgeSpeed;

        }

        if(Input.touchCount > 0)
        {
            if (horizontalMovement == MobileHorizontalMovement.ScreenTouch)
            {
                Touch touch = Input.touches[0];
                horizontalSpeed = CalculateMovement(touch.position);
            }

                            
            

        }
#endif

        rb.AddForce(horizontalSpeed, 0, rollSpeed);
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update()
    {
#if UNITY_IOS || UNITY_ANDROID
        //check if Input has registered more than 0 touches
        if (Input.touchCount > 0)
        {
            if(horizontalMovement == MobileHorizontalMovement.SwipeGesture)
            {
                //Store the first touch detected
                Touch touch = Input.touches[0];
                SwipeTeleport(touch);
            }
            
        }
#endif
    }

    /// <summary>
    /// Will move the Player left/right according to touch
    /// </summary>
    /// <param name="touch">Current touch event</param>
    private void SwipeTeleport(Touch touch)
    {
       //Check if the touch just started
        if(touch.phase == TouchPhase.Began)
        {
            //If so, set touchStart
            touchStart = touch.position;
        }
        //if the touch has ended
       else if(touch.phase == TouchPhase.Ended)
        {
            //Get the position the touch ended at
            Vector2 touchEnd = touch.position;
            //Calculate the difference of touchEnd and touchStart on the x axis
            float xDiff = touchEnd.x - touchStart.x;
            //If we are not moving far enough dont do the movement
            if((Mathf.Abs(xDiff)) < minSwipeDistancePixels)
            {
                return;
            }
            float moveDirection;
            //If moved positively on the x axis, then move right
            if(touchStart.x > touchEnd.x)
            {
                moveDirection = 1f;
            }
            //Otherwise move left 
            else
            {
                moveDirection = -1f;
            }

            rb.AddForce(moveDirection * swipeMove, 0, 0,ForceMode.Impulse);

            /*
            RaycastHit hit;
            //Only move if we dont hit something
            if(!rb.SweepTest(moveDirection,out hit, swipeMove))
            {
                //Move the Player
                //rb.MovePosition(rb.position + );
                Vector3 swipeForce = ;
               
            }

            */



        }

       
    }
}
