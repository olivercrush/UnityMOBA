using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitNotificationCenter : NotificationCenter {

    private UnitController unitController;

    public UnitNotificationCenter(EntityController controller) : base(controller)
    {
        this.unitController = (UnitController) controller;
    }

    public override void Notify(Notification notification)
    {
        if (HasPriority(notification.GetNotificationType()))
        {
            switch (notification.GetNotificationType())
            {
                case NotificationType.ENEMY_DETECTED:
                    unitController.ChangeState(UnitState.TARGETING, notification.GetData());
                    break;

                case NotificationType.TARGET_IN_RANGE:
                    unitController.ChangeState(UnitState.ATTACKING, notification.GetData());
                    break;

                case NotificationType.TARGET_OUT_OF_RANGE:
                    unitController.VerifyEnemyPresence();
                    break;

                case NotificationType.TARGET_DIED:
                    unitController.ChangeState(UnitState.LANING, null);
                    unitController.VerifyEnemyPresence();
                    break;

                case NotificationType.NO_TARGET:
                    unitController.ChangeState(UnitState.LANING, notification.GetData());
                    break;

                case NotificationType.END_OF_WAYPOINTS:
                    unitController.ChangeState(UnitState.IDLE, notification.GetData());
                    break;

                case NotificationType.HIT_TAKEN:
                    unitController.TakeHit((int) notification.GetData());
                    break;

                case NotificationType.SUBSCRIBE_DEATH_OBSERVER:
                    unitController.SubscribeDeathNotifier(notification.GetSender());
                    break;

                case NotificationType.UNSUBSCRIBE_DEATH_OBSERVER:
                    unitController.UnsubscribeDeathNotifier(notification.GetSender());
                    break;

                case NotificationType.DIED:
                    unitController.Die();
                    break;
            }
        }
    }

    private bool HasPriority(NotificationType type)
    {
        bool hasPriority = false;

        switch (unitController.state)
        {
            case UnitState.LANING:
                if (type == NotificationType.ENEMY_DETECTED ||
                    type == NotificationType.END_OF_WAYPOINTS)
                    hasPriority = true;
                break;

            case UnitState.IDLE:
                if (type == NotificationType.ENEMY_DETECTED)
                    hasPriority = true;
                break;

            case UnitState.TARGETING:
                if (type == NotificationType.TARGET_DIED ||
                    type == NotificationType.TARGET_IN_RANGE ||
                    type == NotificationType.TARGET_OUT_OF_RANGE ||
                    type == NotificationType.NO_TARGET)
                    hasPriority = true;
                break;

            case UnitState.ATTACKING:
                if (type == NotificationType.TARGET_DIED || 
                    type == NotificationType.NO_TARGET ||
                    type == NotificationType.TARGET_OUT_OF_RANGE ||
                    type == NotificationType.ENEMY_DETECTED)
                    hasPriority = true;
                break;
        }

        if (type == NotificationType.HIT_TAKEN ||
            type == NotificationType.DIED ||
            type == NotificationType.SUBSCRIBE_DEATH_OBSERVER ||
            type == NotificationType.UNSUBSCRIBE_DEATH_OBSERVER)
        {
            hasPriority = true;
        }

        return hasPriority;
    }
}
