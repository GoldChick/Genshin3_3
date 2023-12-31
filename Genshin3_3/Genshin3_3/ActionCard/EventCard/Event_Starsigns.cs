﻿using System;
using TCGBase;
 

namespace Genshin3_3
{
    public class Event_Starsigns : AbstractCardEvent
    {
        public override string NameID => "event_starsigns";

        public override CostInit Cost => new CostCreate().Void(2).ToCostInit();


        public override void AfterUseAction(PlayerTeam me, int[] targetArgs)
        {
            me.Characters[me.CurrCharacter].MP++;
        }

        public override bool CanBeUsed(PlayerTeam me, int[] targetArgs)
        {
            var c = me.Characters[me.CurrCharacter];
            return c.MP < c.Card.MaxMP;
        }

    }
}
