using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NotificationType
{
    // MAIN STATES
    ENEMY_DETECTED,
    TARGET_IN_RANGE,
    TARGET_OUT_OF_RANGE,
    TARGET_DIED,
    NO_TARGET,
    END_OF_WAYPOINTS,

    // SUBSTATES
    STUNNED,
    DIED,

    // OTHERS
    HIT_TAKEN,
    SUBSCRIBE_DEATH_OBSERVER,
    UNSUBSCRIBE_DEATH_OBSERVER
}
public enum NotificationPriority { LOW, NORMAL, HIGH }

public class Notification {

    private GameObject sender;
    private GameObject receiver;

    private NotificationType type;
    private NotificationPriority priority;
    private object data;

    public Notification(GameObject sender, GameObject receiver, NotificationType type, NotificationPriority priority, object data)
    {
        this.sender = sender;
        this.receiver = receiver;
        this.type = type;
        this.priority = priority;
        this.data = data;
    }

    public NotificationPriority GetPriority()
    {
        return priority;
    }

    public NotificationType GetNotificationType()
    {
        return type;
    }

    public object GetData()
    {
        return data;
    }

    public GameObject GetSender()
    {
        return sender;
    }

    public GameObject GetReceiver()
    {
        return receiver;
    }

    public void Send()
    {
        if (GetReceiver() != null)
        {
            if (type == NotificationType.HIT_TAKEN && sender.GetComponent<EntityController>().debug)
                Debug.Log(type);

            receiver.GetComponent<EntityController>().GetNotificationCenter().Notify(this);
        }
    }
}
