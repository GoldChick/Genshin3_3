using TCGBase;
namespace Genshin3_3
{
    public class DawnWinery : AbstractCardSupport
    {
        public override string NameID => "location_dawnwinery";
        public override SupportTags SupportTag => SupportTags.Place;
        public override CostInit Cost => new CostCreate().Same(2).ToCostInit();
        public override int MaxUseTimes => 1;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.UseDiceFromSwitch, new PersistentDiceCostModifier<UseDiceFromSwitchSender>((me,p,s,v)=>me.TeamIndex==s.TeamID&&p.AvailableTimes>0,0,1)},
            { SenderTag.RoundStep,(me,p,s,v)=>p.AvailableTimes=1 }
        };
    }
}
