using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float panSpeed = 20f;
    public float panBorderThickness = 15f;
	
	// Update is called once per frame
	void Update () {

        Vector3 pos = transform.position;

        if (Input.GetKey("w") /*|| Input.mousePosition.y >= Screen.height - panBorderThickness*/)
        {
            pos.z += (panSpeed * Mathf.Cos(Mathf.Deg2Rad * transform.eulerAngles.y)) * Time.deltaTime;
            pos.x += (panSpeed * Mathf.Sin(Mathf.Deg2Rad * transform.eulerAngles.y)) * Time.deltaTime;
        }
        else if (Input.GetKey("s") /*|| Input.mousePosition.y <= panBorderThickness*/)
        {
            pos.z -= (panSpeed * Mathf.Cos(Mathf.Deg2Rad * transform.eulerAngles.y)) * Time.deltaTime;
            pos.x -= (panSpeed * Mathf.Sin(Mathf.Deg2Rad * transform.eulerAngles.y)) * Time.deltaTime;
        }
            
        if (Input.GetKey("a") /*|| Input.mousePosition.x <= panBorderThickness*/)
        {
            pos.z += (panSpeed * Mathf.Cos(Mathf.Deg2Rad * (90 - transform.eulerAngles.y))) * Time.deltaTime;
            pos.x -= (panSpeed * Mathf.Sin(Mathf.Deg2Rad * (90 - transform.eulerAngles.y))) * Time.deltaTime;
        }
        else if (Input.GetKey("d") /*|| Input.mousePosition.x >= Screen.width - panBorderThickness*/)
        {
            pos.z -= (panSpeed * Mathf.Cos(Mathf.Deg2Rad * (90 - transform.eulerAngles.y))) * Time.deltaTime;
            pos.x += (panSpeed * Mathf.Sin(Mathf.Deg2Rad * (90 - transform.eulerAngles.y))) * Time.deltaTime;
        }

        transform.position = pos;
	}
}
