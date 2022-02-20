using System;
using UnityEngine;
using UnityEngine.UI;

using Apolysis.Interfaces;

public class Player : MonoBehaviour
{
    public float CurrentHealth { get; set; }
    public float MaximumHealth { get; set; }
    public float CurrentStamina { get; set; }
    public float MaximumStamina { get; set; }
    public float WalkingSpeed { get; set; }
    public float RunningSpeed { get; set; }
    public float CrouchingSpeed { get; set; }

    public event EventHandler<HealedEventArgs> Healed;
    public event EventHandler<DamagedEventArgs> Damaged;
    public event EventHandler<RestedEventArgs> Rested;
    public event EventHandler<SprintedEventArgs> Sprinted;

    public IUnityService UnityService;

    public Player(float currentHealth = 100, float maximumHealth = 100, float currentStamina = 100, float maximumStamina = 100, float walkingSpeed = 2, float runningSpeed = 4, float crouchingSpeed = 1)
    {
        //Check for health range exceptions
        if (currentHealth < 0) throw new ArgumentOutOfRangeException("currentHealth");
        if (currentHealth > maximumHealth) throw new ArgumentOutOfRangeException("currentHealth");

        //Check for stamina range exceptions
        if (currentStamina < 0) throw new ArgumentOutOfRangeException("currentStamina");
        if (currentStamina > maximumStamina) throw new ArgumentOutOfRangeException("currentStamina");

        CurrentHealth = currentHealth;
        MaximumHealth = maximumHealth;

        CurrentStamina = currentStamina;
        MaximumStamina = maximumStamina;

        WalkingSpeed = walkingSpeed;
        RunningSpeed = runningSpeed;
        CrouchingSpeed = crouchingSpeed;
    }

    private void Start()
    {
        if (UnityService == null)
            UnityService = new UnityService();
    }

    public void Heal(float amount)
    {
        var newHealth = Mathf.Min(CurrentHealth + amount, MaximumHealth);
        if (Healed != null)
            Healed(this, new HealedEventArgs(newHealth - CurrentHealth));
        CurrentHealth = newHealth;
    }

    public void Damage(float amount)
    {
        var newHealth = Mathf.Max(CurrentHealth - amount, 0);
        if (Damaged != null)
            Damaged(this, new DamagedEventArgs(CurrentHealth - newHealth));
        CurrentHealth = newHealth;
    }

    public void Rest(float amount)
    {
        var newStamina = Mathf.Min(CurrentStamina + amount, MaximumStamina);
        if (Rested != null)
            Rested(this, new RestedEventArgs(newStamina - CurrentStamina));
        CurrentStamina = newStamina;
    }

    public void Sprint(float amount)
    {
        var newStamina = Mathf.Max(CurrentStamina - amount, 0);
        if (Sprinted != null)
            Sprinted(this, new SprintedEventArgs(CurrentStamina - newStamina));
        CurrentStamina = newStamina;
    }
}


public class HealedEventArgs : EventArgs
{
    public HealedEventArgs(float amount)
    {
        Amount = amount;
    }
    public float Amount { get; private set; }
}

public class DamagedEventArgs : EventArgs
{
    public DamagedEventArgs(float amount)
    {
        Amount = amount;
    }
    public float Amount { get; private set; }
}

public class RestedEventArgs : EventArgs
{
    public RestedEventArgs(float amount)
    {
        Amount = amount;
    }
    public float Amount { get; private set; }
}

public class SprintedEventArgs : EventArgs
{
    public SprintedEventArgs(float amount)
    {
        Amount = amount;
    }
    public float Amount { get; private set; }
}