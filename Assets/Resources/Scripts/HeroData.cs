using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/HeroData", order = 1)]
public class HeroData : ScriptableObject
{
    public int PlayerHealth = 0;
    public int PlayerDamage = 0;
    public bool IsHero = false;
    public bool IsEnemy = false;
}
