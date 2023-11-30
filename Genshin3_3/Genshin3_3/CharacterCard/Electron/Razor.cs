using TCGBase;

namespace Genshin3_3
{
    public class Razor : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleA(0,2,4),
            new CharacterSimpleE(4,3),
            new CharacterEffectQ(4,3,new Effect_Razor())
        };

        public override ElementCategory CharacterElement => ElementCategory.Electro;

        public override WeaponCategory WeaponCategory => WeaponCategory.Claymore;

        public override CharacterRegion CharacterRegion => CharacterRegion.MONDSTADT;
    }
    public class Effect_Razor : AbstractCardPersistent
    {
        public override int MaxUseTimes => 2;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundStep,(me,p,s,v)=>p.AvailableTimes--},
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
            { SenderTag.RoundStep,(me,p,s,v)=>p.AvailableTimes=MaxUseTimes},
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
