using TCGBase;


namespace Genshin3_3
{
    public class Event_LeaveItToMe : AbstractCardEventSingleEffect
    {
        public override string NameID => "event_leaveittome";
        public override CostInit Cost => new();

        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.AfterSwitch, (me,p,s,v)=>
            {
                if (me.TeamIndex==s.TeamID && v is FastActionVariable fav)
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
