using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Symptom : MonoBehaviour
{
    public string s_name;
    public float level = 100;
    private bool isTreated = false;

    public void Treat(float damage)
    {
        level -= damage;

        if (level <= 0)
        {
            isTreated = true;
        }
    }

    public bool IsTreated()
    {
        return isTreated;
    }

    public float GetLevel()
    {
        return level;
    }
}
