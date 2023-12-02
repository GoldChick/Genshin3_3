using TCGBase;

namespace Genshin3_7
{
    public class Location_ChinjuForest : AbstractCardSupport
    {
        public override SupportTags SupportTag => SupportTags.Place;

        public override CostInit Cost => new CostCreate().Same(1).ToCostInit();
        public override int MaxUseTimes => 3;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            {
                SenderTag.RoundStart,(me,p,s,v)=>
                {
                    if (me.Game.CurrTeam!=me.TeamIndex)
                    {
                        me.AddDiceRange((int)me.Characters[me.CurrCharacter].Card.CharacterElement);
                    }
                }
            }
        };

    }
}
