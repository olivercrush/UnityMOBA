using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathNotifierComponent : BaseComponent {

    private List<GameObject> observers;
    private bool locked;

    protected override void Start()
    {
        base.Start();
        observers = new List<GameObject>();
        locked = false;
    }

    public void AddObserver(GameObject observer)
    {
        if (!locked)
        {
            observers.Add(observer);
        }
    }

    public void RemoveObserver(GameObject observer)
    {
        if (!locked && observers.Contains(observer))
        {
            observers.Remove(observer);
        }
    }

    public void NotifyDeath()
    {
        locked = true;

        while (observers.Count > 0)
        {
            NotificationFactory.Instance.CreateNotificationTargetDied(controller.gameObject, observers[0]);
            observers.RemoveAt(0);
        }
    }
}
