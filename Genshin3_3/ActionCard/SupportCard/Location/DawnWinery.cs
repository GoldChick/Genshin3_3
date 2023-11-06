using TCGBase;
namespace Genshin3_3
{
    public class DawnWinery : AbstractCardSupport, IDamageSource
    {
        public override string NameID => "location_dawnwinery";
        public override SupportTags SupportTag => SupportTags.Place;
        public override int[] Costs => new int[] { 2 };
        public override bool CostSame => true;
        public override int MaxUseTimes => 1;
        public override bool CustomDesperated => true;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.UseDiceFromSwitch, new PersistentDiceCostModifier<UseDiceFromSwitchSender>((me,p,s,v)=>p.AvailableTimes>0,0,1)},
            { SenderTag.RoundOver,(me,p,s,v)=>p.AvailableTimes=1 }
        };

        public DamageSource DamageSource => DamageSource.Addition;
    }
}
