using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasCooldown : MonoBehaviour
{
    [SerializeField]
    private float cooldown;
    [SerializeField]
    private float specialCooldown;

    [SerializeField]
    private float currentCooldown = 0;
    [SerializeField]
    private float currentSpecialCooldown = 0;

    void Start()
    {
        currentCooldown = 0;
        currentSpecialCooldown = 0;
    }
    public void AlterCooldown(float changeToCooldown)
    {
        if (currentCooldown <= 0)
        {
            currentCooldown = 0;
            return;
        }

        currentCooldown += changeToCooldown;
    }

    public void AlterSpecialCooldown(float changeToCooldown)
    {
        if (currentSpecialCooldown <= 0)
        {
            currentSpecialCooldown = 0;
            return;
        }

        currentSpecialCooldown += changeToCooldown;
    }

    public void ResetCooldown()
    {
        currentCooldown = cooldown;
    }

    public void ResetSpecialCooldown()
    {
        currentSpecialCooldown = specialCooldown;
    }

    public bool CooldownReady()
    {
        return currentCooldown == 0;
    }

    public bool SpecialCooldownReady()
    {
        return currentSpecialCooldown == 0;
    }
}
