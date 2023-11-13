using UnityEngine;
using UnityEngine.Events;

public class BoostPanel : MonoBehaviour
{
    public int[] boostValues;

    public void HpoinBoost(int hp)
    {
        boostValues[0] += hp;
        SendBoost(boostValues);
    }

    public void DamageBoost(int dm)
    {
        boostValues[1] = dm;
        SendBoost(boostValues);
    }

    private void SendBoost(int[] boostValues)
    {
        EventManager.current.SendBoostEvent(boostValues);
    }
}
