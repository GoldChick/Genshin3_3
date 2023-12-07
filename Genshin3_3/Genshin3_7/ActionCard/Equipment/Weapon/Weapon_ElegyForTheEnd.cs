using TCGBase;

namespace Genshin3_7
{
    public class Weapon_ElegyForTheEnd : AbstractCardWeapon
    {
        public override WeaponCategory WeaponCategory => WeaponCategory.Bow;

        public override CostInit Cost => new CostCreate().Same(3).ToCostInit();
        public override int MaxUseTimes => 0;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.AfterUseSkill,(me,p,s,v)=>
            {
                if (me.TeamIndex==s.TeamID && s is AfterUseSkillSender ss && ss.Skill.Category==SkillCategory.Q && ss.Character.Index==p.PersistentRegion)
                {
                    me.AddPersistent(new Effect_ElegyForTheEnd());
	            }
            } 
            },
           new PersistentPreset.WeaponDamageIncrease()
        };
    }
    public class Effect_ElegyForTheEnd : AbstractCardEffect
    {
        public override int MaxUseTimes => 2;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            new PersistentPreset.RoundStepDecrease(),
            { SenderTag.DamageIncrease,(me,p,s,v)=>
            {
                if (me.TeamIndex==s.TeamID && v is DamageVariable dv && dv.DirectSource==DamageSource.Character)
                {
                    dv.Damage++;
	            }
            } 
            }
        };
    }
}
