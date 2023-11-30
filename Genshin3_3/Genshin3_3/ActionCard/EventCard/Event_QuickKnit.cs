using TCGBase;
namespace Genshin3_3
{
    public class Event_QuickKnit : AbstractCardEvent
    {
        public override string NameID => "event_quickknit";
        public override CostInit Cost => new CostCreate().Same(1).ToCostInit();
        public override TargetDemand[] TargetDemands => new TargetDemand[]
        {
            new(TargetEnum.Summon_Me,(me,ts)=>true)
        };

        public override void AfterUseAction(PlayerTeam me, int[] targetArgs)
        {
            me.Summons[targetArgs[0]].AvailableTimes++;
        }
    }
}
