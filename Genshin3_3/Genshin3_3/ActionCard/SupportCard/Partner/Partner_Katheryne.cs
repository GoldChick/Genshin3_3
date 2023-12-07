using TCGBase;

namespace Genshin3_3
{
    public class Partner_Katheryne : AbstractCardSupport
    {
        public override CostInit Cost => new CostCreate().Same(3).ToCostInit();
        public override SupportTags SupportTag => SupportTags.Partner;

        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            new PersistentPreset.RoundStepReset(),
            { SenderTag.AfterSwitch,(me,p,s,v)=>
            {
                if (me.TeamIndex==s.TeamID && v is FastActionVariable fv && !fv.Fast)
                {
                    fv.Fast= true;
                    p.AvailableTimes--;
	            }
            } 
            }
        };
    }
}
