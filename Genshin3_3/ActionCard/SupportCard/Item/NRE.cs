using TCGBase;
namespace Genshin3_3
{
    public class NRE : AbstractCardSupport
    {
        public override SupportTags SupportTag => SupportTags.Item;
        public override int[] Costs => new int[] { 2 };
        public override bool CostSame => false;
        public override string NameID => "item_NRE";
        public override int MaxUseTimes => 1;
        public override bool CustomDesperated => true;
        public override void AfterUseAction(PlayerTeam me, int[]? targetArgs = null)
        {
            base.AfterUseAction(me, targetArgs);
            me.RollCard(typeof(AbstractFood));
        }
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundOver,(me,p,s,v)=>p.AvailableTimes=1},
            { SenderTag.AfterUseCard,(me,p,s,v)=>
            {
                if (s is UseCardSender ucs && ucs.Card is AbstractFood)
                {
                    me.RollCard(typeof(AbstractFood));
                    p.AvailableTimes--;
                }
            }}
        };

    }
}
