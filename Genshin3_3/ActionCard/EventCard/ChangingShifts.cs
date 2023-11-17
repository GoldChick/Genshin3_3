using TCGBase;


namespace Genshin3_3
{
    public class ChangingShifts : AbstractCardEventSingleEffect
    {
        public override string NameID => "event_changingshifts";
        public override int[] Costs => Array.Empty<int>();

        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.UseDiceFromSwitch,new PersistentDiceCostModifier<UseDiceFromSwitchSender>((me,p,s,v)=>true,0,1) }
        };
    }
}
