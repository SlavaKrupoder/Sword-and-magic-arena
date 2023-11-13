using Newtonsoft.Json;
using System;
using UnityEngine;
using System.Collections.Generic;

public class GameLogic : MonoBehaviour
{
    // The generated Dll is located in the folder (Assets/Resources/Plugins/GameLogicCore.dll) and contains the same code,
    // if necessary, can be used as a third-party library instead of this script as discussed
    private static int HeroIndex = 0;
    private static int EemyIndex = 1;

    public string MakeTurn(string unitsObjectList, int[][] playerMove, int[][] unitsBoost)
    {
        List<Character> charactersList = JsonConvert.DeserializeObject<List<Character>>(unitsObjectList);

        if (playerMove != null && playerMove.Length > 0)
        {
            int[] _heroPosition = playerMove[HeroIndex];
            int[] _enemyPosition = playerMove[EemyIndex];
            int[] _heroBoostStats = unitsBoost[HeroIndex];
            int[] _enemyBoostStats = unitsBoost[EemyIndex];
            UpdateCharacterStats(charactersList, _heroPosition, _enemyPosition, _heroBoostStats, _enemyBoostStats);
        }

        return JsonConvert.SerializeObject(charactersList);
    }

    private static void UpdateCharacterStats(List<Character> charactersList, int[] heroPosition, int[] enemyPosition, int[] heroBoostStats, int[] enemyBoostStats)
    {
        if (heroPosition != null && enemyPosition != null)
        {
           var _updatedHeroStats = UpdateStats(charactersList[HeroIndex], heroBoostStats);
           var _updatedEnemyStats =  UpdateStats(charactersList[EemyIndex], enemyBoostStats);
            _updatedEnemyStats.PlayerHealth -= _updatedHeroStats.PlayerDamage;
        }
    }

    private static Character UpdateStats(Character characterStat, int[] boostStats)
    {
        characterStat.PlayerHealth += boostStats[0];
        characterStat.PlayerDamage += boostStats[1];
        return characterStat;
    }
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