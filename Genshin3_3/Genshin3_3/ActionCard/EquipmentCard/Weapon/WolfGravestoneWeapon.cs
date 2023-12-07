using TCGBase;

namespace Genshin3_3
{
    public class WolfGravestoneWeapon : AbstractCardWeapon
    {
        public override string NameID { get; }
        public override WeaponCategory WeaponCategory { get; }
        public override CostInit Cost => new CostCreate().Same(3).ToCostInit();
        public WolfGravestoneWeapon(WeaponCategory category)
        {
            WeaponCategory = category;
            NameID = $"weapon_wolfgravestone_{category.ToString().ToLower()}";
        }
        public override int MaxUseTimes => 0;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            new PersistentPreset.WeaponDamageIncrease((me,p,s,v)=>me.Enemy.Characters[v.TargetIndex].HP<=6?3:1)
        };
    }
}
