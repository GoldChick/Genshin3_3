using TCGBase;

namespace Genshin3_3
{
    public class NingGuang : AbstractCardCharacter
    {
        public override int MaxMP => 3;
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
         new CharacterSimpleA(5,1),
         new CharacterEffectE(5,2,new 璇玑屏(),false),
         new 天权崩玉()
        };

        public override ElementCategory CharacterElement => ElementCategory.Geo;

        public override WeaponCategory WeaponCategory => WeaponCategory.Catalyst;

        public override CharacterRegion CharacterRegion => CharacterRegion.LIYUE;

        public override string NameID => "ningguang";
        public class 璇玑屏 : AbstractCardPersistentEffect
        {
            public override string TextureNameID => PersistentTextures.Shield_Purple;
            public override int MaxUseTimes => 2;

            public override PersistentTriggerDictionary TriggerDic => new()
            {
                {SenderTag.HurtDecrease,new PersistentPurpleShield(1,2) }
            };
        }
        private class 天权崩玉 : AbstractCardSkill
        {
            public override SkillCategory Category => SkillCategory.Q;

            public override int[] Costs => new int[] { 0, 0, 0, 0, 0, 3 };

            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
            {
                me.Enemy.Hurt(new DamageVariable(5, me.Effects.Contains(typeof(璇玑屏)) ? 8 : 6, 0), this);
            }
        }
    }
    public class Talent_Ningguang : AbstractCardTalent
    {
        public override CardPersistentTalent Effect => new 储之千日_用之一刻_Effect();

        public override string CharacterNameID => "ningguang";

        public override int Skill => 1;

        public override int[] Costs => new int[] { 0, 0, 0, 0, 0, 4 };

        private class 储之千日_用之一刻_Effect : CardPersistentTalent
        {
            public override PersistentTriggerDictionary TriggerDic => new()
                {
                    { SenderTag.DamageIncrease,(me,p,s,v)=>
                        {
                            if (me.TeamIndex==s.TeamID && me.Effects.Contains(typeof(NingGuang.璇玑屏)) &&  v is DamageVariable dv && dv.Element==5)
                            {
                                dv.Damage++;
                            }
                        }
                    }
                };
        }
    }

}
