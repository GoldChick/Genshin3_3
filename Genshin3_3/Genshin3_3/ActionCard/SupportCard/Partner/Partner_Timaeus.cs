using TCGBase;
namespace Genshin3_3
{
    public class Partner_Timaeus : AbstractCardSupport
    {
        public override SupportTags SupportTag => SupportTags.Partner;
        public override CostInit Cost => new CostCreate().Same(2).ToCostInit();
        public override int MaxUseTimes => 2;
        public override void AfterUseAction(PlayerTeam me, int[] targetArgs)
        {
            //TODO:依据初始卡组定向检索
            base.AfterUseAction(me, targetArgs);
        }
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.UseDiceFromCard, (me,p,s,v)=>
            {
                if (me.TeamIndex==s.TeamID && s is UseDiceFromCardSender ss && ss.Card is AbstractCardArtifact artifact && v is CostVariable cv)
                {
                    var need=cv.DiceCost.Sum();
                    if (p.AvailableTimes>=need)
                    {
                        CostModifier mod=new(DiceModifierType.Same,need);
                        mod.Modifier(cv);
                        p.AvailableTimes-=need;
                    }
                }
            }
            },
            {SenderTag.RoundOver,(me,p,s,v)=>p.AvailableTimes++ }
        };
    }
}
