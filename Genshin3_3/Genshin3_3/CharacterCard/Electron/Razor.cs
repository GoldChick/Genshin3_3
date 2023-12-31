﻿using TCGBase;

namespace Genshin3_3
{
    public class Razor : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleSkill(SkillCategory.A,new CostCreate().Void(2).Electro(1).ToCostInit(),new DamageVariable(0,2)),
            new CharacterSimpleSkill(SkillCategory.E,new CostCreate().Electro(3).ToCostInit(),new DamageVariable(4,3)),
            new CharacterSimpleSkill(SkillCategory.Q,new CostCreate().Electro(3).MP(2).ToCostInit(),
                (skill,me,c,args)=>me.AddPersistent(new Effect_Razor(),c.Index),new DamageVariable(4,3)),
        };

        public override ElementCategory CharacterElement => ElementCategory.Electro;

        public override WeaponCategory WeaponCategory => WeaponCategory.Claymore;

        public override CharacterRegion CharacterRegion => CharacterRegion.MONDSTADT;
    }
    public class Effect_Razor : AbstractCardEffect
    {
        public override int MaxUseTimes => 2;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            new PersistentPreset.RoundStepDecrease(),
            { SenderTag.AfterUseSkill,(me,p,s,v)=>
            {
                if (me.TeamIndex==s.TeamID && s is AfterUseSkillSender ss && ss.Character.Index==p.PersistentRegion && (ss.Skill.Category==SkillCategory.A||ss.Skill.Category==SkillCategory.E))
                {
                    me.Enemy.Hurt(new(4,2),this);
                }
            }
            }
        };
    }
    public class Talent_Razor : AbstractCardEquipmentOverrideSkillTalent
    {
        public override int Skill => 1;

        public override string CharacterNameID => "razor";
        public override CostInit Cost => new CostCreate().Electro(3).ToCostInit();
        public override int MaxUseTimes => 1;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            new PersistentPreset.RoundStepReset(),
            { SenderTag.AfterUseSkill,(me,p,s,v)=>
            {
                if (p.AvailableTimes>0 &&me.TeamIndex==s.TeamID && s is AfterUseSkillSender ss && ss.Character.Index==p.PersistentRegion && ss.Skill.Category==SkillCategory.E)
                {
                    for (int i = 0; i < me.Characters.Length; i++)
                    {
                        var c=me.Characters[(i+me.CurrCharacter)%me.Characters.Length];
                        if (c.MP<c.Card.MaxMP&&c.Card.CharacterElement==ElementCategory.Electro)
                        {
                            c.MP++;
                            p.AvailableTimes--;
                            break;
	                    }
			        }
                }
            } 
            }
        };
    }
}
