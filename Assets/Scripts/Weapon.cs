using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public Transform firepoint;
    public GameObject bulletPrefab;
    public GameObject impactEffect;

    public string medicineName;
    public string usageInfo;
    public float damage = 40f;
    public float range = 2f;
    public float fireRate = 5f;
    public float impactForce = 30f;

    public int maxAmmo = 10;
    public int totalAmmo = 25;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;
    private bool isAmmoFinished = false;
    //private float nextTimeToFire = 0f;

    public Text ammoInfo;
    public Text shortInfo;
    public Text longInfo;
    public Image medImage;

    private void Awake()
    {
        currentAmmo = maxAmmo;
    }

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    private void OnEnable()
    {
        isReloading = false;
    }

    void Update()
    {
        if(isReloading)
        {
            return;
        }

        if(currentAmmo <= 0 && totalAmmo > 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if(totalAmmo <= 0)
        {
            isAmmoFinished = true;
        }

        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        ammoInfo.text = "Reload";
        AudioManager.Instance.PlayReloadSound();

        yield return new WaitForSeconds(reloadTime);

        if(maxAmmo > totalAmmo)
            currentAmmo = totalAmmo;
        else
            currentAmmo = maxAmmo;
        isReloading = false;
        ChangeWeaponUI();
    }

    void Shoot()
    {
        if(!isAmmoFinished)
        {
            FindObjectOfType<PlayerMovement>().GetComponent<BoxCollider2D>().enabled = false;
            // Prefabs Shooting Logic
            Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
            currentAmmo--;
            totalAmmo--;
            FindObjectOfType<PlayerMovement>().GetComponent<BoxCollider2D>().enabled = true;
            AudioManager.Instance.PlayShotSound();
            
        }
        ChangeWeaponUI();
    }

    public int GetCurrentAmmo()
    {
        if(currentAmmo == -1)
        {
            currentAmmo = maxAmmo;
        }
        return currentAmmo;
    }

    public void ChangeWeaponUI()
    {
        //bazookaType = baz.GetComponent<Weapon>().name;
        ammoInfo.text = GetCurrentAmmo() + " / " + totalAmmo;
        shortInfo.text = medicineName;
        longInfo.text = usageInfo;
        medImage.sprite = bulletPrefab.GetComponent<SpriteRenderer>().sprite;
        medImage.color = bulletPrefab.GetComponent<SpriteRenderer>().color;
    }
}
