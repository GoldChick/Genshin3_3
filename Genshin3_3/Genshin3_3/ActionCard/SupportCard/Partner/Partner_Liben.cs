using TCGBase;
namespace Genshin3_3
{
    public class Partner_Liben : AbstractCardSupport
    {
        public override string NameID => "partner_liben";
        public override SupportTags SupportTag => SupportTags.Partner;
        public override CostInit Cost => new();
        public override int MaxUseTimes => 0;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundStart, (me,p,s,v)=>
            {
               if (p.AvailableTimes>=3)
                {
                    p.Active=false;
                    me.RollCard(2);
                    me.AddDiceRange(0,0);
                }
            }
            },
            {SenderTag.RoundOver,(me,p,s,v)=>
            {
                var sorted_dices=me.GetSortedDices();
                int cnt=0;
                for (int i = 0; i < 3-p.AvailableTimes; i++)
                {
                    if (sorted_dices[i].count>0)
                    {
                        me.TryRemoveDice(sorted_dices[i].element);
                        cnt++;
                    }
                }
                p.AvailableTimes+=cnt;
            } }
        };
    }
}
