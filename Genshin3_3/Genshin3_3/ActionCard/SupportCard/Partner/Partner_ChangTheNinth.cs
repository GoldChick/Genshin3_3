using TCGBase;
namespace Genshin3_3
{
    public class Partner_ChangTheNinth : AbstractCardSupport
    {
        public override SupportTags SupportTag => SupportTags.Partner;
        public override CostInit Cost => new();
        public override string NameID => "partner_changtheninth";
        public override int MaxUseTimes => 0;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.BeforeUseSkill,(me,p,s,v)=> p.Data=1 },
            { SenderTag.AfterHurt,(me,p,s,v)=>
            {
                if (p.Data!=null && p.Data.Equals(1) && s is NoDamageHurtSender hs && (hs.Element<=0||hs.Reaction!=ReactionTags.None))
                {
                    p.Data=2;
                }
            }},
            { SenderTag.AfterUseSkill,(me,p,s,v)=>
            {
                if (p.Data!=null && p.Data.Equals(2))
                {
                    p.AvailableTimes++;
                    if (p.AvailableTimes==3)
                    {
                        p.Active=false;
                        me.RollCard(2);
                    }
                }
                p.Data=0;
            }}
        };
    }
}
