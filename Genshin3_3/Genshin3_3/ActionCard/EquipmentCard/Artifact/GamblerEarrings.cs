using TCGBase;


namespace Genshin3_3
{
    public class GamblerEarrings : AbstractCardArtifact
    {
        public override string NameID => "artifact_gamblerearrings";

        public override CostInit Cost => new CostCreate().Same(1).ToCostInit();

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
