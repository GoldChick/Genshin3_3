using TCGBase;

namespace Genshin3_3
{
    public class XiangLing : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
        new CharacterSimpleA(0,2,3),
        new CharacterSingleSummonE(new SimpleSummon("genshin3_3","summon_xiangling",4,2,2),3),
        new CharacterEffectQ(3,3,new 火轮(),false,3,4)
        };

        public override string NameID => "xiangling";

        public override ElementCategory CharacterElement => ElementCategory.Pyro;

        public override WeaponCategory WeaponCategory => WeaponCategory.Longweapon;

        public override CharacterRegion CharacterRegion => CharacterRegion.LIYUE;
        private class 火轮 : AbstractCardPersistentEffect
        {
            public override int MaxUseTimes => 2;
            public override PersistentTriggerDictionary TriggerDic => new()
            {
                { SenderTag.AfterUseSkill.ToString(),(me, p, s, v) =>
                    {
                        //自己不触发火轮
                        if(s is UseSkillSender ski && (me.Characters[ski.CharIndex].Card is not XiangLing || ski.Skill.Category!=SkillCategory.Q))
                        {
                            me.Enemy.Hurt(new DamageVariable(3, 2, 0), this);
                            p.AvailableTimes --;
                        }
                    }
                }
            };
            public override string TextureNameSpace => "genshin3_3";
            public override string TextureNameID => "effect_xiangling";
        }
    }
    public class Talent_XiangLing : AbstractCardTalent
    {
        public override string CharacterNameID => "xiangling";

        public override int Skill => 1;

        public override int[] Costs => new int[] { 0, 0, 0, 4 };

        public override string NameID => "talent_xiangling";

        public override CardPersistentTalent Effect => throw new NotImplementedException();
    }
}
