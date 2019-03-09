using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomMethods {

    public static bool PercentageChance(float percentage)
    {
        if (Random.Range(0f, 100f) < percentage)
            return true;

        return false;
    }

}
