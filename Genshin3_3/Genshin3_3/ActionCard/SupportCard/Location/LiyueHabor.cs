using TCGBase;
namespace Genshin3_3
{
    public class LiyueHarbor : AbstractCardSupport
    {
        public override string NameID => "location_liyuehabor";
        public override SupportTags SupportTag => SupportTags.Place;
        public override CostInit Cost => new CostCreate().Same(2).ToCostInit();
        public override int MaxUseTimes => 2;
        public override bool CustomDesperated => false;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundOver, (me,p,s,v)=>{me.RollCard(2);p.AvailableTimes--; } }
        };
    }
}
