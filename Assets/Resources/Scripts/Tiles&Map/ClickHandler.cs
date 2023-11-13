using UnityEngine;
using UnityEngine.UI;
using SF = UnityEngine.SerializeField;

public class ClickHandler : MonoBehaviour
{
    [SF] TurnUpdater turnUpdater;
    [SF] Button btnTrunPlay;
    [SF] ClickDetector clickDetector;
    GameObject[] unitsOfTurnList;

    private void Start()
    {
        btnTrunPlay.interactable = false;
    }

    public void HandleClick(GameObject[] unitsGameObjectList)
    {
        unitsOfTurnList = unitsGameObjectList;
        btnTrunPlay.interactable = true;
    }

    public void HandleAiClick(GameObject[] unitsGameObjectList)
    {
        unitsOfTurnList = unitsGameObjectList;
        btnTrunPlay.interactable = true;
    }

    public void PlayTurn()
    {
        turnUpdater.MakeTurn(unitsOfTurnList);
        btnTrunPlay.interactable = false;
        clickDetector.MakeAITurn();
    }
}
