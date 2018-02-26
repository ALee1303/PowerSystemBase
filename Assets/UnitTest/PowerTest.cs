using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PowerTest
{
	[UnityTest]
	public IEnumerator MainPowerAwakeTest() // test awake method
    {
        GameObject go = new GameObject();
        go.AddComponent<MainPowerStatus>();
        MainPowerStatus MPS = go.GetComponent<MainPowerStatus>();

        yield return new WaitForFixedUpdate();

        Assert.AreEqual(50, MPS.AvailablePower);
        Assert.AreEqual(SourceTag.MainControl, MPS.powerDistribution[0].SourceTag);
        Assert.AreEqual(SourceTag.DockingBay, MPS.powerDistribution[1].SourceTag);
        Assert.AreEqual(SourceTag.DiningHall, MPS.powerDistribution[2].SourceTag);
        Assert.AreEqual(SourceTag.PrivateQuarter, MPS.powerDistribution[3].SourceTag);
        Assert.AreEqual(SourceTag.EngineRoom, MPS.powerDistribution[4].SourceTag);
        Assert.AreEqual(100, MPS.powerDistribution[0].maxPower);
        Assert.AreEqual(0, MPS.powerDistribution[0].AvailablePower);
        Assert.AreEqual(50, MPS.powerDistribution[1].maxPower);
        Assert.AreEqual(30, MPS.powerDistribution[2].maxPower);
    }

    [UnityTest]
    public IEnumerator MainPowerIncreaseTest() // test increasing reserve
    {
        GameObject goIncrease = new GameObject();
        goIncrease.AddComponent<MainPowerStatus>();

        MainPowerStatus IncreaseMPS = goIncrease.GetComponent<MainPowerStatus>(); // for increasing reserve

        yield return new WaitForFixedUpdate();

        //ReservePower Test
        Assert.AreEqual(50, IncreaseMPS.AvailablePower); // before increase
        IncreaseMPS.ReservePower(); // increase reserve by 10
        Assert.AreEqual(60, IncreaseMPS.AvailablePower); // check result
        IncreaseMPS.ReservePower(30); // increase power by 30
        Assert.AreEqual(90, IncreaseMPS.AvailablePower);
    }

    [UnityTest]
    public IEnumerator MainPowerSafeTest() // test handling edge cases
    {
        GameObject goFail = new GameObject();
        goFail.AddComponent<MainPowerStatus>();
        MainPowerStatus FailMPS = goFail.GetComponent<MainPowerStatus>(); // for failing to make change

        yield return new WaitForFixedUpdate();

        //Fail call Test
        Assert.AreEqual(50, FailMPS.AvailablePower); // initial
        FailMPS.RetrievePower(SourceTag.MainControl); // Retrieving from empty Source. shouldn't work
        Assert.AreNotEqual(60, FailMPS.AvailablePower);
        Assert.AreEqual(50, FailMPS.AvailablePower);
        FailMPS.TransferPower(SourceTag.MainControl, 60); // Transferring more than available.
        Assert.AreEqual(50, FailMPS.AvailablePower); // should be same
        Assert.AreNotEqual(60, FailMPS.powerDistribution[0].AvailablePower); //shouldn't have transfered
        FailMPS.TransferPower(SourceTag.DiningHall, 50); // Transferring more than needed.
        Assert.AreEqual(50, FailMPS.AvailablePower); // should be same
        Assert.AreNotEqual(50, FailMPS.powerDistribution[2]);
        Assert.AreEqual(0, FailMPS.powerDistribution[0].AvailablePower);
        FailMPS.RetrievePower(SourceTag.MainControl); // retrieving from empty source
        Assert.AreEqual(50, FailMPS.AvailablePower);
        Assert.AreEqual(0, FailMPS.powerDistribution[0].AvailablePower);
    }

    [UnityTest]
    public IEnumerator MainPowerEmptyTest() // test lif support edge cases
    {
        GameObject goEmpty = new GameObject();
        goEmpty.AddComponent<MainPowerStatus>();
        MainPowerStatus EmptyMPS = goEmpty.GetComponent<MainPowerStatus>(); // for making it empty

        yield return new WaitForFixedUpdate();

        //lifeSupport test
        Assert.AreEqual(50, EmptyMPS.AvailablePower);
        EmptyMPS.RetrieveLifeSupport(); // get rid of one life support
        Assert.AreEqual(60, EmptyMPS.AvailablePower); // more power should be available
        EmptyMPS.RetrieveLifeSupport(20); // getting rid of all life support
        Assert.AreEqual(80, EmptyMPS.AvailablePower); // Available should be Reserve now
        EmptyMPS.TransferPower(SourceTag.MainControl, 80); // Transferring all power to MainControl
        Assert.AreEqual(0, EmptyMPS.AvailablePower);
        Assert.AreEqual(80, EmptyMPS.powerDistribution[0].AvailablePower);
        EmptyMPS.RetrievePower(SourceTag.MainControl, 80); //Retrieve all power
        Assert.AreEqual(80, EmptyMPS.AvailablePower);
        Assert.AreEqual(0, EmptyMPS.powerDistribution[0].AvailablePower);
    }

}
