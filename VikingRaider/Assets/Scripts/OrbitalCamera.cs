using UnityEngine;
using System.Collections;
    
public class OrbitalCamera : MonoBehaviour {
    //initialisation des variables
    public float rotationSpeed = 60.0F;
    public float zoomSpeed = 0.5F;
    public float angleMax = 80.0f;
    public GameObject Map;

    float xcam;
    float ycam;
    float zcam;

    float MIN_X, MIN_Y, MIN_Z;
    float MAX_X, MAX_Y, MAX_Z;

    Quaternion rotcam;

    void Start () {
        xcam = this.transform.position.x;
        ycam = this.transform.position.y;
        zcam = this.transform.position.z;
        rotcam = this.transform.rotation;

        MIN_Z = -20;
        MAX_Z = -5;

        MIN_X = -20;
        MIN_Y = -10;

        MAX_X = 20;
        MAX_Y = 10;
    }
	
	void Update () {



        //rotation de la caméra autour de ses axes
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        float b_zoom = Input.GetAxis("Zoom");

        Debug.Log(horiz);

        float rotation_y = horiz * rotationSpeed;
        rotation_y *= 0.016f;
        transform.Rotate(0, rotation_y, 0);

        float rotation_x = vert * rotationSpeed;
        rotation_x *= -0.016f;
        transform.Rotate(rotation_x, 0, 0);

        float zoom = b_zoom * zoomSpeed;
        zoom *= 0.016f;
        transform.Translate(0, 0, zoom, Space.Self);

        transform.position = new Vector3(
        Mathf.Clamp(transform.position.x, MIN_X, MAX_X),
        Mathf.Clamp(transform.position.y, MIN_Y, MAX_Y),
        Mathf.Clamp(transform.position.z, MIN_Z, MAX_Z));

        if (Input.GetKeyDown("space"))
        {
            this.transform.position = new Vector3(xcam, ycam, zcam);
            this.transform.rotation = rotcam;
        }

    }
}