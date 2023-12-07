using TCGBase;
namespace Genshin3_3
{
    public class NRE : AbstractCardSupport
    {
        public override SupportTags SupportTag => SupportTags.Item;
        public override CostInit Cost => new CostCreate().Same(1).ToCostInit();
        public override string NameID => "item_NRE";
        public override int MaxUseTimes => 1;
        public override void AfterUseAction(PlayerTeam me, int[] targetArgs)
        {
            base.AfterUseAction(me, targetArgs);
            me.RollCard(typeof(ICardFood));
        }
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            new PersistentPreset.RoundStepReset(),
            { SenderTag.AfterUseCard,(me,p,s,v)=>
            {
                if (s is AfterUseCardSender ucs && ucs.Card is ICardFood)
                {
                    me.RollCard(typeof(ICardFood));
                    p.AvailableTimes--;
                }
            }}
        };

    }
}
