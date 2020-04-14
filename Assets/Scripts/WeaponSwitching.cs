using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedBazooka = 0;
    private string bazookaType;
    public Weapon weapon;
    /*
    public Text ammoInfo;
    public Text shortInfo;
    public Text longInfo;
    public Image medImage;
    */

    void Start()
    {
        SelectBazooka();
    }

    void Update()
    {
        int previousSelectedBazooka = selectedBazooka;

        if(Input.GetButtonDown("NextBazooka"))
        {
            if (selectedBazooka >= transform.childCount - 1)
                selectedBazooka = 0;
            else
                selectedBazooka++;
        }
        if (Input.GetButtonDown("PreviousBazooka"))
        {
            if (selectedBazooka <= 0)
                selectedBazooka = transform.childCount - 1;
            else
                selectedBazooka--;
        }

        if(previousSelectedBazooka != selectedBazooka)
        {
            SelectBazooka();
        }
    }

    void SelectBazooka()
    {
        int i = 0;
        foreach (Transform baz in transform)
        {
            if(i == selectedBazooka)
            {
                baz.gameObject.SetActive(true);
                weapon = baz.GetComponent<Weapon>();
                weapon.ChangeWeaponUI();
            }
            else
            {
                baz.gameObject.SetActive(false);
            }
            i++;
        }
    }

}
