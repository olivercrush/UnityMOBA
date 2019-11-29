using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NotificationCenter {

    protected EntityController controller;

    public NotificationCenter(EntityController controller)
    {
        this.controller = controller;
    }

    public abstract void Notify(Notification notification);
}
