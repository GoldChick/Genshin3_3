using TCGBase;

namespace Genshin3_7
{
    public class Weapon_VortexVanquisher : AbstractCardWeapon
    {
        public override WeaponCategory WeaponCategory => WeaponCategory.Longweapon;
        public override CostInit Cost => new CostCreate().Same(3).ToCostInit();

        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            new PersistentPreset.RoundStepReset(),
            { SenderTag.DamageIncrease,(me,p,s,v)=>
            {
                if (PersistentFunc.IsCurrCharacterDamage(me,p,s,v,out var dv))
                {
                    dv.Damage++;
                    if (me.Effects.Contains(typeof(AbstractPersistentShieldYellow)) || me.Characters[p.PersistentRegion].Effects.Contains(typeof(AbstractPersistentShieldYellow)))
                    {
                        dv.Damage++;
                    }
                }
            }
            },
            { SenderTag.AfterUseSkill,(me,p,s,v)=>
            {
                if (me.TeamIndex==s.TeamID && s is AfterUseSkillSender ss && ss.Character.Index==p.PersistentRegion && ss.Skill.Category==SkillCategory.E)
                {
                    var shield=me.Effects.Find(typeof(AbstractPersistentShieldYellow));
                    if (shield!=null)
                    {
                         shield.AvailableTimes++;
	                }
	            }
            } 
            }
        };
    }
}
