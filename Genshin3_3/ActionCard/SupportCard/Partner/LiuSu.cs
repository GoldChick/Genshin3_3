using TCGBase;
namespace Genshin3_3
{
    public class LiuSu : AbstractCardSupport
    {
        public override string NameID => "partner_liusu";
        public override SupportTags SupportTag => SupportTags.Partner;
        public override int[] Costs => new int[] { 1 };
        public override int MaxUseTimes => 2;
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
