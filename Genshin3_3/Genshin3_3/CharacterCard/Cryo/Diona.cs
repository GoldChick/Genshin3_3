using TCGBase;

namespace Genshin3_3
{
    /// <summary>
    /// 奥兹换班霜华矢，唯此一心冰灵珠！
    /// </summary>
    public class Diona : AbstractCardCharacter
    {
        public override int MaxMP => 3;
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[] {
            new CharacterSimpleSkill(SkillCategory.A,new CostCreate().Void(2).Cryo(1).ToCostInit(),new DamageVariable(0,2)),
            new CharacterSimpleSkill(SkillCategory.E,new CostCreate().Cryo(3).ToCostInit(),(skill,me,c,args)=>me.AddPersistent(new Effect_Diona(1)),new DamageVariable(1,2)),
            new Q(),
        };
        private class Q : AbstractCardSkill
        {
            public override SkillCategory Category => SkillCategory.Q;

            public override CostInit Cost =>new CostCreate().Cryo(3).MP(3).ToCostInit();

            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
            {
                me.Enemy.Hurt(new(1, 1), this, () =>
                {
                    me.Heal(this, new HealVariable(2));
                    me.AddSummon(new Summon_Diona());
                });
            }
        }

        public override ElementCategory CharacterElement => ElementCategory.Cryo;

        public override WeaponCategory WeaponCategory => WeaponCategory.Bow;

        public override CharacterRegion CharacterRegion => CharacterRegion.MONDSTADT;

        public override string NameID => "diona";
    }
    public class Effect_Diona : AbstractPersistentShieldYellow
    {
        public override int MaxUseTimes { get; }
        public Effect_Diona(int maxusetimes)
        {
            MaxUseTimes = maxusetimes;
        }
    }
    public class Summon_Diona : AbstractCardSummon
    {
        public override int MaxUseTimes => 2;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundOver,(me,p,s,v)=>
                {
                    me.Enemy.Hurt(new(4,1),this,()=>
                    {
                        me.Heal(this,new HealVariable (2));
                        p.AvailableTimes--;
                    });
                }
            }
        };
        public override string NameID => "summon_diona";
    }

    public class Talent_Diona : AbstractCardEquipmentOverrideSkillTalent
    {
        public override string CharacterNameID => "diona";
        public override int Skill => 1;
        public override CostInit Cost => new CostCreate().Cryo(3).ToCostInit();
        public override void TalentTriggerAction(PlayerTeam me, Character c, int[] targetArgs) => me.Enemy.Hurt(new(4, 2), c.Card.Skills[1], () => me.AddPersistent(new Effect_Diona(2)));
    }
}
