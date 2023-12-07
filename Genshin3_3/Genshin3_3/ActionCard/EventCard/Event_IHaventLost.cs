using System;
using TCGBase;


namespace Genshin3_3
{
    public class Event_IHaventLost : AbstractCardEventSingleEffect
    {
        public override CostInit Cost => new();

        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            new PersistentPreset.RoundStepDecrease()
        };

        public override void AfterUseAction(PlayerTeam me, int[] targetArgs)
        {
            me.Characters[me.CurrCharacter].MP++;
            me.AddDiceRange(0);
            base.AfterUseAction(me, targetArgs);
        }
        public override bool CanBeUsed(PlayerTeam me, int[] targetArgs) => me.Game.Records.Last().Any(r => r is DieRecord dr && dr.TeamID == me.TeamIndex);
    }
}
