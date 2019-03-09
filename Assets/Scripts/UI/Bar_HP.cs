using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO : Make UIComponent_HP
public class Bar_HP : MonoBehaviour {

    public float updateSpeed;
    public LifeComponent lifeComponent;
    public GameObject lifeBar;

    private bool showed = false;

    public void InitializeHPBar(LifeComponent lifeComponent)
    {
        this.lifeComponent = lifeComponent;
        gameObject.SetActive(showed);
    }

    public void UpdateHPBar(int baseLifePoints, int lifePoints)
    {
        if (!showed)
        {
            showed = true;
            gameObject.SetActive(showed);
        }

        float pct = (float)lifePoints / (float)baseLifePoints;
        StartCoroutine(LerpHPBar(pct));
        //lifeBar.GetComponent<Image>().fillAmount = pct;
    }

    private IEnumerator LerpHPBar(float pct)
    {
        float preChangePct = lifeBar.GetComponent<Image>().fillAmount;
        float timeElapsed = 0f;

        while (timeElapsed < updateSpeed)
        {
            timeElapsed += Time.deltaTime;
            lifeBar.GetComponent<Image>().fillAmount = Mathf.Lerp(preChangePct, pct, timeElapsed / updateSpeed);
            yield return null;
        }

        lifeBar.GetComponent<Image>().fillAmount = pct;
    }
}
