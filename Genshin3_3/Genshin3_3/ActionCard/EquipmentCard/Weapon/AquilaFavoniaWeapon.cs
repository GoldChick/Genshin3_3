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
            { SenderTag.RoundStep,(me,p,s,v)=>p.AvailableTimes=2},
            { SenderTag.AfterUseSkill,(me,p,s,v)=>
            {
                if (me.TeamIndex!=s.TeamID && p.AvailableTimes>0)
                {
                    me.Heal(this,new HealVariable(1));
                    p.AvailableTimes--;
                }
            }
            },
            { SenderTag.DamageIncrease,(me,p,s,v)=>
            {
                if (PersistentFunc.IsCurrCharacterDamage(me,p,s,v,out var dv))
                {
                    if (dv.Element>=0)
                    {
                        dv.Damage++;
                    }
                }
            }
            }
        };
    }
}
