using TCGBase;
namespace Genshin3_3
{
    public class QuickKnit : AbstractCardEvent,ITargetSelector
    {
        public override string NameID => "event_quickknit";
        public override int[] Costs => new int[] { 1};

        public TargetEnum[] TargetEnums => new TargetEnum[] { TargetEnum.Summon_Me};

        public override void AfterUseAction(PlayerTeam me, int[] targetArgs)
        {
            me.Summons[targetArgs[0]].AvailableTimes++;
        }
    }
}
