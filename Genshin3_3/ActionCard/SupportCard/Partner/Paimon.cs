using TCGBase;
namespace Genshin3_3
{
    public class Paimon : AbstractCardSupport
    {
        public override string NameID => "partner_paimon";
        public override SupportTags SupportTag => SupportTags.Partner;
        public override int[] Costs => new int[] { 3 };
        public override int MaxUseTimes => 2;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundStart, (me,p,s,v)=>
            {
                p.AvailableTimes--;
                me.AddDiceRange(0,0);
            }
            }
        };
    }
}
