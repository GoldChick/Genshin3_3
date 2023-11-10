using TCGBase;
namespace Genshin3_3
{
    public class BestCompanion : AbstractCardEvent
    {
        public override string NameID => "event_bestcompanion";
        public override int[] Costs => new int[] {2};
        public override bool CostSame => false;

        public override void AfterUseAction(PlayerTeam me, int[] targetArgs)
        {
            me.AddDiceRange(0, 0);
        }
    }
}
