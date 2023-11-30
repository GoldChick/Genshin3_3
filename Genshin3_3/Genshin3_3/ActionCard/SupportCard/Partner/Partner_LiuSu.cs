using TCGBase;
namespace Genshin3_3
{
    public class Partner_LiuSu : AbstractCardSupport
    {
        public override string NameID => "partner_liusu";
        public override SupportTags SupportTag => SupportTags.Partner;
        public override CostInit Cost => new CostCreate().Same(1).ToCostInit();
        public override int MaxUseTimes => 2;
        public override bool CustomDesperated => false;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundStart, (me,p,s,v)=>p.Data=0 },
            {SenderTag.AfterSwitch,(me,p,s,v)=>
            {
                if ((p.Data==null || p.Data.Equals(0)) && me.TeamIndex==s.TeamID && s is AfterSwitchSender ss && me.Characters[ss.Target].MP==0)
                {
                    me.Characters[ss.Target].MP++;
                    p.Data=1;
                    p.AvailableTimes--;
                }
            }}
        };
    }
}
