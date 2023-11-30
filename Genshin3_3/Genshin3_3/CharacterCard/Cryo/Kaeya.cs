using TCGBase;

namespace Genshin3_3
{
    public class Kaeya : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleA(0,2,1),
            new CharacterSimpleE(1,3),
            new CharacterEffectQ(1,1,new Effect_Kaeya(),false,1,4)
        };

        public override ElementCategory CharacterElement => ElementCategory.Cryo;

        public override WeaponCategory WeaponCategory => WeaponCategory.Sword;

        public override CharacterRegion CharacterRegion => CharacterRegion.MONDSTADT;

        public override string NameID => "kaeya";
    }
    public class Effect_Kaeya : AbstractCardPersistent
    {
        public override string NameID => "effect_kaeya";
        public override int MaxUseTimes => 3;
        public override PersistentTriggerDictionary TriggerDic => new()
            {
                { SenderTag.AfterSwitch,(me,p,s,v)=>
                    {
                        if (me.TeamIndex==s.TeamID)
                        {
                            me.Enemy.Hurt(new DamageVariable(1,2),this);
                        }
                    }
                }
            };
    }

    public class Talent_Kaeya : AbstractCardEquipmentOverrideSkillTalent
    {
        public override string CharacterNameID => "kaeya";

        public override int Skill => 1;

        public override CostInit Cost => new CostCreate().Cryo(4).ToCostInit();

        public override int MaxUseTimes => 1;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundStep,(me,p,s,v)=>p.AvailableTimes=1},
            { SenderTag.AfterUseSkill,(me,p,s,v)=>
            {
                if (me.TeamIndex==s.TeamID && p.AvailableTimes>0 && s is AfterUseSkillSender ss && ss.Character.Index==p.PersistentRegion && ss.Skill.Category==SkillCategory.E)
                {
                    me.Heal(ss.Skill,new HealVariable(2,ss.Character.Index-me.CurrCharacter));
                    p.AvailableTimes--;
                }
            }
            }
        };
    }
}
