using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum UnitState { IDLE, LANING, TARGETING, ATTACKING, DEAD }

public class UnitController : EntityController {

    // PUBLIC VARIABLES
    [Header("General settings")]
    public UnitState state;
    public Material red;
    public Material blue;

    // COMPONENTS
    [Header("Components")]
    public MovementComponent movementComponent;
    public DetectionComponent detectionComponent;
    public AttackComponent attackComponent;
    public LifeComponent lifeComponent;
    public DeathNotifierComponent deathNotifierComponent;

    private void Start()
    {
        notificationCenter = new UnitNotificationCenter(this);

        if (this.color == EntityColor.RED)
        {
            GetComponent<Renderer>().material = red;
        }
        else
        {
            GetComponent<Renderer>().material = blue;
        }

        if (state == UnitState.IDLE)
        {
            InitializeUnitIdle();
        }
    }

    // INITIALIZATIONS

    public void InitializeUnitIdle()
    {
        movementComponent.InitializeMovementComponent(GetComponent<NavMeshAgent>(), transform, null);
        ChangeState(UnitState.IDLE, null);
    }

    public void InitializeUnit(EntityColor color, Transform transform, GameObject[] waypoints)
    {
        this.color = color;
        movementComponent.InitializeMovementComponent(GetComponent<NavMeshAgent>(), transform, waypoints);
        ChangeState(UnitState.LANING, null);
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

    public void Die()
    {
        // TODO : Use the DeathNotifierComponent, particles and destruction
        // TODO : UnitState.DEAD
        ChangeState(UnitState.DEAD, null);
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

    public void ChangeState(UnitState state, object data)
    {
        // TODO : ajouter un timer dans l'attackComponent
        attackComponent.Stop();

        switch (state)
        {
            case UnitState.ATTACKING:
                movementComponent.StopMovement();
                attackComponent.Attack();
                break;

            case UnitState.TARGETING:
                attackComponent.Stop();
                attackComponent.DefineTarget((GameObject)data);
                movementComponent.GoToTarget((GameObject) data);
                break;

            case UnitState.LANING:
                movementComponent.GoToWaypoint();
                attackComponent.Stop();
                attackComponent.UndefineTarget();
                break;

            case UnitState.IDLE:
                movementComponent.StopMovement();
                attackComponent.Stop();
                attackComponent.UndefineTarget();
                break;

            case UnitState.DEAD:
                movementComponent.StopMovement();
                attackComponent.Stop();
                attackComponent.UndefineTarget();
                break;
        }

        this.state = state;
    }
}
