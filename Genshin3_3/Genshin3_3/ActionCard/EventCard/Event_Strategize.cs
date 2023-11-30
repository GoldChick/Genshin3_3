using TCGBase;
namespace Genshin3_3
{
    public class Event_Strategize : AbstractCardEvent
    {
        public override string NameID => "event_strategize";
        public override CostInit Cost => new CostCreate().Same(1).ToCostInit();

        public override void AfterUseAction(PlayerTeam me, int[] targetArgs)
        {
            me.RollCard(2);
        }
    }
}
