using UnityEngine;
using SF = UnityEngine.SerializeField;

public class ClickHandler : MonoBehaviour
{
    [SF] TurnUpdater turnUpdater;

    public void HandleClick(GameObject[] unitsOfTurnList)
    {
        turnUpdater.MakeTurn(unitsOfTurnList);
    }
}
