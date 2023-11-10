using TCGBase;
 

namespace Genshin3_3
{
    public class GuardianOath : AbstractCardEvent
    {
        public override string NameID => "event_guardianoath";

        public override int[] Costs => new int[] { 4};

        public override void AfterUseAction(PlayerTeam me, int[] targetArgs)
        {
            me.Effects.Copy().ForEach(p => p.Active = false);
            me.Enemy.Effects.Copy().ForEach(p => p.Active = false);
        }
    }
}
