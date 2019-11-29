using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntityColor { BLUE, RED, WHITE }

public class EntityController : MonoBehaviour {

    [Header("Entity settings")]
    public EntityColor color;
    public GameObject canvas;
    public bool debug;

    protected NotificationCenter notificationCenter;

    public NotificationCenter GetNotificationCenter()
    {
        return notificationCenter;
    }
}
