using TCGBase;
namespace Genshin3_3
{
    public class Partner_Timmie : AbstractCardSupport
    {
        public override string NameID => "partner_timmie";
        public override SupportTags SupportTag => SupportTags.Partner;
        public override CostInit Cost => new();
        public override int MaxUseTimes => 0;
        public override bool CustomDesperated => true;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundStart, (me,p,s,v)=>p.Data=0},
            { SenderTag.AfterAnyAction,(me,p,s,v)=>
            {
                if (p.Data==null || p.Data.Equals(0))
                {
                    p.Data=1;
                    p.AvailableTimes++;
                    if (p.AvailableTimes>=3)
                    {
                        p.Active=false;
                        me.RollCard(1);
                        me.AddDiceRange(0);
                    }
                }
            } }
        };
    }
}
