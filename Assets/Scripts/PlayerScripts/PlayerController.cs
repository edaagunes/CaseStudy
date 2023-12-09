using Blended;
using UnityEngine;

public class PlayerController : MonoSingleton<PlayerController>
{
    // Reference to the joystick for input
    public DynamicJoystick dynamicJoystick;
    public float speed;
    public float turnSpeed;

    public Animator playerAnimator;

    private TouchManager touchManager;

    // Hash for the "Run" parameter in the Animator
    
    private static readonly int Run = Animator.StringToHash("Run");
    //private static readonly int Idle = Animator.StringToHash("Idle");
    //private static readonly int Run = Animator.StringToHash("Run");

    private void Awake()
    {
        // Accessing the TouchManager instance
        touchManager = TouchManager.Instance;
    }

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Subscribe to touch events when the script is enabled
    public void OnEnable()
    {
        touchManager.onTouchMoved += OnTouchMoved;
        touchManager.onTouchEnded += OnTouchEnded;
    }

    // Unsubscribe from touch events when the script is disabled
    public void OnDisable()
    {
        touchManager.onTouchMoved -= OnTouchMoved;
        touchManager.onTouchEnded -= OnTouchEnded;
    }


    // Callback function for touch movement
    private void OnTouchMoved(TouchInput touchInput)
    {
        playerAnimator.SetBool(Run, true);
        
        // Get input from the joystick for movement
        float horizontal = dynamicJoystick.Horizontal;
        float vertical = dynamicJoystick.Vertical;
        
        // Calculate the movement vector based on joystick input and speed
        Vector3 addedPos = new Vector3(horizontal * speed * Time.deltaTime, 0, vertical * speed * Time.deltaTime);
        var transform1 = transform;
        transform1.position += addedPos;

        // Calculate the direction for player rotation based on input
        Vector3 direction = Vector3.forward * vertical + Vector3.right * horizontal;
        transform.rotation = Quaternion.Slerp(transform1.rotation, Quaternion.LookRotation(direction),
            turnSpeed * Time.deltaTime);
    }

    // Callback function for touch end event
    private void OnTouchEnded(TouchInput touch)
    {
        // Set the "Run" animation to false when touch input ends
        playerAnimator.SetBool(Run, false);
    }
    
    
}