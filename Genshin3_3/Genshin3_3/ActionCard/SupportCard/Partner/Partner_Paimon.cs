using TCGBase;
namespace Genshin3_3
{
    public class Partner_Paimon : AbstractCardSupport
    {
        public override string NameID => "partner_paimon";
        public override SupportTags SupportTag => SupportTags.Partner;
        public override CostInit Cost => new CostCreate().Same(3).ToCostInit();
        public override int MaxUseTimes => 2;
        public override bool CustomDesperated => false;
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
