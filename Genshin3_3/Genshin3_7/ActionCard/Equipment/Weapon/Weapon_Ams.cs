using TCGBase;

namespace Genshin3_7
{
    public class Weapon_Ams : AbstractCardWeapon
    {
        public override WeaponCategory WeaponCategory => WeaponCategory.Bow;

        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.DamageIncrease,(me,p,s,v)=>
            {
                if (PersistentFunc.IsCurrCharacterDamage(me,p,s,v,out var dv))
                {
                    dv.Damage++;
                    if (s is PreHurtSender hs  && p.AvailableTimes>0 && hs.RootSource is AbstractCardSkill skill && skill.Cost.GetCost().Sum()>=5)
                    {
                        dv.Damage+=2;
                        p.AvailableTimes--;
                    }
                }
            }
            },
            { SenderTag.RoundStep,(me,p,s,v)=>p.AvailableTimes=MaxUseTimes}
        };

        public override CostInit Cost => new CostCreate().Same(3).ToCostInit();
    }
}
