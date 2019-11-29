using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrackNotificationCenter : NotificationCenter
{
    private BarrackController barrackController;

    public BarrackNotificationCenter(EntityController controller) : base(controller)
    {
        this.barrackController = (BarrackController)controller;
    }
    
    public override void Notify(Notification notification)
    {
        switch (notification.GetNotificationType())
        {
            case NotificationType.HIT_TAKEN:
                barrackController.TakeHit((int)notification.GetData());
                break;

            case NotificationType.SUBSCRIBE_DEATH_OBSERVER:
                barrackController.SubscribeDeathNotifier(notification.GetSender());
                break;

            case NotificationType.UNSUBSCRIBE_DEATH_OBSERVER:
                barrackController.UnsubscribeDeathNotifier(notification.GetSender());
                break;

            case NotificationType.DIED:
                barrackController.DestroyBarrack();
                break;
        }
    }
}
