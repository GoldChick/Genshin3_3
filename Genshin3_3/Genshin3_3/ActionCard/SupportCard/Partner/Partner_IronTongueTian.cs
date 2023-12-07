using TCGBase;
namespace Genshin3_3
{
    public class Partner_IronTongueTian : AbstractCardSupport
    {
        public override SupportTags SupportTag => SupportTags.Partner;
        public override CostInit Cost => new CostCreate().Void(2).ToCostInit();
        public override int MaxUseTimes => 2;
        public override bool CustomDesperated => false;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundOver, (me,p,s,v)=>
            {
                var l=me.Characters.Length;
                for (int i = 0; i < l; i++)
                {
                    var c=me.Characters[(i+l)%l];
                    if(c.MP<c.Card.MaxMP)
                    {
                        c.MP++;
                        p.AvailableTimes--;
                        break;
                    }
                }
            }
            }
        };
    }
}
