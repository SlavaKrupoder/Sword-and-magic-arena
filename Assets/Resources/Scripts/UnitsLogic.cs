using TMPro;
using UnityEngine;
using SF = UnityEngine.SerializeField;

public class UnitsLogic : MonoBehaviour
{
    [SF] private TextMeshPro heroHpText;
    [SF] private TextMeshPro heroDmText;
    private HeroDataInfo heroData;

    public void SetUnitsParams(int heroHealth, int heroDamage, bool isHero, bool IsEnemy)
    {
        heroData = new HeroDataInfo();
        heroData.IsHero = isHero;
        heroData.IsEnemy = IsEnemy;
        heroData.PlayerDamage = heroDamage;
        heroData.PlayerHealth = heroHealth;
        heroHpText.text = heroHealth.ToString();
        heroDmText.text = heroDamage.ToString();
        CheckUnitParams();
    }

    public HeroDataInfo GetUnitsParams()
    {
        return heroData;
    }

    private void CheckUnitParams()
    {
        // need to create obj pool and returnt to pool after dead
        if (heroData.PlayerHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public class HeroDataInfo
    {
        public int PlayerHealth = 0;
        public int PlayerDamage = 0;
        public bool IsHero = false;
        public bool IsEnemy = false;
    }
   
}