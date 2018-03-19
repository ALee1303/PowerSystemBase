using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerableObject : MonoBehaviour, IPowerable
{
    [SerializeField]
    SourceTag sourceTag;
    public SourceTag SourceTag { get { return sourceTag; } }
    public MainPowerStatus MPS { get; private set; }
    public PowerSource PowerSource { get { return MPS.powerDistribution[(int)sourceTag]; } }
    public int requiredPower { get; private set; }
    public bool isPowered { get; private set; }
    // Use this for initialization
    void Start()
    {
        MPS = GameObject.Find("PowerStatus").GetComponent<MainPowerStatus>();
    }

    public void PowerUp()
    {
        if (!isPowered)
            if (PowerSource.DrawPower(requiredPower))
                isPowered = true;
    }
    public void PowerDown()
    {
        if (isPowered)
            if (PowerSource.StorePower(requiredPower))
                isPowered = false;
    }
}