using TCGBase;
namespace Genshin3_3
{
    public class WangshuInn : AbstractCardSupport, IDamageSource
    {
        public override string NameID => "location_wangshuinn";
        public override SupportTags SupportTag => SupportTags.Place;
        public override CostInit Cost => new CostCreate().Same(2).ToCostInit();
        public override int MaxUseTimes => 2;
        public override bool CustomDesperated => false;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundOver, (me,p,s,v)=>
                {
                    int id=me.FindHPLostMost(me.CurrCharacter);
                    if(id!=me.CurrCharacter)
                    {
                        me.Heal(this,new HealVariable(2,id-me.CurrCharacter));
                        p.AvailableTimes--;
                    }
                }
            }
        };

        public DamageSource DamageSource => DamageSource.Addition;
    }
}
