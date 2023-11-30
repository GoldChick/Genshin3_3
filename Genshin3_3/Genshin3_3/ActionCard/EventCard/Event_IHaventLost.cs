using System;
using TCGBase;


namespace Genshin3_3
{
    public class Event_IHaventLost : AbstractCardEvent
    {
        public override CostInit Cost => new();
        public override void AfterUseAction(PlayerTeam me, int[] targetArgs)
        {
            me.Characters[me.CurrCharacter].MP++;
            me.AddDiceRange(0);
        }
        //TODO:回合限制一次
        public override bool CanBeUsed(PlayerTeam me, int[] targetArgs) => me.Game.Records.Last().Any(r => r is DieRecord dr && dr.TeamID == me.TeamIndex);
    }
}
