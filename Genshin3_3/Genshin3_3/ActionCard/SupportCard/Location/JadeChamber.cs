using TCGBase;
namespace Genshin3_3
{
    public class JadeChamber : AbstractCardSupport
    {
        public override string NameID => "location_jadechamber";
        public override SupportTags SupportTag => SupportTags.Place;
        public override CostInit Cost => new ();
        public override int MaxUseTimes => 1;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.BeforeRerollDice, (me,p,s,v)=>
                {
                    if (me.TeamIndex==s.TeamID && v is DiceRollingVariable drv)
                    {
                        int e=(int)me.Characters[me.CurrCharacter].Card.CharacterElement;
                        drv.InitialDices.Add(e);
                        drv.InitialDices.Add(e);
                        drv.DiceNum-=2;
                        //TODO: unchecked
                    }
                }
            }
        };
    }
}
