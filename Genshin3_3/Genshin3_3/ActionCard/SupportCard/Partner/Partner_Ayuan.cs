using TCGBase;

namespace Genshin3_3
{
    public class Partner_Ayuan : AbstractCardSupport
    {
        public override CostInit Cost => new CostCreate().Same(2).ToCostInit();
        public override SupportTags SupportTag => SupportTags.Partner;

        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            new PersistentPreset.RoundStepReset(),
            { SenderTag.UseDiceFromCard,new PersistentDiceCostModifier<UseDiceFromCardSender>(
                (me,p,s,v)=>me.TeamIndex==s.TeamID && s.Card is AbstractCardSupport sp && sp.SupportTag==SupportTags.Place,
                0,2)
            }
        };
    }
}
