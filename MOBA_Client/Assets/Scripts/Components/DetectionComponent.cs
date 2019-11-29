using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class DetectionComponent : BaseComponent
{

    private List<GameObject> entitiesInRange;

    protected override void Start()
    {
        base.Start();
        entitiesInRange = new List<GameObject>();
    }

    public void VerifyEnemyPresence()
    {
        if (EnemiesInRange())
        {
            if (RandomMethods.PercentageChance(25f))
                NotificationFactory.Instance.CreateNotificationEnemyDetected(controller.gameObject, controller.gameObject, GetSecondNearestEnemy());
            else
                NotificationFactory.Instance.CreateNotificationEnemyDetected(controller.gameObject, controller.gameObject, GetNearestEnemy());
        }
        else
        {
            NotificationFactory.Instance.CreateNotificationNoTarget(controller.gameObject, controller.gameObject);
        }
    }

    private void CheckEnemiesArray()
    {
        List<GameObject> artefacts = new List<GameObject>();

        for (int i = 0; i < entitiesInRange.Count; i++)
        {
            bool isDead = false;
            if (entitiesInRange[i] == null)
            {
                isDead = true;
            }
            else if (entitiesInRange[i].GetComponent<EntityController>() is UnitController && entitiesInRange[i].GetComponent<UnitController>().state == UnitState.DEAD)
            {
                isDead = true;
            }
            else if (entitiesInRange[i].GetComponent<EntityController>() is BarrackController && entitiesInRange[i].GetComponent<BarrackController>().state == BarrackState.DESTROYED)
            {
                isDead = true;
            }
            else if (entitiesInRange[i].GetComponent<EntityController>() is TowerController && entitiesInRange[i].GetComponent<TowerController>().state == TowerState.DESTROYED)
            {
                isDead = true;
            }

            if (isDead)
            {
                artefacts.Add(entitiesInRange[i]);
            }
        }

        for (int i = 0; i < artefacts.Count; i++)
        {
            entitiesInRange.Remove(artefacts[i]);
        }
    }

    private GameObject GetNearestEnemy()
    {
        float minDistance = -1;
        int nearestTargetId = -1;

        for (int i = 0; i < entitiesInRange.Count; i++)
        {
            if (Vector3.Distance(transform.position, entitiesInRange[i].transform.position) < minDistance || minDistance < 0)
            {
                minDistance = Vector3.Distance(transform.position, entitiesInRange[i].transform.position);
                nearestTargetId = i;
            }
        }

        return entitiesInRange[nearestTargetId];
    }

    private GameObject GetSecondNearestEnemy()
    {
        float minDistance = -1;
        int nearestTargetId = -1;
        int secondNearestTargetId = -1;

        for (int i = 0; i < entitiesInRange.Count; i++)
        {
            if (Vector3.Distance(transform.position, entitiesInRange[i].transform.position) < minDistance || minDistance < 0)
            {
                minDistance = Vector3.Distance(transform.position, entitiesInRange[i].transform.position);
                secondNearestTargetId = nearestTargetId;
                nearestTargetId = i;
            }
        }

        if (secondNearestTargetId < 0)
        {
            secondNearestTargetId = 0;
        }

        return entitiesInRange[secondNearestTargetId];
    }

    private bool EnemiesInRange()
    {
        CheckEnemiesArray();

        if (entitiesInRange.Count > 0)
            return true;
        else
            return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Unit" || other.gameObject.tag == "Barrack" || other.gameObject.tag == "Tower") && controller != null)
        {
            GameObject enemy = other.gameObject;
            EntityController enemyController = enemy.GetComponent<EntityController>();

            if (enemyController.color != controller.color)
            {
                if (enemyController is UnitController)
                {
                    UnitController enemyUnitController = (UnitController)enemyController;

                    if (enemyUnitController.state != UnitState.DEAD)
                    {
                        entitiesInRange.Add(enemy);
                        VerifyEnemyPresence();
                    }
                }
                else if (enemyController is BarrackController)
                {
                    BarrackController enemyBarrackController = (BarrackController)enemyController;

                    if (enemyBarrackController.state != BarrackState.DESTROYED)
                    {
                        entitiesInRange.Add(enemy);
                        VerifyEnemyPresence();
                    }
                }
                else if (enemyController is TowerController)
                {
                    TowerController enemyTowerController = (TowerController)enemyController;

                    if (enemyTowerController.state != TowerState.DESTROYED)
                    {
                        entitiesInRange.Add(enemy);
                        VerifyEnemyPresence();
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (entitiesInRange.Contains(other.gameObject))
        {
            entitiesInRange.Remove(other.gameObject);
            VerifyEnemyPresence();
        }
    }
}
