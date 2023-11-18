using TCGBase;

namespace Genshin3_3
{
    public class Fischl : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleA(0,2,4),
            new CharacterSingleSummonE(4,1,new Summon_Fischl()),
            new 至夜幻现()
        };
        public override int MaxMP => 3;
        public override ElementCategory CharacterElement => ElementCategory.Electro;

        public override WeaponCategory WeaponCategory => WeaponCategory.Bow;

        public override CharacterRegion CharacterRegion => CharacterRegion.MONDSTADT;

        public override string NameID => "fischl";
        private class 至夜幻现 : AbstractCardSkill
        {
            public override SkillCategory Category => SkillCategory.Q;

            public override int[] Costs => new int[] { 0, 0, 0, 0, 3 };

            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
            {
                me.Enemy.MultiHurt(new DamageVariable[] { new(4, 4), new(-1, 2, 0, true) }, this);
            }
        }
    }
    public class Summon_Fischl : AbstractCardPersistentSummon
    {
        public Summon_Fischl(bool talent = false)
        {
            Variant = talent ? 1 : 0;
            TriggerDic = new()
            {
                { SenderTag.RoundOver,(me,p,s,v)=>{me.Enemy.Hurt(new(4,1),this); p.AvailableTimes--; } },
            };
            if (talent)
            {
                TriggerDic.Add(SenderTag.AfterUseSkill, (me, p, s, v) =>
                {
                    if (me.TeamIndex == s.TeamID && s is AfterUseSkillSender ss && me.Characters[ss.CharIndex].Card is Fischl && ss.Skill.Category == SkillCategory.A)
                    {
                        me.Enemy.Hurt(new(4, 2), this);
                        p.AvailableTimes--;
                    }
                });
            }
        }
        public override int MaxUseTimes => 2;

        public override PersistentTriggerDictionary TriggerDic { get; }

        public override string NameID => "summon_fischl";
    }

    public class Talent_Fischl : AbstractCardEquipmentOverrideSkillTalent
    {
        public override string CharacterNameID => "fischl";

        public override int Skill => 1;

        public override int[] Costs => new int[] { 0, 0, 0, 0, 3 };

        public override void TalentTriggerAction(PlayerTeam me, Character c, int[] targetArgs) => me.Enemy.Hurt(new(4, 1), c.Card.Skills[1], () => me.AddSummon(new Summon_Fischl(true)));
    }
}
