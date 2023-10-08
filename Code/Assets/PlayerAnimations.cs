using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerAnimations : MonoBehaviour
{
    Animator animator; 
    CharacterController controller = null; 
    float velocityZ = 0.0f; 
    float velocityX = 0.0f;
    public float acceleration = 2.0f; 
    public float deceleration = 2.0f; 
    public float maximumWalkVelocity = 2.0f; 
    public float maximumRunVelocity = 6.0f;
    int VelocityZHash; int VelocityXHash;

    Vector2 currentMouseDelta = Vector2.zero; 
    Vector2 currentMouseDeltaVelocity = Vector2.zero;
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;

    [SerializeField] Transform playerCamera = null;
    [SerializeField] float mouseSensitivity = 3.5f; 
    
    float cameraPitch = 0.0f;

    public float gravityForce = 9.81f;
    private bool isGrounded = false;

    void Start()
    {
        animator = GetComponent<Animator>(); 
        controller = GetComponent<CharacterController>();

        VelocityZHash = Animator.StringToHash("Velocity Z"); 
        VelocityXHash = Animator.StringToHash("Velocity X");
    }

    void ChangeVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool backPressed, float currentMaxVelocity)
    {
        if (forwardPressed && velocityZ < currentMaxVelocity) 
        { 
            velocityZ += Time.deltaTime * acceleration; 
        }

        if (leftPressed && velocityX > -currentMaxVelocity) 
        { 
            velocityX -= Time.deltaTime * acceleration; 
        }

        if (rightPressed && velocityX < currentMaxVelocity) 
        { 
            velocityX += Time.deltaTime * acceleration; 
        }

        if (backPressed && velocityZ > -currentMaxVelocity) 
        { 
            velocityZ -= Time.deltaTime * acceleration; 
        }

        if (!forwardPressed && velocityZ > 0.0f) 
        { 
            velocityZ -= Time.deltaTime * deceleration; 
        }

        if (!backPressed && velocityZ < 0.0f) 
        { 
            velocityZ += Time.deltaTime * deceleration; 
        }

        if (!rightPressed && velocityX > 0.0f) 
        { 
            velocityX -= Time.deltaTime * deceleration; 
        }

        if (!leftPressed && velocityX < 0.0f) 
        { 
            velocityX += Time.deltaTime * deceleration; 
        }

    }
    void LockOrResetVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool backPressed, bool runPressed, float currentMaxVelocity)
    {
        if (!forwardPressed && !backPressed && velocityZ != 0.0f && (velocityZ > -0.5f && velocityZ < 0.05f)) 
        { 
            velocityZ = 0.0f; 
        }

        if (!rightPressed && !leftPressed && velocityX != 0.0f && (velocityX > -0.5f && velocityX < 0.05f)) 
        { 
            velocityX = 0.0f; 
        }

        if (forwardPressed && runPressed && velocityZ > currentMaxVelocity) 
        { 
            velocityZ = currentMaxVelocity; 
        }

        else if (forwardPressed && velocityZ > currentMaxVelocity) 
        { 
            velocityZ -= Time.deltaTime * deceleration; 
            if (velocityZ > currentMaxVelocity && velocityZ < (currentMaxVelocity + 0.05)) 
            { 
                velocityZ = currentMaxVelocity; 
            } 
        }

        else if (forwardPressed && velocityZ < currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.05)) 
        { 
            velocityZ = currentMaxVelocity;
        }

        if (backPressed && runPressed && velocityZ < -currentMaxVelocity) 
        { 
            velocityZ = -currentMaxVelocity;
        }

        else if (backPressed && velocityZ < -currentMaxVelocity) 
        { 
            velocityZ += Time.deltaTime * deceleration; 
            if (velocityZ < -currentMaxVelocity && velocityZ > (-currentMaxVelocity - 0.05)) 
            { 
                velocityZ = -currentMaxVelocity; 
            } 
        }

        else if (backPressed && velocityZ > -currentMaxVelocity && velocityZ < (-currentMaxVelocity + 0.05)) 
        { 
            velocityZ = -currentMaxVelocity; 
        }

        if (rightPressed && runPressed && velocityX > currentMaxVelocity) 
        { 
            velocityX = currentMaxVelocity; 
        }

        else if (rightPressed && velocityX > currentMaxVelocity) 
        { 
            velocityX -= Time.deltaTime * deceleration; 
            if (velocityX > currentMaxVelocity && velocityX < (currentMaxVelocity + 0.05)) 
            { 
                velocityX = currentMaxVelocity; 
            } 
        }

        else if (rightPressed && velocityX < currentMaxVelocity && velocityX > (currentMaxVelocity - 0.05)) 
        { 
            velocityX = currentMaxVelocity; 
        }

        if (leftPressed && runPressed && velocityX < -currentMaxVelocity) 
        { 
            velocityZ = -currentMaxVelocity; 
        }

        else if (leftPressed && velocityX < -currentMaxVelocity) 
        { 
            velocityX += Time.deltaTime * deceleration; 
            if (velocityX < -currentMaxVelocity && velocityX > (-currentMaxVelocity - 0.05)) 
            { 
                velocityX = -currentMaxVelocity; 
            } 
        }

        else if (leftPressed && velocityX > -currentMaxVelocity && velocityX < (-currentMaxVelocity + 0.05)) 
        {
            velocityX = -currentMaxVelocity; 
        }

    }
   
    void UpdatMouseLook()
    {
        Vector2 targetDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraPitch -= currentMouseDelta.y * mouseSensitivity; cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 40.0f);

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;

        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }
    void Update()
    {
        UpdatMouseLook();

        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool backPressed = Input.GetKey(KeyCode.S);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        float currentMaxVelocity = runPressed ? maximumRunVelocity : maximumWalkVelocity;

        ChangeVelocity(forwardPressed, leftPressed, rightPressed, backPressed, currentMaxVelocity);
        LockOrResetVelocity(forwardPressed, leftPressed, rightPressed, backPressed, runPressed, currentMaxVelocity);

        animator.SetFloat(VelocityZHash, velocityZ);
        animator.SetFloat(VelocityXHash, velocityX);

        // Check if the character is grounded.
        isGrounded = controller.isGrounded;

        // Apply gravity only when not grounded.
        if (!isGrounded)
        {
            Vector3 gravityVector = new Vector3(0f, -gravityForce, 0f);

            Vector3 velocity = (transform.forward * velocityZ + transform.right * velocityX + gravityVector);

            controller.Move(velocity * Time.deltaTime);
        }

        else
        {
            Vector3 velocity = (transform.forward * velocityZ + transform.right * velocityX);

            controller.Move(velocity * Time.deltaTime);
        }
    }
}