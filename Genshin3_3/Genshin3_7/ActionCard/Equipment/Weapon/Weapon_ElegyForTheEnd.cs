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
            { SenderTag.DamageIncrease,(me,p,s,v)=>
            {
                if (PersistentFunc.IsCurrCharacterDamage(me,p,s,v,out var dv))
                {
                     dv.Damage++;
	            }
            } 
            }
        };
    }
    public class Effect_ElegyForTheEnd : AbstractCardPersistent
    {
        public override int MaxUseTimes => 2;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundStep,(me,p,s,v)=>p.AvailableTimes--},
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
