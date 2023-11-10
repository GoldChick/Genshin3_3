using TCGBase;

namespace Genshin3_3
{
    public class Fischl : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleA(0,2,4),
            new CharacterSingleSummonE(4,1,new SimpleSummon("genshin3_3","summon_fischl",4,1,2)),
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
    public class Talent_Fischl : AbstractCardEquipmentFightActionTalent
    {
        public override string CharacterNameID => "fischl";

        public override int Skill => 1;

        public override CardPersistentTalent Effect => new E();

        public override int[] Costs => new int[] { 0, 0, 0, 0, 3 };
        public override void AfterUseAction(PlayerTeam me, int[] targetArgs)
        {
            me.Summons.TryRemove("summon_fischl");
            base.AfterUseAction(me, targetArgs);
        }
        private class E : CardPersistentTalent
        {
            public override int Skill => 1;
            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
            {
                me.Enemy.Hurt(new(4, 1), c.Card.Skills[1], () => me.TryAddSummon(new 特殊奥兹(c.Index)));
            }
            private class 特殊奥兹 : AbstractCardPersistentSummon
            {
                public override string TextureNameSpace => "genshin3_3";
                public override string TextureNameID => "summon_fischl";
                private int _fischl;
                public 特殊奥兹(int fischl)
                {
                    _fischl = fischl;
                }
                public override int MaxUseTimes => 2;

                public override PersistentTriggerDictionary TriggerDic => new()
                {
                    { SenderTag.RoundOver,(me,p,s,v)=>{me.Enemy.Hurt(new(4,1),this); p.AvailableTimes--; } },
                    { SenderTag.AfterUseSkill,(me,p,s,v)=>
                        {
                            if (me.TeamIndex==s.TeamID && s is AfterUseSkillSender ss && ss.CharIndex==_fischl && ss.Skill.Category==SkillCategory.A)
                            {
                                me.Enemy.Hurt(new(4,2),this);
                                p.AvailableTimes--;
                            }
                        }
                    }
                };
            }
        }
    }
}
