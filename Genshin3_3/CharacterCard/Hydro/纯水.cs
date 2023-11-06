using TCGBase;

namespace Genshin3_3
{
    public class 纯水 : AbstractCardCharacter
    {
        public override int MaxMP => 3;
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleA(1,1),
            new 召唤(1),
            new 召唤(2),
            new 潮涌与激流(),
        };
        public override ElementCategory CharacterElement => ElementCategory.Hydro;
        public override WeaponCategory WeaponCategory => WeaponCategory.Other;
        public override CharacterRegion CharacterRegion => CharacterRegion.None;
        public override CharacterCategory CharacterCategory => CharacterCategory.Mob;

        public override string NameID => "hydro";
        private class 召唤 : AbstractCardSkill, IMultiPersistentProvider<AbstractCardPersistentSummon>
        {
            public int PersistentNum { get; }
            public override int[] Costs { get; }
            public 召唤(int num)
            {
                PersistentNum = num;
                Costs = new int[] { 0, 0, 1 + PersistentNum * 2 };
            }
            public override SkillCategory Category => SkillCategory.E;
            public AbstractCardPersistentSummon[] PersistentPool => new AbstractCardPersistentSummon[]
            {
                new SimpleSummon("genshin3_3","summon_bird",2,1,3),
                new SimpleSummon("genshin3_3","summon_squirrel",2,2,2),
                new Frog()
            };
            public override void AfterUseAction(PlayerTeam me, Character c, int[]? targetArgs = null)
            {
            }
            private class Frog : AbstractCardPersistentSummon
            {
                public override string TextureNameID => "summon_frog";
                public override string TextureNameSpace => "genshin3_3";
                public override int MaxUseTimes => 2;
                public override bool CustomDesperated => true;
                public override PersistentTriggerDictionary TriggerDic => new()
                {
                    { SenderTag.HurtDecrease,new PersistentPurpleShield(1,1)},
                    { SenderTag.RoundOver,(me,p,s,v)=>
                    {
                        if (p.AvailableTimes==0)
                        {
                            me.Enemy.Hurt(new(2,2),this);
                            p.Active=false;
                        }
                    }
                    }
                };
            }
        }
        private class 潮涌与激流 : AbstractCardSkill
        {
            public override SkillCategory Category => SkillCategory.Q;

            public override int[] Costs => new int[] { 0, 0, 3 };

            public override void AfterUseAction(PlayerTeam me, Character c, int[]? targetArgs = null)
            {
                me.Enemy.Hurt(new(2, 4 + me.Summons.Count), this);
            }
        }
    }
    public class Talent_纯水 : AbstractCardTalent
    {
        public override CardPersistentTalent Effect => new Talent_E();

        public override string CharacterNameID => "hydro";

        public override int Skill => 3;

        public override int[] Costs => new int[] { 0,0,4};

        public override string NameID => "talent_hydro";
        private class Talent_E: CardPersistentTalent
        {
            public override int Skill => 3;
            public override void AfterUseAction(PlayerTeam me, Character c, int[]? targetArgs = null)
            {
                base.AfterUseAction(me, c, targetArgs);
                me.Summons.Copy().ForEach(p => p.AvailableTimes++);
            }
        }
    }

}
