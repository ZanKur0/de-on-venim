using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

    Credits: Acacia Developer
        - https://www.youtube.com/channel/UCXUiajdy-DV4D_aQ2wr-7Sg
        - https://github.com/Acacia-Developer/

    
 */

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform playerCamera = null;
    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] float walkSpeed = 6.0f;
    [SerializeField] float runSpeed = 10.0f;
    [SerializeField] float gravity = -13.0f;
    [SerializeField] float mass = 13.0f;
    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;

    [SerializeField] [Range(5f, 15f)] float jumpForce = 7.5f;

    [SerializeField] bool lockCursor = true;
    [SerializeField] KeyCode runKey = KeyCode.LeftShift;

    float cameraPitch = 0.0f;
    float velocityY = 0.0f;
    CharacterController controller = null;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        UpdateMouseLook();
        UpdateMovement();
    }

    void UpdateMouseLook()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraPitch -= currentMouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    void UpdateMovement()
    {
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        if (controller.isGrounded)
            velocityY = 0.0f;

        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
            velocityY += jumpForce;

        // If mass >= 0 means it weight less than the air, so it will start floating
        float force = (gravity * ((mass <= 0 ) ? 10 : mass)) * Time.deltaTime;
        velocityY += force;

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * ((Input.GetKey(runKey)) ? runSpeed : walkSpeed) + Vector3.up * velocityY;
  

        controller.Move(velocity * Time.deltaTime);

    }
}
