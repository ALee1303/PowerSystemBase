using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerSource : MonoBehaviour
{
    [SerializeField]
    protected int maxPower; // limits the power
    [SerializeField]
    private int currentPower; // obvious
    public int AvailablePower // get and set property accessing current power
    {
        get
        {
            return currentPower;
        }
        protected set
        {
            if (0 <= value && value <= maxPower)
            {
                currentPower = value;
            }
        }
    }
    // called by powerableObject. if it returns true, power the object
    public virtual bool HasDrawnPower(int pw = 10)
    {
        if (AvailablePower < pw)
            return false;
        AvailablePower -= pw;
        return true;
    }
    //called by powerableObject. if it returns false, turn off the power
    public virtual bool HasStoredPower(int pw = 10)
    {
        if (AvailablePower + pw > maxPower)
            return false;
        AvailablePower += pw;
        return true;
    }
}
