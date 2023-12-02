using TCGBase;

namespace Genshin3_7
{
    public class Location_SangonomiyaShrine : AbstractCardSupport
    {
        public override SupportTags SupportTag => SupportTags.Place;
        public override CostInit Cost => new CostCreate().Same(2).ToCostInit();
        public override int MaxUseTimes => 0;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            {
                SenderTag.RoundOver,(me,p,s,v)=>
                {
                    if (me.Characters.Any(c=>c.HP<c.Card.MaxHP))
                    {
                        me.Heal(this,new HealVariable(1),new HealVariable(1,0,true));
                    }
                }
            }
        };

    }
}
