using TCGBase;

namespace Genshin3_3
{
    public class MaguuKenki : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleA(0,2,7),
            new CharacterSingleSummonE(new Summon_MaguuKenki_Anemo(),7),
            new CharacterSingleSummonE(new Summon_MaguuKenki_Cryo(),1),
            new CharacterSimpleQ(7,4)
        };
        public override int MaxMP => 3;
        public override ElementCategory CharacterElement => ElementCategory.Anemo;

        public override WeaponCategory WeaponCategory => WeaponCategory.Other;

        public override CharacterRegion CharacterRegion => CharacterRegion.None;

        public override CharacterCategory CharacterCategory => CharacterCategory.Mob;
    }
    public class Summon_MaguuKenki_Anemo : AbstractCardPersistentSummon
    {
        public override int MaxUseTimes => 2;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundOver,(me,p,s,v)=>{ me.Enemy.Hurt(new(7,1),this);p.AvailableTimes--; } },
            { SenderTag.AfterUseSkill,(me,p,s,v)=>
            {
                if (me.TeamIndex==s.TeamID && s is AfterUseSkillSender ss && ss.Character.Card is MaguuKenki && ss.Skill.Category==SkillCategory.Q)
                {
                     me.Enemy.Hurt(new(7,1),this);
                }
            } 
            }
        };
    }
    public class Summon_MaguuKenki_Cryo : AbstractCardPersistentSummon
    {
        public override int MaxUseTimes => 2;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundOver,(me,p,s,v)=>{ me.Enemy.Hurt(new(1,1),this);p.AvailableTimes--; } },
            { SenderTag.AfterUseSkill,(me,p,s,v)=>
            {
                if (me.TeamIndex==s.TeamID && s is AfterUseSkillSender ss && ss.Character.Card is MaguuKenki && ss.Skill.Category==SkillCategory.Q)
                {
                     me.Enemy.Hurt(new(1,1),this);
                }
            }
            }
        };
    }
    public class Talent_MaguuKenki : AbstractCardEquipmentOverrideSkillTalent
    {
        public override int Skill => 1;

        public override string CharacterNameID => "maguukenki";

        public override CostInit Cost => new CostCreate().Anemo(3).ToCostInit();
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.AfterUseSkill,(me,p,s,v)=>
            {
                if (me.TeamIndex==s.TeamID && s is AfterUseSkillSender ss && ss.Character.Index==p.PersistentRegion && ss.Skill.Category==SkillCategory.E)
                {
                    if (ss.Skill.Cost.DiceCost[1]>0)
                    {
                        me.SwitchToLast();
	                }else if(ss.Skill.Cost.DiceCost[7]>0)
                    {
                        me.SwitchToNext();
                    }
	            }
            }
            }
        };
    }
}
