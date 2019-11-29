using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class AttackComponent : BaseComponent
{

    // PUBLIC
    public int attackPoints;
    public float attackRate;

    // ATTACK STATE
    private GameObject target = null;
    public float attackRange;

    protected override void Start()
    {
        base.Start();
        attackRange = GetComponent<SphereCollider>().radius * controller.transform.localScale.x;
    }

    public void Attack()
    {
        InvokeRepeating("AttackTarget", 0.1f, attackRate);
    }

    public void Stop()
    {
        CancelInvoke("AttackTarget");
    }

    public void DefineTarget(GameObject target)
    {
        this.target = target;
        NotificationFactory.Instance.CreateNotificationSubscribeDeathObserver(controller.gameObject, this.target);

        VerifyTargetInRange();
    }

    public void UndefineTarget()
    {
        if (target != null)
        {
            NotificationFactory.Instance.CreateNotificationUnsubscribeDeathObserver(controller.gameObject, target);
            target = null;
        }
    }

    private void AttackTarget()
    {
        // TODO : Create Attack object to send better infos
        NotificationFactory.Instance.CreateNotificationHitTaken(controller.gameObject, target, attackPoints);
    }

    private void VerifyTargetInRange()
    {
        if (target != null && Vector3.Distance(target.transform.position, transform.position) < attackRange + 0.2)
        {
            //if (controller.debug)
                //Debug.Log("Distance " + Vector3.Distance(target.transform.position, transform.position) + " vs attack range " + GetComponent<SphereCollider>().radius);

            NotificationFactory.Instance.CreateNotificationTargetInRange(controller.gameObject, controller.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target)
        {
            NotificationFactory.Instance.CreateNotificationTargetInRange(controller.gameObject, controller.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == target)
        {
            NotificationFactory.Instance.CreateNotificationTargetOutOfRange(controller.gameObject, controller.gameObject);
        }
    }

    private void Update()
    {
        // TODO : check every x seconds ?
        VerifyTargetInRange();
    }

}
