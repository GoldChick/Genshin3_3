using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCGBase;

namespace Genshin3_7
{
    public class Amber : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleA(0,2,3),
            new CharacterSingleSummonE(new Summon_Amber(),3),
            new CharacterSimpleSkill(SkillCategory.Q,new CostCreate().Pyro(3).MP(2).ToCostInit(),new DamageVariable(3,2), new(-1, 2, 0, true)),
        };

        public override ElementCategory CharacterElement => ElementCategory.Pyro;

        public override WeaponCategory WeaponCategory => WeaponCategory.Bow;

        public override CharacterRegion CharacterRegion => CharacterRegion.MONDSTADT;
    }
    public class Summon_Amber : AbstractCardPersistentSummon
    {
        public override int MaxUseTimes => 1;
        public override bool CustomDesperated => true;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.HurtDecrease,new PersistentPurpleShield(2)},
            { SenderTag.RoundOver,(me,p,s,v)=>
            {
                if (p.AvailableTimes==0)
                {
                    me.Enemy.Hurt(new(3,2),this,()=>p.Active=false);
                }
            }
            }
        };
    }

    public class Talent_Amber : AbstractCardEquipmentOverrideSkillTalent
    {
        public override int Skill => 1;

        public override string CharacterNameID => "amber";

        public override CostInit Cost => new CostCreate().Pyro(3).ToCostInit();
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.AfterUseSkill,(me,p,s,v)=>
            {
                if (s is AfterUseSkillSender ss && ss.Skill.Category==SkillCategory.A && ss.Character.Index==p.PersistentRegion)
                {
                    var rabbit=me.Summons.Find(typeof(Summon_Amber));
                    if (rabbit!=null)
                    {
                        me.Enemy.Hurt(new(3,4),rabbit.Card,()=>rabbit.Active=false) ;
                    }
                }
            }
            }
        };
    }
}
