using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static float Gaussian(float stdDev)
    {
        float u, v, s;
        do
        {
            u = Random.Range(-1f, 1f);
            v = Random.Range(-1f, 1f);
            s = u * u + v * v;
        } while (s >= 1 || s == 0);

        float mul = Mathf.Sqrt(-2.0f * Mathf.Log(s) / s);
        return stdDev * u * mul;
    }
}
