using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatientTracker : MonoBehaviour
{
    public Text patientCnt;
    public GameObject uncuredPatients;
    public GameObject curedPatients;
    public Patient[] totalPatients;
    private int totalPats;
    private int curedPats;
    void Start()
    {
        totalPats = uncuredPatients.transform.childCount;
        curedPats = 0;
        totalPatients = uncuredPatients.GetComponentsInChildren<Patient>();
    }

    private void Update()
    {
        patientCnt.text = "Patients Cured: \n" + curedPats + " / " + totalPats;
    }

    public void CheckCuredPatients()
    {
        foreach(Patient pat in uncuredPatients.GetComponentsInChildren<Patient>()) // totalPatients
        {
            if(pat.CheckIsCured())
            {
                pat.transform.SetParent(curedPatients.transform, false);
                curedPats = curedPatients.transform.childCount;
                pat.animator.SetBool("isCured", pat.CheckIsCured());
                //settings.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
            }
        }
    }
}
