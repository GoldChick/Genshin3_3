using TCGBase;
namespace Genshin3_3
{
    public class Event_BestCompanion : AbstractCardEvent
    {
        public override string NameID => "event_bestcompanion";
        public override CostInit Cost => new CostCreate().Void(2).ToCostInit();

        public override void AfterUseAction(PlayerTeam me, int[] targetArgs)
        {
            me.AddDiceRange(0, 0);
        }
    }
}
