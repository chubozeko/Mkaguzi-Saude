using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public string[] usedFor;
    public float dosageForChildren = 100f;
    public float dosageForTeens = 50f;
    public float dosageForAdults = 50f;
    public float dosageForElders = 34f;
    private float[] dosages;
    public Rigidbody2D rb;
    public GameObject impactEffect;

    void Start()
    {
        rb.velocity = transform.right * speed;
        dosages = new float[4];
        dosages[0] = dosageForChildren;
        dosages[1] = dosageForTeens;
        dosages[2] = dosageForAdults;
        dosages[3] = dosageForElders;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // Debug.Log(col.name);
        if(col.tag == "Patient")
        {
            Patient pat = col.GetComponent<Patient>();
            if (pat != null)
            {    
                pat.TreatSymptom(usedFor, dosages);
                Destroy(gameObject);
                AudioManager.Instance.PlaySplashSound();
            }

            // Instantiate(impactEffect, transform.position, transform.rotation);
            
        }
    }
}
