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
            new CharacterSimpleA(0,2,1),
            new CharacterEffectE(1,2,new Effect_Diona(1),false),
            new Q(),
        };
        private class Q : AbstractCardSkill
        {
            public override SkillCategory Category => SkillCategory.Q;

            public override int[] Costs => new int[] { 0, 3 };

            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
            {
                me.Enemy.Hurt(new(1, 1), this, () =>
                {
                    me.Heal(this, new DamageVariable(0, 2));
                    me.AddSummon(new Summon_Diona());
                });
            }
        }

        public override ElementCategory CharacterElement => ElementCategory.Cryo;

        public override WeaponCategory WeaponCategory => WeaponCategory.Bow;

        public override CharacterRegion CharacterRegion => CharacterRegion.MONDSTADT;

        public override string NameID => "diona";
    }
    public class Effect_Diona : AbstractCardPersistent
    {
        public override int MaxUseTimes { get; }
        public Effect_Diona(int maxusetimes)
        {
            MaxUseTimes = maxusetimes;
        }
        public override PersistentTriggerDictionary TriggerDic => new()
            {
                {SenderTag.HurtDecrease, new PersistentYellowShield()}
            };
        public override string NameID => "effect_diona";
    }
    public class Summon_Diona : AbstractCardPersistentSummon
    {
        public override int MaxUseTimes => 2;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundOver,(me,p,s,v)=>
                {
                    me.Enemy.Hurt(new(4,1),this,()=>me.Heal(this,new DamageVariable(0,2)));
                    p.AvailableTimes--;
                }
            }
        };
        public override string NameID => "summon_diona";
    }

    public class Talent_Diona : AbstractCardEquipmentOverrideSkillTalent
    {
        public override string CharacterNameID => "diona";
        public override int Skill => 1;
        public override int[] Costs => new int[] { 0, 3 };
        public override void TalentTriggerAction(PlayerTeam me, Character c, int[] targetArgs) => me.Enemy.Hurt(new(4, 2), c.Card.Skills[1], () => me.AddPersistent(new Effect_Diona(2)));
    }
}
