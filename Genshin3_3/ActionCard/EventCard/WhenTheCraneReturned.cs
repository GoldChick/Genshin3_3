using TCGBase;


namespace Genshin3_3
{
    public class WhenTheCraneReturned : AbstractCardEventSingleEffect
    {
        public override string NameID => "event_whenthecranereturned";

        public override int[] Costs => new int[] { 1 };

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
