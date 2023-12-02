using TCGBase;

namespace Genshin3_7
{
    internal class Item_TreasureSeekingSeelie : AbstractCardSupport
    {
        public override SupportTags SupportTag => SupportTags.Item;

        public override int MaxUseTimes => 0;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.AfterUseSkill,(me,p,s,v)=>
            {
                if (me.TeamIndex==s.TeamID)
                {
                    p.AvailableTimes++;
                    if (p.AvailableTimes==3)
                    {
                        p.Active=false;
                        me.RollCard(3);
                    }
                }
            } 
            }
        };

        public override CostInit Cost => new CostCreate().Same(1).ToCostInit();
    }
}
