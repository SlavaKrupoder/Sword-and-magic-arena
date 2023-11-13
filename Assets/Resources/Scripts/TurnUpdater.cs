using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using SF = UnityEngine.SerializeField;

public class TurnUpdater : MonoBehaviour
{
    [SF] GameLogic gameLogic;
    [SF] GameRoot gameRoot;
    [SF] int playerHp;
    [SF] int playerDm;
    int[][] playerBoost = new int[][]
    {
        new int[] {0, 0},
        new int[] {0, 0}
    };

    private void Start()
    {
        playerBoost[0][0] = playerHp;
        playerBoost[0][1] = playerDm;
    }
   
    public void MakeTurn (GameObject[] unitsOfTurnList)
    {
        if (unitsOfTurnList.Length >= 2)
        {
            int[][] playerMove = new int[unitsOfTurnList.Length][];
            bool[] isHeroesList = new bool[unitsOfTurnList.Length];
            bool[] isEnemiesList = new bool[unitsOfTurnList.Length];
            Character[] characters = new Character[unitsOfTurnList.Length];

            for (int i = 0; i < unitsOfTurnList.Length; i++)
            {
                Character character = new Character(0, 0, 0, 0, true, false);
                if (unitsOfTurnList[i] != null)
                {
                    TileData.TileDataInfo unitInfo = unitsOfTurnList[i].GetComponentInParent<TileData>().GetDataInfo();
                    UnitsLogic.HeroDataInfo _unitInfo = unitsOfTurnList[i]?.GetComponent<UnitsLogic>().GetUnitsParams();
                    int rowIndex = unitInfo.TileRow;
                    int colIndex = unitInfo.TileColumn;
                    bool isHero = unitInfo.IsHeroPlace;
                    bool isEnemy = unitInfo.IsEnemyPlace;

                    isEnemiesList[i] = isEnemy;
                    isHeroesList[i] = isHero;

                    character.PlayerHealth = _unitInfo.PlayerHealth;
                    character.PlayerDamage = _unitInfo.PlayerDamage;
                    character.RowIndex = rowIndex;
                    character.ColIndex = colIndex;
                    character.IsHero = isHero;
                    character.IsEnemy = isEnemy;

                    characters[i] = character;
                    playerMove[i] = new int[] { rowIndex, colIndex };
                }
            }

            //string unitJsonData = CreateJsonData(characters);
            string unitJsonData = JsonConvert.SerializeObject(characters, Formatting.Indented);
            Debug.Log("jsonString = " + unitJsonData);
            var returnedString = gameLogic.MakeTurn(unitJsonData, playerMove, playerBoost);
            Debug.Log("returnedString = " + returnedString);
            List<Character> charactersList = JsonConvert.DeserializeObject<List<Character>>(returnedString);
            gameRoot.UpdateUnitsInfo(charactersList);
            playerBoost[0][0] = 0;
            playerBoost[0][1] = 0;

        }
        else
        {
            Debug.LogError("Insufficient units for the turn!");
        }
    }

    private void HandleBoostEvent(int[] boostValues)
    {
        if (playerBoost != null && playerBoost.Length > 0 && playerBoost[0] != null && playerBoost[0].Length > 0)
        {
            playerBoost[0][0] = boostValues[0];
            playerBoost[0][1] = boostValues[1];
            // for test
            playerHp = playerBoost[0][0] = boostValues[0];
            playerDm = playerBoost[0][1] = boostValues[1];
            //
        }
        else
        {
            Debug.LogWarning("Player boost array is not properly initialized!");
        }
    }

    private void OnEnable()
    {
        EventManager.OnBoostEvent += HandleBoostEvent;
    }

    private void OnDisable()
    {
        EventManager.OnBoostEvent -= HandleBoostEvent;
    }

    public class Character
    {
        public int PlayerHealth { get; set; }
        public int PlayerDamage { get; set; }
        public int RowIndex { get; set; }
        public int ColIndex { get; set; }
        public bool IsHero { get; set; }
        public bool IsEnemy { get; set; }


        public Character(int playerHealth, int playerDamage, int rowIndex, int colIndex, bool isHero, bool isEnemy)
        {
            PlayerHealth = playerHealth;
            PlayerDamage = playerDamage;
            RowIndex = rowIndex;
            ColIndex = colIndex;
            IsHero = isHero;
            IsEnemy = isEnemy;
        }
    }
}
