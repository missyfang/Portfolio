using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    // Data Storage
    int rupee_count = 0;
    int key_count = 0;
    int bomb_count = 0;
    int current_alt_weapon = 0;
    int brang_count = 1;
    int max = 255;

    // Weapon Storage
    public Weapon weapon1;
    public Weapon weapon2;
    public Weapon[] altWeapons = new Weapon[3];
    [SerializeField]
    private TextMeshProUGUI itemDisplay;
    [SerializeField]
    private TextMeshProUGUI item1Display;
    [SerializeField]
    private GameObject altWeaponDisplay;

    void Start()
    {
        weapon1.GetComponent<NoBeam>().enabled = false;
    }
    void Update()
    {
        itemDisplay.text = "x" + rupee_count.ToString() + "\n" +
                           "x" + key_count.ToString() + "\n";
        item1Display.text = "x" + bomb_count.ToString() + "\n";

        altWeaponUpdate();

        // Make the primary weapon not fire a beam if Link is not at full health.
        if (gameObject.GetComponent<HasHealth>().numHealth < gameObject.GetComponent<HasHealth>().maxHealth
            || !weapon1.GetComponent<HasCooldown>().SpecialCooldownReady())
        {
            weapon1.speed = 0f;
            weapon1.GetComponent<NoBeam>().enabled = true;
        }

        if (gameObject.GetComponent<HasHealth>().numHealth == gameObject.GetComponent<HasHealth>().maxHealth
            && weapon1.GetComponent<HasCooldown>().SpecialCooldownReady())
        {
            weapon1.speed = 12f;
            weapon1.GetComponent<NoBeam>().enabled = false;
        }
    }

    private void altWeaponUpdate()
    {
        for (int i = 0; i < altWeaponDisplay.transform.childCount; ++i)
        {
            if (altWeaponDisplay.transform.GetChild(i).tag != weapon2.type)
            {
                altWeaponDisplay.transform.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                altWeaponDisplay.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    public void switchBeamActivation(float speed)
    {
        weapon1.speed = speed;
        weapon1.GetComponent<NoBeam>().enabled = true;
    }

    public void altWeaponSwitch()
    {
        current_alt_weapon = (current_alt_weapon + 1) % altWeapons.Length;
        weapon2 = altWeapons[current_alt_weapon];
    }

   public void addAltWeapon(Weapon newAltWeapon)
    {
        for (int i = 0; i < altWeapons.Length; i++)
        {
            if (altWeapons[i].type == "empty")
            {
                altWeapons[i] = newAltWeapon;
                return;
            }
        }
    }
    public bool hasAmmunition()
    {
        if (weapon2.type == "arrow")
        {
            return GetRupees() > 0;
        }

        if (weapon2.type == "bomb")
        {
            return GetBombs() > 0;
        }

        if (weapon2.type == "brang")
        {
            return GetBrang() > 0;
        }

        return true;
    }

    public void subAmmunition()
    {
        if (weapon2.type == "arrow")
        {
            SubRupees(1);
        }

        if (weapon2.type == "bomb")
        {
            SubBombs(1);
        }

        if (weapon2.type == "brang")
        {
            SubBrang(1);
        }

        return;
    }

    public void AddRupees(int num_rupees)
    {
        rupee_count += num_rupees;
        if (rupee_count > max)
            rupee_count = max;
       
    }

    public void AddKeys(int num_keys)
    {
        key_count += num_keys;
        if (key_count > max)
            key_count = max;
    }

    public void AddBombs(int num_bombs)
    {
        bomb_count += num_bombs;
        if (bomb_count > max)
            bomb_count = max;
    }

    public void AddBrang(int num_brang)
    {
        brang_count = num_brang;
    }

    public void SubRupees(int num_rupees)
    {
        rupee_count -= num_rupees;
    }

    public void SubKeys(int num_keys)
    {
        key_count -= num_keys;
    }

    public void SubBombs(int num_bombs)
    {
        bomb_count -= num_bombs;
    }

    public void SubBrang(int num_brang)
    {
        brang_count -= num_brang;
    }

    public int GetRupees()
    {
        return rupee_count;
    }

    public int GetKeys()
    {
        return key_count;
    }

    public int GetBombs()
    {
        return bomb_count;
    }

    public int GetBrang()
    {
        return brang_count;
    }
}
