
using UnityEngine;

public class SpacecraftController : MonoBehaviour
{
    public GameObject explosion;
    // public vars
    public float mouseSensitivityX = 1;
    public float mouseSensitivityY = 1;
    public float walkSpeed = 6;
    public float jumpForce = 220;
    public LayerMask groundedMask;

    // System vars

    float verticalLookRotation;
    float horizontalLookRotation;
    public Transform cameraTransform;
    Rigidbody body;

    Controls controls;
    bool thrust;
    float pitch;
    float yaw;
    float roll;

    public float force = 1f;
    public float torque = 1f;

    public ParticleSystem[] vfx;
    public Transform camTarget;


    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        body = GetComponent<Rigidbody>();

        controls = new Controls();

        controls.Spacecraft.Thrust.performed += context => StartThrust();
        controls.Spacecraft.Thrust.canceled += context => StopThrust();

        controls.Spacecraft.Pitch.performed += context => pitch = context.ReadValue<float>();
        controls.Spacecraft.Pitch.canceled += context => pitch = 0f;

        controls.Spacecraft.Yaw.performed += context => yaw = context.ReadValue<float>();
        controls.Spacecraft.Yaw.canceled += context => yaw = 0f;

        controls.Spacecraft.Roll.performed += context => roll = context.ReadValue<float>();
        controls.Spacecraft.Roll.canceled += context => roll = 0f;


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.impulse.magnitude > 10f)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            controls.Spacecraft.Disable();
            //this.enabled = false;
            //cameraTransform.SetParent(null);
        }
        
    }

    private void OnEnable()
    {
        controls.Spacecraft.Enable();
    }

    private void OnDisable()
    {
        controls.Spacecraft.Disable();
    }

    void StartThrust()
    {
        thrust = true;
        foreach (ParticleSystem fx in vfx)
        {
            fx.Play();
        }
    }

    void StopThrust()
    {
        thrust = false;
        foreach (ParticleSystem fx in vfx)
        {
            fx.Stop();
        }
    }

    void Update()
    {

        // Look rotation:
        //transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX);
        horizontalLookRotation += Input.GetAxis("Mouse X") * mouseSensitivityX;
        verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90, 90);
        cameraTransform.position = camTarget.position;
        cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation + Vector3.up * horizontalLookRotation;

 





    }

    void FixedUpdate()
    {
        // Apply movement to rigidbody
        //rigidbody.MovePosition(rigidbody.position + localMove);

        if (thrust)
        {
            body.AddRelativeForce(Vector3.up * force, ForceMode.Impulse);

        }

        body.AddRelativeTorque(Vector3.right * torque * pitch, ForceMode.Impulse);
        body.AddRelativeTorque(Vector3.forward * torque * yaw, ForceMode.Impulse);
        body.AddRelativeTorque(Vector3.up * torque * roll, ForceMode.Impulse);

    }
}
