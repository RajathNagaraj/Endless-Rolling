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
    [Range(0,15)]
    public float rollSpeed = 1f;
    /// <summary>
    /// Using a value to prevent ball from bouncing up and down for some reason
    /// </summary>
    private const float CLAMP_Y = 0.55f;

    private GameController gameController;
    public InputStyleObject inputStyleObject;
    private MobileHorizontalMovement horizontalMovement; 

    [Header("Scale Properties")]
    [Tooltip("The minimum size (in Unity units) that the Player should be ")]
    public float minScale = 0.5f;
    [Tooltip("The maximum size (in Unity units) that the Player should be ")]
    public float maxScale = 3f;
    /// <summary>
    /// The current scale of the Player
    /// </summary>
    private float currentScale = 1f;


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



    private void Awake()
    {
        //gameController = GameObject.FindObjectOfType<GameController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        //Setting the Player speed and dodge speed according to difficulty
        //SetSpeed(gameController.difficultySetting.mode);

        //Set the Player's initial position to (0,0,0)
        transform.position = Vector3.zero;
        rb = GetComponent<Rigidbody>();
        //Set the minimum distance to be swiped in pixel units
        minSwipeDistancePixels = minSwipeDistance * Screen.dpi;
        //Setting the input style
        horizontalMovement = inputStyleObject.horizMovement;
        
        Debug.Log("PlayerBehaviour : "+ horizontalMovement);
    }

    /*
    private void SetSpeed(GameMode mode)
    {
        switch(mode)
        {
            case GameMode.Easy:
                rollSpeed = 7f;
                break;
            case GameMode.Medium:
                rollSpeed = 9f;
                break;
            case GameMode.Hard:
                rollSpeed = 10f;
                break;

            default:
                break;
        }
    }
    */

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

        ClampPlayerVertical();
        

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
            //Store the first touch detected
            Touch touch = Input.touches[0];
            if (horizontalMovement == MobileHorizontalMovement.SwipeGesture)
            {                
                SwipeTeleport(touch);
            }
            TouchObjects(touch);
            ScalePlayer();
            
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

            rb.AddForce(-moveDirection * swipeMove, 0, 0,ForceMode.Impulse);

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
    /// <summary>
    /// Will change the Player's scale via pinching and stretching
    /// </summary>
    private void ScalePlayer()
    {
        //Check that there are exactly two touches when scaling the Player
        if(Input.touchCount != 2)
        {
            return;
        }

        else
        {
            //Store the first two touches
            Touch touch0 = Input.touches[0];
            Touch touch1 = Input.touches[1];
            //Find the position of the touches in the previous frame
            Vector2 prevTouch0 = touch0.position - touch0.deltaPosition;
            Vector2 prevTouch1 = touch1.position - touch1.deltaPosition;
            //Find the distance(or magnitude) between the touches in each frame
            float prevTouchDeltaMag = (prevTouch0 - prevTouch1).magnitude;
            float touchDeltaMag = (touch0.position - touch1.position).magnitude;

            //Find the difference in the distances between each frame
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
            //Keep the chaange consistent no matter the framerate
            float newScale = currentScale - (deltaMagnitudeDiff * Time.deltaTime);
            //Ensure that the value is valid
            newScale = Mathf.Clamp(newScale ,minScale ,maxScale );
            //Update the Player's Scale
            transform.localScale = (newScale * Vector3.one);
            //Set our current scale for the next frame
            currentScale = newScale;
        }
    }

    /// <summary>
    /// Will determine if we are calling a game object 
    /// and if so, call events for it
    /// </summary>
    /// <param name="touch">Our touch event</param>
    private static void TouchObjects(Touch touch)
    {
        //Convert the position into a Ray
        Ray touchRay = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit hit;

        //Create a layer mask that will collide with all possible channels
        int layerMask = ~0;

        //Are we touching an object with a collider?
        if(Physics.Raycast(touchRay,out hit,Mathf.Infinity, layerMask , QueryTriggerInteraction.Ignore))
        {
            //Call the PlayerTouch method if it exists on a component attached to this object
            hit.transform.SendMessage("PlayerTouch",SendMessageOptions.DontRequireReceiver);
        }

    }

    private void ClampPlayerVertical()
    {
        //code to prevent Player from bouncing up and down
        float yPos = transform.position.y;
        yPos = Mathf.Clamp(yPos, 0, CLAMP_Y);
        Vector3 pos = new Vector3(transform.position.x, yPos, transform.position.z);
        transform.position = pos;
    }
}
