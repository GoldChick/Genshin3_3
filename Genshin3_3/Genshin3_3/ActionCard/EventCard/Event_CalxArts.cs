using TCGBase;


namespace Genshin3_3
{
    public class Event_CalxArts : AbstractCardEvent
    {
        public override CostInit Cost => new CostCreate().Same(1).ToCostInit();


        public override void AfterUseAction(PlayerTeam me, int[] targetArgs)
        {
            var c_next = me.Characters[(me.CurrCharacter + 1) % me.CurrCharacter];
            var c_last = me.Characters[(me.CurrCharacter - 1) % me.CurrCharacter];
            int cnt = 0;
            if (c_next.MP>0)
            {
                cnt++;
                c_next.MP--;
            }
            if (c_last.MP>0)
            {
                cnt++;
                c_last.MP--;
            }
            me.Characters[me.CurrCharacter].MP+=cnt;
        }

        public override bool CanBeUsed(PlayerTeam me, int[] targetArgs)
        {
            var c = me.Characters[me.CurrCharacter];
            return c.MP < c.Card.MaxMP;
        }
    }
}
