using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityCanvas : MonoBehaviour {

    private Camera playerCamera;

    void Start ()
    {
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        transform.LookAt(playerCamera.transform);
        transform.rotation = Quaternion.LookRotation(playerCamera.transform.forward);
    }
}
