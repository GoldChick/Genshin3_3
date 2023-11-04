using TCGBase;
namespace Genshin3_3
{
    public class FavoniusCathedral : AbstractCardSupport,IDamageSource
    {
        public override string NameID => "location_favoniuscathedral";
        public override SupportTags SupportTag => SupportTags.Place;
        public override int[] Costs => new int[] { 2 };
        public override bool CostSame => true;
        public override int MaxUseTimes => 2;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundOver, (me,p,s,v)=>
                {
                    var c=me.Characters[me.CurrCharacter];
                    if (c.HP<c.Card.MaxHP)
                    {
                        me.Heal(this,new DamageVariable(0,2));
                        p.AvailableTimes--;
                    }
                }
            }
        };

        public DamageSource DamageSource => DamageSource.Addition;
    }
}
