using TCGBase;


namespace Genshin3_3
{
    public class Event_WhenTheCraneReturned : AbstractCardEventSingleEffect
    {
        public override string NameID => "event_whenthecranereturned";

        public override CostInit Cost => new CostCreate().Same(1).ToCostInit();

        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.AfterUseSkill, (me,p,s,v)=>
            {
                if (me.TeamIndex==s.TeamID)
                {
                    p.AvailableTimes--;
                    me.SwitchToNext();
                }
            }
            }
        };
    }
}
