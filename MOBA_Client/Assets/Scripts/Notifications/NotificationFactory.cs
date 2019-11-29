using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class NotificationFactory {

    private static NotificationFactory instance = null;
    private static readonly object padlock = new object();

    public static NotificationFactory Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new NotificationFactory();
                }
            }
            return instance;
        }
    }

    private NotificationFactory() { }

    public void CreateNotification(GameObject sender, GameObject receiver, NotificationType type, NotificationPriority priority, object data)
    {
        Notification notification = new Notification(sender, receiver, type, priority, data);
        notification.Send();
    }

    // ENEMY_DETECTED
    public void CreateNotificationEnemyDetected(GameObject sender, GameObject receiver, GameObject target)
    {
        Notification notification = new Notification(sender, receiver, NotificationType.ENEMY_DETECTED, NotificationPriority.NORMAL, target);
        notification.Send();
    }

    // TARGET_IN_RANGE
    public void CreateNotificationTargetInRange(GameObject sender, GameObject receiver)
    {
        Notification notification = new Notification(sender, receiver, NotificationType.TARGET_IN_RANGE, NotificationPriority.NORMAL, null);
        notification.Send();
    }

    // TARGET_OUT_OF_RANGE
    public void CreateNotificationTargetOutOfRange(GameObject sender, GameObject receiver)
    {
        Notification notification = new Notification(sender, receiver, NotificationType.TARGET_OUT_OF_RANGE, NotificationPriority.NORMAL, null);
        notification.Send();
    }

    // TARGET_DIED
    public void CreateNotificationTargetDied(GameObject sender, GameObject receiver)
    {
        Notification notification = new Notification(sender, receiver, NotificationType.TARGET_DIED, NotificationPriority.NORMAL, null);
        notification.Send();
    }

    // NO_TARGET
    public void CreateNotificationNoTarget(GameObject sender, GameObject receiver)
    {
        Notification notification = new Notification(sender, receiver, NotificationType.NO_TARGET, NotificationPriority.NORMAL, null);
        notification.Send();
    }

    // END_OF_WAYPOINTS
    public void CreateNotificationEndOfWaypoints(GameObject sender, GameObject receiver)
    {
        Notification notification = new Notification(sender, receiver, NotificationType.END_OF_WAYPOINTS, NotificationPriority.NORMAL, null);
        notification.Send();
    }

    // HIT_TAKEN
    public void CreateNotificationHitTaken(GameObject sender, GameObject receiver, int damage)
    {
        Notification notification = new Notification(sender, receiver, NotificationType.HIT_TAKEN, NotificationPriority.NORMAL, damage);
        notification.Send();
    }

    // DIED
    public void CreateNotificationDied(GameObject entity)
    {
        Notification notification = new Notification(entity, entity, NotificationType.DIED, NotificationPriority.NORMAL, null);
        notification.Send();
    }

    // SUBSCRIBE_DEATH_OBSERVER
    public void CreateNotificationSubscribeDeathObserver(GameObject sender, GameObject receiver)
    {
        Notification notification = new Notification(sender, receiver, NotificationType.SUBSCRIBE_DEATH_OBSERVER, NotificationPriority.NORMAL, null);
        notification.Send();
    }

    // UNSUBSCRIBE_DEATH_OBSERVER
    public void CreateNotificationUnsubscribeDeathObserver(GameObject sender, GameObject receiver)
    {
        Notification notification = new Notification(sender, receiver, NotificationType.UNSUBSCRIBE_DEATH_OBSERVER, NotificationPriority.NORMAL, null);
        notification.Send();
    }
}
