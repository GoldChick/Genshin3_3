using TCGBase;
namespace Genshin3_7
{
    public class Artifact_ShimenawaReminiscence : Artifact_CapriciousVisage
    {
        public override CostInit Cost => new CostCreate().Void(3).ToCostInit();
        public Artifact_ShimenawaReminiscence():base()
        {
            TriggerDic.Add(SenderTag.DamageIncrease, (me, p, s, v) =>
            {
                if (me.Characters[p.PersistentRegion].MP>=2 && PersistentFunc.IsCurrCharacterDamage(me,p,s,v,out var dv)&& s is PreHurtSender hs && hs.RootSource is AbstractCardSkill skill &&(skill.Category==SkillCategory.A || skill.Category==SkillCategory.E))
                {
                    dv.Damage++;
                }
            });
        }
    }
}
