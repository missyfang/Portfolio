using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Inventory weapons;
    public Sprite[] attacks;

    private Weapon spawnedWeapon;
    private Rigidbody rb;
    Vector3 weaponPos;
    Vector3 weaponRot;

    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (weapons.altWeapons != null && Input.GetKeyDown(KeyCode.Space)) {
            weapons.altWeaponSwitch();
        }

        updateCooldowns(-Time.deltaTime);

        if (weapons.weapon1 != null && Input.GetKeyDown("x") && ArrowKeyMovement.playerControl
            && weaponReady(weapons.weapon1))
        {
            Debug.Log("Sword Attack Start");
            ArrowKeyMovement.attackDirection = ArrowKeyMovement.direction;
            ArrowKeyMovement.playerControl = false;
            StartCoroutine(MakeAttack(weapons.weapon1));

        }

        if (weapons.weapon2 != null && weapons.weapon2.type != "empty" 
            && Input.GetKeyDown("z") && ArrowKeyMovement.playerControl
            && weapons.hasAmmunition() && weaponReady(weapons.weapon2))
        {
            ArrowKeyMovement.attackDirection = ArrowKeyMovement.direction;
            weapons.subAmmunition();
            ArrowKeyMovement.playerControl = false;
            StartCoroutine(MakeAttack(weapons.weapon2));

        }
    }

    void updateCooldowns(float changeToCooldown)
    {
        HasCooldown weapon1CD = weapons.weapon1.GetComponent<HasCooldown>();
        HasCooldown weapon2CD = weapons.weapon2.GetComponent<HasCooldown>();

        if (weapon1CD != null)
        {
            weapon1CD.AlterCooldown(changeToCooldown);
            weapon1CD.AlterSpecialCooldown(changeToCooldown);
        }

        if (weapon2CD != null)
        {
            weapon2CD.AlterCooldown(changeToCooldown);
            weapon2CD.AlterSpecialCooldown(changeToCooldown);
        }
    }

    bool weaponReady(Weapon weapon)
    {
        return weapon.GetComponent<HasCooldown>().CooldownReady();
    }

    IEnumerator MakeAttack(Weapon usedWeapon)
    {

        while (ArrowKeyMovement.isMoving)
        {
            yield return new WaitForSeconds(0.1f);
        }

        if (usedWeapon.type != "bomb")
        {
            AudioManager.PlayAudio(usedWeapon.type.ToString() + "Sfx");
        }

        // Reset cooldowns
        usedWeapon.GetComponent<HasCooldown>().ResetCooldown();

        if (usedWeapon.GetComponent<HasCooldown>().SpecialCooldownReady())
        {
            usedWeapon.GetComponent<HasCooldown>().ResetSpecialCooldown();
        }

        // Variables needed for instantiation.
        Vector3 weaponVel;
        weaponPos = this.GetComponent<Transform>().position;
        Animator playerAnim = this.GetComponent<Animator>();
        string currentClip = playerAnim.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        playerAnim.enabled = false;

        // Sprites needed for attacks.
        Sprite originalSprite = this.GetComponent<SpriteRenderer>().sprite;
        Debug.Log(originalSprite.name.ToString());
        Debug.Log(attacks[0].name.ToString());

        // Generate parameters based on direction the player is facing.
        if (this.GetComponent<Transform>().rotation.y == 1 && currentClip == "run_right") {
            Debug.Log("Rotate Left");

            weaponPos.x -= 1f;
            weaponRot = new Vector3(0f, 0f, 90f);
            weaponVel = new Vector3(-usedWeapon.speed, 0, 0);

            this.GetComponent<SpriteRenderer>().sprite = attacks[1];
        }
        else if (this.GetComponent<Transform>().rotation.y == 0 && currentClip == "run_right")
        {
            Debug.Log("Rotate Right");
            weaponPos.x += 1f;
            weaponRot = new Vector3(0f, 0f, 270f);
            weaponVel = new Vector3(usedWeapon.speed, 0, 0);
            this.GetComponent<SpriteRenderer>().sprite = attacks[0];
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (currentClip == "run_down")
        {
            weaponPos.y -= 1f;
            weaponRot = new Vector3(0f, 0f, 180f);
            weaponVel = new Vector3(0, -usedWeapon.speed, 0);
            this.GetComponent<SpriteRenderer>().sprite = attacks[2];
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            weaponPos.y += 1f;
            weaponRot = new Vector3(0f, 0f, 0f);
            weaponVel = new Vector3(0, usedWeapon.speed, 0);
            this.GetComponent<SpriteRenderer>().sprite = attacks[3];
        }

        if (!usedWeapon.rotate)
        {
            weaponRot = Vector3.zero;
        }

        // Spawn weapon and wait a little bit of time before launching it.
        spawnedWeapon = Instantiate(usedWeapon, weaponPos, Quaternion.identity);
        spawnedWeapon.transform.Rotate(weaponRot.x, weaponRot.y, weaponRot.z);

        yield return new WaitForSeconds(0.1f);

        ArrowKeyMovement.playerControl = true;
        playerAnim.enabled = true;
        this.GetComponent<SpriteRenderer>().flipX = false;

        if (spawnedWeapon != null)
        {
            // Launch weapon
            rb = spawnedWeapon.GetComponent<Rigidbody>();
            rb.velocity = weaponVel;
        }

        yield return new WaitForSeconds(1f);
    }
}
