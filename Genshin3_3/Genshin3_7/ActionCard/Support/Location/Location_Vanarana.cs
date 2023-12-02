using TCGBase;

namespace Genshin3_7
{
    public class Location_Vanarana : AbstractCardSupport
    {
        public override SupportTags SupportTag => SupportTags.Place;

        public override CostInit Cost => new();
        public override int MaxUseTimes => 0;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundStart,(me,p,s,v)=>
            {
                if (p.Data is int data)
                {
                    me.AddDiceRange(data/10-1,data%10-1);
                }
                p.Data=null;
                p.AvailableTimes=0;
            }
            },
            { SenderTag.RoundOver,(me,p,s,v)=>
            {
                var sorted_dice=me.GetSortedDices();
                int cnt=0;
                for (int i = 0; i < 8; i++)
                {
                    if (cnt==1 && p.Data is int data)
                    {
                        if (sorted_dice[i].count>0)
                        {
                            p.Data=data+10*(sorted_dice[i].element+1);
                            me.TryRemoveDice(sorted_dice[i].element);
                            cnt++;
                            break;
                        }
                    }else if(sorted_dice[i].count>0)
                    {
                        p.Data=1+sorted_dice[i].element;
                        me.TryRemoveDice(sorted_dice[i].element);
                        cnt++;
                        if (sorted_dice[i].count>1)
                        {
                            p.Data=(1+sorted_dice[i].element)*11;
                            me.TryRemoveDice(sorted_dice[i].element);
                            cnt++;
                            break;
                        }
                    }
                }
                p.AvailableTimes=cnt;
            }
            }
        };

    }
}
