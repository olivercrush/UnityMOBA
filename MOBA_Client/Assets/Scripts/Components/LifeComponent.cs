using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeComponent : BaseComponent
{

    // CHARATERISTICS
    [Header("Life settings")]
    public int baseLifePoints;
    public int baseArmor;

    protected int lifePoints;
    protected int armor;

    [Header("UI settings")]
    public GameObject healthbar_prefab;
    private GameObject healthbar;

    protected override void Start()
    {
        base.Start();

        lifePoints = baseLifePoints;
        armor = baseArmor;

        healthbar = Instantiate(healthbar_prefab, controller.canvas.transform);
        healthbar.GetComponent<Bar_HP>().InitializeHPBar(this);
    }

    public void TakeHit(int damage)
    {
        lifePoints -= damage;
        healthbar.GetComponent<Bar_HP>().UpdateHPBar(baseLifePoints, lifePoints);

        if (IsDead())
        {
            NotificationFactory.Instance.CreateNotificationDied(controller.gameObject);
        }
    }

    public bool IsDead()
    {
        return lifePoints <= 0;
    }
}
