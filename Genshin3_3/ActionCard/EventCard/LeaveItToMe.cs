﻿using TCGBase;
 

namespace Genshin3_3
{
    public class LeaveItToMe : AbstractCardEvent
    {
        public override string NameID => "leaveittome";

        public override bool CostSame => false;

        public override int[] Costs => Array.Empty<int>();


        public override void AfterUseAction(PlayerTeam me, int[] targetArgs)
        {
            me.AddPersistent(new LeaveItToMeEffect());
        }
        public class LeaveItToMeEffect : AbstractCardPersistentEffect
        {
            public override int MaxUseTimes => 1;

            public override string TextureNameID => PersistentTextures.Buff;

            public override PersistentTriggerDictionary TriggerDic => new() { 
                { SenderTag.AfterSwitch, (me,p,s,v)=>
                {
                    if (v is FastActionVariable fav)
                    {
                        if (!fav.Fast)
                        {
                            fav.Fast = true;
                            p.AvailableTimes--;
                        }
                    }
                }
                }
            };
        }
    }
}
