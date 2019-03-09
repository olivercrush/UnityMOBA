using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Outdated

public class Text_HP : MonoBehaviour {

    public LifeComponent lifeComponent;
    public Camera playerCamera;

	public void InitializeText(LifeComponent lifeComponent)
    {
        this.lifeComponent = lifeComponent;
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

	private void LateUpdate ()
    {
	    if (!MissingInfo())
        {
            transform.position = lifeComponent.transform.position + new Vector3(0, 1.5f, 0);
            transform.LookAt(playerCamera.transform);
            transform.rotation = Quaternion.LookRotation(playerCamera.transform.forward);
        }

        //GetComponent<Text>().text = lifeComponent.lifePoints.ToString();
	}

    private bool MissingInfo() { return (lifeComponent == null || playerCamera == null); }
}
