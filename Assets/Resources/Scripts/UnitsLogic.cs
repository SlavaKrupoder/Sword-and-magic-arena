using TMPro;
using UnityEngine;
using SF = UnityEngine.SerializeField;

public class UnitsLogic : MonoBehaviour
{
    [SF] private HeroData HeroData;
    [SF] private TextMeshPro HeroHpText;
    [SF] private TextMeshPro HeroDmText;

    public void SetUnitsParams(int heroHealth, int heroDamage, bool isHero, bool IsEnemy)
    {
        HeroData.IsHero = isHero;
        HeroData.IsEnemy = IsEnemy;
        HeroData.PlayerDamage = heroDamage;
        HeroData.PlayerHealth = heroHealth;
        HeroHpText.text = heroHealth.ToString();
        HeroDmText.text = heroDamage.ToString();
        CheckUnitParams();
    }

    public HeroData GetUnitsParams()
    {
        return HeroData;
    }

    private void CheckUnitParams()
    {
        // need to create obj pool and returnt to pool after dead
        if(HeroData.PlayerHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
