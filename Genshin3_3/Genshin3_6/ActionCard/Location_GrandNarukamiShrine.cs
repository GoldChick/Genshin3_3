using TCGBase;

namespace Genshin3_6
{
    public class Location_GrandNarukamiShrine : AbstractCardSupport
    {
        public override SupportTags SupportTag => SupportTags.Place;

        public override CostInit Cost => new CostCreate().Same(2).ToCostInit();
        public override int MaxUseTimes => 3;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundStart,(me,p,s,v)=>p.Data=null},
            { SenderTag.AfterAnyAction,(me,p,s,v)=>
            {
                if (p.Data!=null)
                {
                     me.AddDiceRange(me.Random.Next(0,7)+1);
	            }
            }
            }
        };
    }
}
