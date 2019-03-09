using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerState { IDLE, ATTACKING, TARGETING, DESTROYED }

public class TowerController : EntityController {

    // PUBLIC VARIABLES
    [Header("General settings")]
    public TowerState state;
    public Material red;
    public Material blue;

    // COMPONENTS
    [Header("Components")]
    public LifeComponent lifeComponent;
    public DetectionComponent detectionComponent;
    public AttackComponent attackComponent;
    public DeathNotifierComponent deathNotifierComponent;

    void Start()
    {
        if (this.color == EntityColor.RED)
        {
            GetComponent<Renderer>().material = red;
        }
        else
        {
            GetComponent<Renderer>().material = blue;
        }

        notificationCenter = new TowerNotificationCenter(this);
    }

    // METHODS
    public void VerifyEnemyPresence()
    {
        detectionComponent.VerifyEnemyPresence();
    }

    public void TakeHit(int damage)
    {
        // TODO : Use Attack object
        lifeComponent.TakeHit(damage);
    }

    public void DestroyTower()
    {
        state = TowerState.DESTROYED;
        deathNotifierComponent.NotifyDeath();

        Destroy(this.gameObject);
    }

    public void SubscribeDeathNotifier(GameObject observer)
    {
        deathNotifierComponent.AddObserver(observer);
    }

    public void UnsubscribeDeathNotifier(GameObject observer)
    {
        deathNotifierComponent.RemoveObserver(observer);
    }

    // CHANGE STATE

    public void ChangeState(TowerState state, object data)
    {
        // TODO : ajouter un timer dans l'attackComponent
        attackComponent.Stop();

        switch (state)
        {
            case TowerState.ATTACKING:
                attackComponent.Attack();
                break;

            case TowerState.TARGETING:
                attackComponent.Stop();
                attackComponent.DefineTarget((GameObject)data);
                break;


            case TowerState.IDLE:
                attackComponent.Stop();
                attackComponent.UndefineTarget();
                break;

            case TowerState.DESTROYED:
                attackComponent.Stop();
                attackComponent.UndefineTarget();
                break;
        }

        this.state = state;
    }
}
