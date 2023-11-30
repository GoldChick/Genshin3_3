using TCGBase;

namespace Genshin3_3
{
    public class Xingqiu : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleSkill(SkillCategory.A,new CostCreate().Void(2).Hydro(1).ToCostInit(),new DamageVariable(0,2)),
            new CharacterSimpleSkill(SkillCategory.E,new CostCreate().Hydro(3).ToCostInit(),(skill,me,c,args) =>
                {
                    me.AttachElement(skill, 2,  c.Index-me.CurrCharacter);
                    me.AddPersistent(new Effect_Xingqiu_E(false));
                }, new DamageVariable(2, 2)),
            new CharacterSimpleSkill(SkillCategory.Q,new CostCreate().Hydro(3).MP(2).ToCostInit(),(skill,me,c,args) =>
                {
                    me.AttachElement(skill, 2, c.Index-me.CurrCharacter);
                    me.AddPersistent(new Effect_Xingqiu_Q());
                }, new DamageVariable(2, 2))
        };

        public override ElementCategory CharacterElement => ElementCategory.Hydro;

        public override WeaponCategory WeaponCategory => WeaponCategory.Sword;

        public override CharacterRegion CharacterRegion => CharacterRegion.LIYUE;
    }
    public class Effect_Xingqiu_E : AbstractCardPersistent
    {
        public override int MaxUseTimes { get; }
        public override PersistentTriggerDictionary TriggerDic { get; }
        public Effect_Xingqiu_E(bool talent)
        {
            Variant = talent ? 1 : 0;
            MaxUseTimes = talent ? 3 : 2;
            TriggerDic = new()
            {
                { SenderTag.HurtDecrease,new PersistentPurpleShield(1,talent?2:3)}
            };
        }

    }
    public class Effect_Xingqiu_Q : AbstractCardPersistent
    {
        public override int MaxUseTimes => 3;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.AfterUseSkill,(me,p,s,v)=>
            {
                if (me.TeamIndex==s.TeamID && s is AfterUseSkillSender ss && ss.Skill.Category==SkillCategory.A)
                {
                    me.Enemy.Hurt(new(2,1),this);
                    p.AvailableTimes--;
                }
            }
            }
        };
    }
    public class Talent_Xingqiu : AbstractCardEquipmentOverrideSkillTalent
    {
        public override int Skill => 1;

        public override string CharacterNameID => "xingqiu";

        public override CostInit Cost => new CostCreate().Hydro(3).ToCostInit();
        public override void TalentTriggerAction(PlayerTeam me, Character c, int[] targetArgs)
        {
            me.Enemy.Hurt(new(2, 2), c.Card.Skills[1], () =>
            {
                me.AttachElement(c.Card.Skills[1], 2, c.Index - me.CurrCharacter);
                me.AddPersistent(new Effect_Xingqiu_E(true));
            });
        }
    }
}
