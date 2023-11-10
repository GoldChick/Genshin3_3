using TCGBase;
namespace Genshin3_3
{
    public class Strategize : AbstractCardEvent
    {
        public override string NameID => "event_strategize";
        public override int[] Costs => new int[] { 1};

        public override void AfterUseAction(PlayerTeam me, int[] targetArgs)
        {
            me.RollCard(2);
        }
    }
}
