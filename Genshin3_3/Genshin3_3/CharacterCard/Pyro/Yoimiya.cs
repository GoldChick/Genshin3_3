using TCGBase;

namespace Genshin3_3
{
    public class Yoimiya : AbstractCardCharacter
    {
        public override int MaxMP => 3;
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleA(0,2,3),
            new E(),
            new CharacterEffectQ(3,3,new Effect_Yoimiya_Q(),false)
        };

        public override ElementCategory CharacterElement => ElementCategory.Pyro;

        public override WeaponCategory WeaponCategory => WeaponCategory.Bow;

        public override CharacterRegion CharacterRegion => CharacterRegion.INAZUMA;

        public override string NameID => "yoimiya";
        public class E : AbstractCardSkill
        {
            public override SkillCategory Category => SkillCategory.E;
            public override CostInit Cost => new CostCreate().Pyro(1).ToCostInit();
            public override bool GiveMP => false;
            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
            {
                me.AddPersistent(new Effect_Yoimiya_E(2), c.Index);
            }
        }
    }
    public class Effect_Yoimiya_E : AbstractCardPersistent
    {
        public override int MaxUseTimes { get; }
        public override PersistentTriggerDictionary TriggerDic => new()
                {
                    { SenderTag.ElementEnchant,(me,p,s,v)=>
                        {
                            if (PersistentFunc.IsCurrCharacterDamage(me,p,s,v,out var dv))
                            {
                                if (s is PreHurtSender phs && phs.RootSource is AbstractCardSkill acs && acs.Category==SkillCategory.A)
                                {
                                    dv.Damage++;
                                    dv.Element=3;
                                }
                            }
                        }
                    }
                };
        public Effect_Yoimiya_E(int maxusetimes)
        {
            Variant = maxusetimes - 2;
            MaxUseTimes = maxusetimes;
            TriggerDic.Add(SenderTag.AfterUseSkill, (me, p, s, v) =>
            {
                if (me.TeamIndex == s.TeamID && s is AfterUseSkillSender uss && uss.Character.Index == p.PersistentRegion && uss.Skill.Category == SkillCategory.A)
                {
                    if (maxusetimes == 3)
                    {
                        me.Enemy.Hurt(new(3, 1), this);
                    }
                    p.AvailableTimes--;
                }
            });
        }
    }
    public class Effect_Yoimiya_Q : AbstractCardPersistent
    {
        public override int MaxUseTimes => 2;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundStart.ToString(),(me, p, s, v) => p.AvailableTimes --},
            { SenderTag.AfterUseSkill.ToString(),(me, p, s, v) =>
            {
                if(s is AfterUseSkillSender usks && usks.Skill.Category == SkillCategory.A)
                {
                    me.Enemy.Hurt(new(3, 1, 0), this);
                }
            }}
        };
    }

    public class Talent_Yoimiya : AbstractCardEquipmentOverrideSkillTalent
    {
        public override string CharacterNameID => "yoimiya";

        public override int Skill => 1;
        public override CostInit Cost => new CostCreate().Pyro(2).ToCostInit();

        public override void TalentTriggerAction(PlayerTeam me, Character c, int[] targetArgs) => me.AddPersistent(new Effect_Yoimiya_E(3), c.Index);
    }
}
