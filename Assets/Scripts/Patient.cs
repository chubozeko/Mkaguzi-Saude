using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Patient : MonoBehaviour
{
    public string p_name;
    public int age;
    public string gender;
    public string diagnosis;
    private bool isCured = false;
    private int treatedSymptoms;
    public Symptom[] symptoms;

    public float health = 100;

    public GameObject curedEffect;
    public Animator animator;

    private void Start()
    {
        treatedSymptoms = symptoms.Length;
    }

    public void TreatSymptom(string[] usedFor, float[] dosages)
    {
        float damage = CheckDosageAge(dosages);
        
        foreach(Symptom sym in symptoms)
        {
            foreach(string usage in usedFor)
            {
                if(sym.s_name == usage)
                {
                    sym.Treat(damage);
                }
            }
            if(sym.IsTreated())
            {
                //Debug.Log(sym.name + " is treated!");
                treatedSymptoms--;
            }
        }
        
    }

    private float CheckDosageAge(float[] dosages)
    {
        if (age < 13)
        {
            // Children
            return dosages[0];
        }
        else if (age >= 13 && age < 20)
        {
            // Teens
            return dosages[1];
        }
        else if (age >= 20 && age < 50)
        {
            // Adults
            return dosages[2];
        }
        else if (age >= 50)
        {
            // Elderly
            return dosages[3];
        }
        else
        {
            return dosages[0];
        }
    }

    public bool CheckIsCured()
    {
        foreach (Symptom sym in symptoms)
        {
            if(sym.IsTreated())
            {
                isCured = true;
                // animator.SetBool("isCured", isCured);
            }
            else
            {
                isCured = false;
                break;
            }
        }
        return isCured;
    }
}
