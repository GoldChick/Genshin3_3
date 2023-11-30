using TCGBase;

namespace Genshin3_7
{
    public class Artifact_VermillionHereafter : Artifact_ThunderingPoise
    {
        public Artifact_VermillionHereafter() : base()
        {
            TriggerDic.Add(SenderTag.AfterSwitch, (me, p, s, v) =>
            {
                if (me.TeamIndex==s.TeamID && s is AfterSwitchSender ss && ss.Target==p.PersistentRegion && ss.Target!=ss.Source)
                {
                    me.AddPersistent(new Effect_VermillionHereafter(), p.PersistentRegion);
                }
            });
        }
    }
    public class Effect_VermillionHereafter : AbstractCardPersistent
    {
        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.DamageIncrease,(me,p,s,v)=>
            {
                if (me.TeamIndex==s.TeamID && v is DamageVariable dv && dv.DirectSource==DamageSource.Character && s is PreHurtSender hs && hs.RootSource is AbstractCardSkill skill && skill.Category==SkillCategory.A)
                {
                    dv.Damage++;
	            }
            } 
            },
            { SenderTag.RoundStep,(me,p,s,v)=>p.AvailableTimes--}
        };
    }
}
