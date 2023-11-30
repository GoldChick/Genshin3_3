using Genshin3_5;
using TCGBase;

namespace Genshin3_7
{
    public class Artifact_Helm_Big : AbstractCardArtifact
    {
        public override int MaxUseTimes => 1;
        public override CostInit Cost => new CostCreate().Same(3).ToCostInit();

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundStart,(me,p,s,v)=>
            {
                p.AvailableTimes=MaxUseTimes;
                me.AddPersistent(new Effect_GeneralAncientHelm(),p.PersistentRegion);
            } 
            },
            {SenderTag.AfterHurt,(me,p,s,v)=>
            {
                if (p.AvailableTimes>0&&s is HurtSender hs && me.TeamIndex==s.TeamID && hs.TargetIndex==p.PersistentRegion && me.CurrCharacter==p.PersistentRegion && me.Characters[p.PersistentRegion].Alive)
                {
                    p.AvailableTimes--;
                    me.AddDiceRange(me.Characters[hs.TargetIndex].Element);
	            }
            }
            }
        };
    }
}
