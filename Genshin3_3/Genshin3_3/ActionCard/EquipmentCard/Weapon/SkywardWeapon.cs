using TCGBase;

namespace Genshin3_3
{
    public class SkywardWeapon : AbstractCardWeapon
    {
        public override string NameID { get; }
        public override WeaponCategory WeaponCategory { get; }
        public override CostInit Cost => new CostCreate().Same(3).ToCostInit();
        public SkywardWeapon(WeaponCategory category)
        {
            WeaponCategory = category;
            NameID = $"weapon_skyward_{category.ToString().ToLower()}";
        }
        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            new PersistentPreset.RoundStepReset(),
            { SenderTag.DamageIncrease,(me,p,s,v)=>
            {
                if (PersistentFunc.IsCurrCharacterDamage(me,p,s,v,out var dv))
                {
                    dv.Damage++;
                    if (p.AvailableTimes>0 &&s is PreHurtSender hs && hs.RootSource is AbstractCardSkill sk && sk.Category==SkillCategory.A)
                    {
                        dv.Damage++;
                        p.AvailableTimes--;
                    }
                }
            }
            }
        };
    }
}
