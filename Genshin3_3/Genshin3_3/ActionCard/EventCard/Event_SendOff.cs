using TCGBase;
namespace Genshin3_3
{
    public class Event_SendOff : AbstractCardEvent
    {
        public override string NameID => "event_sendoff";
        public override CostInit Cost => new CostCreate().Same(2).ToCostInit();
        public override TargetDemand[] TargetDemands => new TargetDemand[]
        {
            new(TargetEnum.Summon_Enemy,(me,ts)=>true)
        };

        public override void AfterUseAction(PlayerTeam me, int[] targetArgs)
        {
            me.Enemy.Summons[targetArgs[0]].AvailableTimes -= 2;
        }
    }
}
