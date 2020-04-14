using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;
    public GameObject bazookaHolder;

    Vector2 movement;

    [Header("Raycast Variables")]
    public float rayCastLength = 2;
    private LayerMask layerMask;
    private Vector2 startPoint1 = Vector2.zero;
    private Vector2 pivotPoint1 = Vector2.zero;
    public float angle1 = 180.0f;

    [Header("Patient UI")]
    public GameObject patientPanel;
    public Image patientImage;
    public Text patientDetails;
    public Text patientSymptoms;
    public PatientTracker patientTracker;

    void Start()
    {
        //transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        layerMask = LayerMask.GetMask("Patient");
        patientPanel.SetActive(false);
    }

    private void Update()
    {
        // Pause Game
        if(Input.GetButtonDown("Pause"))
        {
            FindObjectOfType<GameManager>().PauseCurrentGame();
        }

        // Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(movement.x) == 1 || Mathf.Abs(movement.y) == 1)
        {
            animator.SetFloat("LastXDirection", movement.x);
            animator.SetFloat("LastYDirection", movement.y);
        }

        // Animation
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        // Debug.DrawRay(transform.position, new Vector2(animator.GetFloat("LastXDirection"), 0) * rayCastLength, Color.red);
        // Debug.DrawRay(transform.position, new Vector2(0, animator.GetFloat("LastYDirection")) * rayCastLength, Color.red);

        Vector2 dirX = new Vector2(animator.GetFloat("LastXDirection"), 0);
        Vector2 dirY = new Vector2(0, animator.GetFloat("LastYDirection"));

        // Bazooka Rotation
        if (dirX.x == -1)
        {
            // position: -0.33, -0.11, 0
            // rotation: 0, -180, 0
            bazookaHolder.transform.position = new Vector3(
                -0.33f + transform.position.x, 
                -0.11f + transform.position.y, 
                0f);
            bazookaHolder.transform.rotation = Quaternion.Euler(0f, -180f, 0f);
        }
        else if (dirX.x == 1)
        {
            // position: -0.33, -0.11, 0
            // rotation: 0, 0, 0
            bazookaHolder.transform.position = new Vector3(
                0.33f + transform.position.x, 
                -0.11f + transform.position.y, 
                0f);
            bazookaHolder.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (dirY.y == 1)
        {
            // position: -0.27, -0.47, 0
            // rotation: 0, 0, 90
            bazookaHolder.transform.position = new Vector3(
                -0.28f + transform.position.x, 
                0.48f + transform.position.y, 
                0f);
            bazookaHolder.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
        else if (dirY.y == -1)
        {
            // position: 0.25, 0, 0
            // rotation: 0, 0, -90
            bazookaHolder.transform.position = new Vector3(
                0.28f + transform.position.x, 
                -0.45f + transform.position.y, 
                0f);
            bazookaHolder.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        }

        // RayCasting
        RaycastHit2D hit = CheckRaycast(new Vector2(0,0));
        if (Mathf.Abs(dirX.x) == 1)
        {
            hit = CheckRaycast(dirX);
        }
        else if (Mathf.Abs(dirY.y) == 1)
        {
            hit = CheckRaycast(dirY);
        }
        
        if (hit.collider != null)
        {
            //Debug.Log("Collider: " + hit.collider.tag + "; Name: " + hit.collider.name);
            //Debug.Log("");
            if (hit.collider.tag == "Patient")
            {
                patientImage.sprite = hit.collider.GetComponent<SpriteRenderer>().sprite;
                string ptext = "", syText = "";
                ptext += "Name: " + hit.collider.GetComponent<Patient>().p_name + "\n";
                ptext += "Age: " + hit.collider.GetComponent<Patient>().age + "\n";
                ptext += "Gender: " + hit.collider.GetComponent<Patient>().gender + "\n";
                if(hit.collider.GetComponent<Patient>().diagnosis != "")
                    ptext += "Diagnosis: " + hit.collider.GetComponent<Patient>().diagnosis+ "\n";
                patientDetails.text = ptext;
                foreach (Symptom sy in hit.collider.GetComponent<Patient>().symptoms)
                {
                    if(sy.GetLevel() > 0)
                        syText += sy.s_name + " - " + sy.GetLevel() + "% \n";
                }
                if(hit.collider.GetComponent<Patient>().CheckIsCured())
                {
                    patientSymptoms.text = "CURED!";
                    patientTracker.CheckCuredPatients();
                }
                else
                {
                    patientSymptoms.text = syText;
                }

                patientPanel.SetActive(true);
            }
        }
        else
        {
            patientPanel.SetActive(false);
        }
    }
    void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);   
    }

    private RaycastHit2D CheckRaycast(Vector2 dir)
    {
        return Physics2D.Raycast(
            transform.position,
            dir,
            rayCastLength,
            layerMask);
    }

}
