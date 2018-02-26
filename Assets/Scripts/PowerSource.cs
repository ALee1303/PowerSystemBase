using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSource
{
    public int maxPower { get; private set; }
    private int currentPower;
    private SourceTag sourceTag;
    public int AvailablePower
    {
        get
        {
            return currentPower;
        }
        private set
        {
            if (0 <=  value && value <= maxPower)
                currentPower = value;
        }
    }
    public SourceTag SourceTag
    {
        get
        {
            return sourceTag;
        }
    }

    public PowerSource(SourceTag sourceTag, int maxPower = 50, int currentPower = 0)
    {
        this.maxPower = maxPower;
        this.currentPower = currentPower;
        this.sourceTag = sourceTag;
    }

    public bool DrawPower(int pw = 10)
    {
        if (AvailablePower < pw) return false;
        AvailablePower -= pw;
        return true;
    }
    public bool StorePower(int pw = 10)
    {
        if (AvailablePower + pw > maxPower) return false;
        AvailablePower += pw;
        return true;
    }
}
