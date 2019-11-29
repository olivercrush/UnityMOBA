using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerNotificationCenter : NotificationCenter
{
    private TowerController towerController;

    public TowerNotificationCenter(EntityController controller) : base(controller)
    {
        this.towerController = (TowerController)controller;
    }

    public override void Notify(Notification notification)
    {
        if (HasPriority(notification.GetNotificationType()))
        {
            switch (notification.GetNotificationType())
            {
                case NotificationType.HIT_TAKEN:
                    towerController.TakeHit((int)notification.GetData());
                    break;

                case NotificationType.SUBSCRIBE_DEATH_OBSERVER:
                    towerController.SubscribeDeathNotifier(notification.GetSender());
                    break;

                case NotificationType.UNSUBSCRIBE_DEATH_OBSERVER:
                    towerController.UnsubscribeDeathNotifier(notification.GetSender());
                    break;

                case NotificationType.DIED:
                    towerController.DestroyTower();
                    break;

                case NotificationType.ENEMY_DETECTED:
                    towerController.ChangeState(TowerState.TARGETING, notification.GetData());
                    break;

                case NotificationType.TARGET_IN_RANGE:
                    towerController.ChangeState(TowerState.ATTACKING, notification.GetData());
                    break;

                case NotificationType.TARGET_OUT_OF_RANGE:
                    towerController.VerifyEnemyPresence();
                    break;

                case NotificationType.TARGET_DIED:
                    towerController.ChangeState(TowerState.IDLE, null);
                    towerController.VerifyEnemyPresence();
                    break;

                case NotificationType.NO_TARGET:
                    towerController.ChangeState(TowerState.IDLE, notification.GetData());
                    break;
            }
        }
    }

    private bool HasPriority(NotificationType type)
    {
        bool hasPriority = false;

        switch (towerController.state)
        {
            case TowerState.IDLE:
                if (type == NotificationType.ENEMY_DETECTED)
                    hasPriority = true;
                break;

            case TowerState.TARGETING:
                if (type == NotificationType.TARGET_DIED ||
                    type == NotificationType.TARGET_IN_RANGE ||
                    type == NotificationType.TARGET_OUT_OF_RANGE ||
                    type == NotificationType.NO_TARGET)
                    hasPriority = true;
                break;

            case TowerState.ATTACKING:
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
