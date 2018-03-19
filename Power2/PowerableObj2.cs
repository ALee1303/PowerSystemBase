using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerableObj2 : MonoBehaviour, IPowerable  // derived class can declare its functionality and condition on their own call method
{
    [SerializeField]
    private bool isPowered; // true if powerableObject is on. false if not.
    [SerializeField]
    private int requiredPower; // power that will draw from source when it's powered, and therefore give when off.

    //Properties
    public PowerSource PowerSource { get; protected set; } // when it is pickupable, hooked to player's powerSource on initialization
    public int RequiredPower
    {
        get
        {
            return requiredPower;
        }
        protected set
        {
            if (requiredPower != value)
            {
                requiredPower = value;
            }
        }
    }
    public bool IsPowered
    {
        get
        {
            return isPowered;
        }
        protected set
        {
            if (isPowered != value)
            {
                isPowered = value;
            }
        }
    }

    /// <summary>
    /// functions used to draw power from PowerSource
    /// </summary>
    public virtual void PowerUp() // virtual for now(in case)
    {
        if (!IsPowered && PowerSource.HasDrawnPower(RequiredPower)) // if it's not powered, check if it can draw power
            IsPowered = true;                                       // if it can, draw power and power up
    }
    /// <summary>
    /// functions used to store power to PowerSource
    /// </summary>
    public virtual void PowerDown() // virtual for now(in case)
    {
        if (IsPowered && PowerSource.HasStoredPower(RequiredPower)) // if it's powered, check if power source is not full
            IsPowered = false;                                      // if it's not, give back power and turn off.
    }
}