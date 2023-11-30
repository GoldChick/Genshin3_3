using TCGBase;
namespace Genshin3_3
{
    public class KnightsOfFavoniusLibrary : AbstractCardSupport
    {
        public override string NameID => "location_knightsoffavoniuslibrary";
        public override SupportTags SupportTag => SupportTags.Place;
        public override CostInit Cost => new CostCreate().Same(1).ToCostInit();
        public override int MaxUseTimes => 1;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.BeforeRerollDice, (me,p,s,v)=>
                {
                    if (me.TeamIndex==s.TeamID && v is DiceRollingVariable drv)
                    {
                         drv.RollingTimes++;
	                }
                }
            }
        };
    }
}
