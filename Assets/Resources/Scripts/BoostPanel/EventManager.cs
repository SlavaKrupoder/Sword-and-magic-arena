using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager current;

    public delegate void BoostEvent(int[] boostValues);
    public static event BoostEvent OnBoostEvent;

    private void Awake()
    {
        current = this;
    }

    public void SendBoostEvent(int[] boostValues)
    {
        OnBoostEvent?.Invoke(boostValues);
    }
}
