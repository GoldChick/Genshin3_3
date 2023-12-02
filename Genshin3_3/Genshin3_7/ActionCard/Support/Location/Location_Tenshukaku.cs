using TCGBase;

namespace Genshin3_7
{
    public class Location_Tenshukaku : AbstractCardSupport
    {
        public override SupportTags SupportTag => SupportTags.Place;

        public override CostInit Cost => new CostCreate().Same(2).ToCostInit();
        public override int MaxUseTimes => 0;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            {
                SenderTag.RoundStart,(me,p,s,v)=>
                {
                    if (me.GetDices().Select((d,index)=>index>0? (d>0?1:0):d).Sum()>=5)
                    {
                        me.AddDiceRange(0);
                    }
                }
            }
        };

    }
}
