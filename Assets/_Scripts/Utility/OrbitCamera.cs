using UnityEngine;
using System.Collections;

public class OrbitCamera : MonoBehaviour
{
    /* These variables are what tell the camera how its going to function by
     * setting the viewing target, collision layers, and other properties
     * such as distance and viewing angles */
    public Transform viewTarget;
    public LayerMask collisionLayers;
    public float distance = 6.0f;
    public float distanceSpeed = 150.0f;
    public float collisionOffset = 0.3f;
    public float minDistance = 4.0f;
    public float maxDistance = 12.0f;
    public float height = 1.5f;
    public float sensitivity = 150.0f;
    public float rotationDampening = 0.75f;
    public float minVerticalAngle = -60.0f;
    public float maxVerticalAngle = 60.0f;
    public bool useRMBToAim = false;
    public bool pause;

    /* These variables are meant to store values given by the script and
     * not the user */
    private float h, v, newDistance, smoothDistance;
    private Vector3 newPosition;
    private Quaternion newRotation, smoothRotation;
    private Transform cameraTransform;

    /* This is where we initialize our script */
    void Start()
    {
        Initialize();
    }

    /* This is where we set our private variables, check for null errors,
     * and anything else that needs to be called once during startup */
    void Initialize()
    {
        h = this.transform.eulerAngles.x;
        v = this.transform.eulerAngles.y;

        cameraTransform = this.transform;
        smoothDistance = distance;

        NullErrorCheck();
    }

    /* We check for null errors or warnings and notify the user to fix them */
    void NullErrorCheck()
    {
        if (!viewTarget)
        {
            Debug.LogError("Please make sure to assign a view target!");
            Debug.Break();
        }
        if (collisionLayers == 0)
        {
            Debug.LogWarning("Make sure to set the collision layers to the layers the camera should collide with!");
        }
    }

    /* This is where we do all our camera updates. This is where the camera
     * gets all of its functionality. From setting the position and rotation,
     * to adjusting the camera to avoid geometry clipping */
    void LateUpdate()
    {
        
        if (!viewTarget)
            return;
        if (pause == false)
        {
            /* We check for right mouse button functionality, set the rotation
             * angles, and lock the mouse cursor */
            if (!useRMBToAim)
            {
                h += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
                v -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

                h = ClampAngle(h, -360.0f, 360.0f);
                v = ClampAngle(v, minVerticalAngle, maxVerticalAngle);

                newRotation = Quaternion.Euler(v, h, 0.0f);
            }
            else
            {
                if (Input.GetMouseButton(1))
                {
                    h += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
                    v -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

                    h = ClampAngle(h, -360.0f, 360.0f);
                    v = ClampAngle(v, minVerticalAngle, maxVerticalAngle);

                    newRotation = Quaternion.Euler(v, h, 0.0f);
                }
            }
        }

            /* We set the distance by moving the mouse wheel and use a custom
             * growth function as the time value for linear interpolation */
            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 10, minDistance, maxDistance);
            smoothDistance = Mathf.Lerp(smoothDistance, distance, TimeSignature(distanceSpeed));

            /*We give the rotation some smoothing for a nicer effect */
            smoothRotation = Quaternion.Slerp(smoothRotation, newRotation, TimeSignature((1 / rotationDampening) * 100.0f));

            newPosition = viewTarget.position;
            newPosition += smoothRotation * new Vector3(0.0f, height, -smoothDistance);

            /* Calls the function to adjust the camera position to avoid clipping */
            CheckSphere();

            smoothRotation.eulerAngles = new Vector3(smoothRotation.eulerAngles.x, smoothRotation.eulerAngles.y, 0.0f);

            cameraTransform.position = newPosition;
            cameraTransform.rotation = smoothRotation;
    }

    /* This is where the camera checks for a collsion hit within a specified radius,
     * and then moves the camera above the location it hit with an offset value */
    void CheckSphere()
    {
        /* Add height to our spherecast origin */
        Vector3 tmpVect = viewTarget.position;
        tmpVect.y += height;

        RaycastHit hit;

        /* Get the direction from the camera position to the origin */
        Vector3 dir = (newPosition - tmpVect).normalized;

        /* Check a radius for collision hits and then set the new position for
         * the camera */
        if (Physics.SphereCast(tmpVect, 0.3f, dir, out hit, distance, collisionLayers))
        {
            newPosition = hit.point + (hit.normal * collisionOffset);
        }
    }

    /* Keeps the angles values within their specificed minimum and maximum
     * inputs while at the same time putting the values back to 0 if they 
     * go outside of the 360 degree range */
    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;

        if (angle > 360)
            angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }

    /* This is our custom logistic growth time signature with speed as input */
    private float TimeSignature(float speed)
    {
        return 1.0f / (1.0f + 80.0f * Mathf.Exp(-speed * 0.02f));
    }
}