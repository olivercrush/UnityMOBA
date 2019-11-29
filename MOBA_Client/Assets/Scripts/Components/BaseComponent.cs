using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseComponent : MonoBehaviour {

    protected EntityController controller;

    protected virtual void Start () {
        controller = transform.parent.GetComponent<EntityController>();

        if (controller == null)
            Debug.LogError(this.ToString() + " must be an entity child.");
    }

}
