using UnityEngine;
using System.Collections;

public class CameraMouvement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 cursorPos = this.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
            //transform.Translate(cursorPos - this.GetComponent<Camera>());
            //transform.LookAt(cursorPos);
        }
    }
}
