using TCGBase;
 

namespace Genshin3_3
{
    public class 赌徒 : AbstractCardArtifact, ITargetSelector
    {
        public override string NameID => "赌徒的耳环";

        public override int[] Costs => new int[] { 1 };

        public TargetEnum[] TargetEnums => new TargetEnum[] { TargetEnum.Character_Me };

        public override AbstractCardPersistentEffect Effect =>  new 赌徒_Effect();

        public class 赌徒_Effect : AbstractCardPersistentEffect
        {
            public override int MaxUseTimes => 3;

            public override PersistentTriggerDictionary TriggerDic => new()
            {
                { SenderTag.Die,(me,p,s,v)=>
                {
                    if (me.TeamIndex!=s.TeamID && me.CurrCharacter==p.PersistentRegion && p.AvailableTimes>0)
                    {
                        me.AddDiceRange(0,0);
                        p.AvailableTimes--;
	                }
                }
                }
            };
        }
    }
}
