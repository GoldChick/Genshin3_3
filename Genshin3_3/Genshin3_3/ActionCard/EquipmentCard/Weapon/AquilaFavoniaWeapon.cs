using TCGBase;

namespace Genshin3_3
{
    public class AquilaFavoniaWeapon : AbstractCardWeapon
    {
        public override string NameID { get; }
        public override WeaponCategory WeaponCategory { get; }
        public override CostInit Cost => new CostCreate().Same(3).ToCostInit();
        public AquilaFavoniaWeapon(WeaponCategory category)
        {
            WeaponCategory = category;
            NameID = $"weapon_aquilafavonia_{category.ToString().ToLower()}";
        }
        public override int MaxUseTimes => 2;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            new PersistentPreset.RoundStepReset(),
            new PersistentPreset.WeaponDamageIncrease(),
            { SenderTag.AfterUseSkill,(me,p,s,v)=>
            {
                if (me.TeamIndex!=s.TeamID && p.AvailableTimes>0)
                {
                    me.Heal(this,new HealVariable(1));
                    p.AvailableTimes--;
                }
            }
            },
        };
    }
}
