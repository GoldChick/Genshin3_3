﻿using TCGBase;
 

namespace Genshin3_3
{
    public class GamblerEarrings : AbstractCardArtifact, ITargetSelector
    {
        public override string NameID => "artifact_gamblerearrings";

        public override int[] Costs => new int[] { 1 };

        public override AbstractCardPersistentArtifact Effect =>  new 赌徒_Effect();

        public class 赌徒_Effect : AbstractCardPersistentArtifact
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