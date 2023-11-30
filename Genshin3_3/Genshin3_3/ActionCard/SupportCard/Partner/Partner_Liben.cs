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
                int[] cost= new int[] { 0,0,0,0,0,0,0,0};
                var dices=me.GetDices();
                var selects=dices.Select((num,index)=>(num,element:index)).ToList();
                selects.RemoveAt(0);
                selects.Sort((b,a)=>10*(a.num-b.num)+a.element-b.element);
                int cnt=0;

                int need=3-p.AvailableTimes;
                for (int i = 0; i < need; i++)
                {
                    if (selects[i].num>0)
                    {
                         cost[selects[i].element]++;
                         cnt++;
                    }
                }
                if (cnt<need)
                {
                    cost[0]+=int.Min(need-cnt,dices[0]);
                    cnt+=cost[0];
                }
                me.CostDices(cost);
                p.AvailableTimes+=cnt;
            } }
        };
    }
}
