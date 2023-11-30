using TCGBase;
namespace Genshin3_3
{
    public class ParametricTransfomer : AbstractCardSupport
    {
        public override SupportTags SupportTag => SupportTags.Item;
        public override CostInit Cost => new CostCreate().Void(2).ToCostInit();
        public override string NameID => "item_parametrictransfomer";
        public override int MaxUseTimes => 0;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.BeforeUseSkill,(me,p,s,v)=> p.Data=1 },
            { SenderTag.AfterHurt,(me,p,s,v)=>
            {
                if (p.Data!=null && p.Data.Equals(1) && s is HurtSender hs && hs.Element>0)
                {
                    p.Data=2;
                }
            }},
            { SenderTag.AfterUseSkill,(me,p,s,v)=>
            {
                if (p.Data!=null && p.Data.Equals(2))
                {
                    p.AvailableTimes++;
                    if (p.AvailableTimes==3)
                    {
                        p.Active=false;
                        me.AddDiceRange(1+me.Random.Next(7),1+me.Random.Next(7),1+me.Random.Next(7));
                    }
                }
                p.Data=null;
            }}
        };
    }
}
