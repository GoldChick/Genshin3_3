using TCGBase;

namespace Genshin3_7
{
    internal class Partner_Dunyarzad : AbstractCardSupport
    {
        public override SupportTags SupportTag => SupportTags.Partner;
        public override CostInit Cost => new CostCreate().Same(1).ToCostInit();
        public override int MaxUseTimes => 1;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundStep,(me,p,s,v)=>p.AvailableTimes=MaxUseTimes},
            { SenderTag.UseDiceFromCard,new PersistentDiceCostModifier<UseDiceFromCardSender>(
                (me,p,s,v)=>me.TeamIndex==s.TeamID && s.Card is AbstractCardSupport sp && sp.SupportTag==SupportTags.Partner,
                0,1)},
            { SenderTag.AfterUseCard,(me,p,s,v)=>
            {
                if (p.Data==null && s is AfterUseCardSender ss && ss.Card is AbstractCardSupport sp && sp.SupportTag==SupportTags.Partner)
                {
                    me.RollCard(typeof(AbstractCardSupport));
                    p.Data=114;
	            }
            } 
            }
        };
    }
}
