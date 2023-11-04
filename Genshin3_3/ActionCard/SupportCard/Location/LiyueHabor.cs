using TCGBase;
namespace Genshin3_3
{
    public class LiyueHarbor : AbstractCardSupport, IDamageSource
    {
        public override string NameID => "location_liyuehabor";
        public override SupportTags SupportTag => SupportTags.Place;
        public override int[] Costs => new int[] { 2 };
        public override bool CostSame => true;
        public override int MaxUseTimes => 2;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundOver, (me,p,s,v)=>{me.RollCard(2);p.AvailableTimes--; } }
        };
        public DamageSource DamageSource => DamageSource.Addition;
    }
}
