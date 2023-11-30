using TCGBase;


namespace Genshin3_3
{
    public class Event_ChangingShifts : AbstractCardEventSingleEffect
    {
        public override string NameID => "event_changingshifts";
        public override CostInit Cost => new CostInit();

        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.UseDiceFromSwitch,new PersistentDiceCostModifier<UseDiceFromSwitchSender>((me,p,s,v)=>me.TeamIndex==s.TeamID,0,1) }
        };
    }
}
