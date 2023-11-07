using TCGBase;

namespace Genshin3_3
{
    public class Collei : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleA(0,2,6),
            new CharacterSimpleE(6,3),
            new CharacterSingleSummonQ(6,2,new SimpleSummon("genshin3_3","summon_collei",6,2,2))
        };

        public override ElementCategory CharacterElement => ElementCategory.Dendro;

        public override WeaponCategory WeaponCategory => WeaponCategory.Bow;

        public override CharacterRegion CharacterRegion => CharacterRegion.SUMERU;

        public override string NameID => "collei";
    }

    public class 飞叶迴斜 : AbstractCardTalent
    {
        public override CardPersistentTalent Effect => null;

        public override string CharacterNameID => "collei";

        public override int Skill => 1;

        public override int[] Costs => new int[] { 0, 0, 0, 0, 0, 0, 4 };

        //private class 飞叶迴斜_Effect : CardPersistentTalent
        //{
        //    private bool _prepare;
        //    private bool _trigger;
        //    public override int MaxUseTimes => 1;
        //    public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
        //    {
        //        base.AfterUseAction(me, c, targetArgs);
        //    }
        //    public override PersistentTriggerDictionary TriggerDic => new()
        //    {
        //        {SenderTag.BeforeUseSkill,(me,p,s,v)=> _prepare=true},
        //        {SenderTag.AfterHurt,(me,p,s,v)=>
        //        {
        //        }
        //        },
        //        {SenderTag.AfterUseSkill,(me,p,s,v)=>
        //        {
        //            _prepare=false;
        //        }
        //        }
        //    };
        //}
        private class 飞叶 : AbstractCardPersistentEffect
        {
            public override int MaxUseTimes => 1;

            public override PersistentTriggerDictionary TriggerDic => throw new NotImplementedException();
        }
    }
}
