using TCGBase;
 

namespace Genshin3_3
{
    public class Event_GuardianOath : AbstractCardEvent
    {
        public override string NameID => "event_guardianoath";

        public override CostInit Cost => new CostCreate().Same(4).ToCostInit();

        public override void AfterUseAction(PlayerTeam me, int[] targetArgs)
        {
            me.Effects.Copy().ForEach(p => p.Active = false);
            me.Enemy.Effects.Copy().ForEach(p => p.Active = false);
        }
    }
}
