using TCGBase;

namespace Genshin3_7
{
    internal class Partner_KidKujirai : AbstractCardSupport
    {
        public override SupportTags SupportTag => SupportTags.Partner;
        public override CostInit Cost => new();
        public override int MaxUseTimes => 0;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundStart,(me,p,s,v)=>
            {
                me.AddDiceRange(0);
                if (!me.Enemy.Supports.Full)
                {
                    p.Active=false;
                    me.Enemy.AddSupport(this);
	            }
            } 
            }
        };
    }
}
