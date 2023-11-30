using TCGBase;

namespace Genshin3_3
{
    public class Partner_ChefMao : AbstractCardSupport
    {
        public override SupportTags SupportTag => SupportTags.Partner;

        public override int MaxUseTimes => 1;
        public override CostInit Cost => new CostCreate().Same(1).ToCostInit();

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundStep,(me,p,s,v)=>p.AvailableTimes=MaxUseTimes},
            { SenderTag.AfterUseCard,(me,p,s,v)=>
            {
                me.AddDiceRange(1+me.Random.Next(7));
                if (p.Data==null)
                {
                    me.RollCard(typeof(ICardFood));
                    p.Data=1;
                }
            } }
        };
        public override void AfterUseAction(PlayerTeam me, int[] targetArgs)
        {
            base.AfterUseAction(me, targetArgs);
            me.RollCard(typeof(ICardFood));
        }
    }
}
