using TCGBase;
 

namespace Genshin3_3
{
    public class 赌徒 : AbstractCardArtifact, ITargetSelector
    {
        public override string NameID => "赌徒的耳环";

        public override int[] Costs => new int[] { 1 };

        public override bool CostSame => false;

        public TargetEnum[] TargetEnums => new TargetEnum[] { TargetEnum.Character_Me };

        public override AbstractCardPersistentEffect Effect =>  new 赌徒_Effect();

        public class 赌徒_Effect : AbstractCardPersistentEffect
        {
            public override int MaxUseTimes => 3;

            public override PersistentTriggerDictionary TriggerDic => throw new NotImplementedException();
        }
    }
}
