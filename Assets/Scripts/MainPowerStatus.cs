using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SourceTag { MainControl, DockingBay, DiningHall, PrivateQuarter, EngineRoom }

public class MainPowerStatus : MonoBehaviour
{
    private int powerReserve;
    private int lifeSupport;
    public List<PowerSource> powerDistribution { get; private set; }
    public int AvailablePower
    {
        get
        {
            return powerReserve - lifeSupport;
        }
        set
        {
            if (lifeSupport <= value)
                powerReserve = value + lifeSupport;
        }
    }

    void Awake()
    {
        powerDistribution = new List<PowerSource>();
        powerDistribution.Add(new PowerSource(SourceTag.MainControl,100));
        powerDistribution.Add(new PowerSource(SourceTag.DockingBay));
        powerDistribution.Add(new PowerSource(SourceTag.DiningHall, 30));
        powerDistribution.Add(new PowerSource(SourceTag.PrivateQuarter));
        powerDistribution.Add(new PowerSource(SourceTag.EngineRoom));
        this.powerReserve = 80;
        this.lifeSupport = 30;
    }

    public void TransferPower(SourceTag st, int pw = 10)
    {
        if (powerReserve - pw >= lifeSupport)
            if (powerDistribution[(int)st].StorePower(pw))
                AvailablePower -= pw;
    }
    public void RetrievePower(SourceTag st, int pw = 10)
    {
        if (powerDistribution[(int)st].DrawPower(pw))
            ReservePower(pw);
    }
    public void ReservePower(int pw = 10)
    {
        AvailablePower += pw;
    }
    public void RetrieveLifeSupport(int pw = 10)
    {
        if (lifeSupport >= pw)
        {
            lifeSupport -= pw;
        }
    }
}
