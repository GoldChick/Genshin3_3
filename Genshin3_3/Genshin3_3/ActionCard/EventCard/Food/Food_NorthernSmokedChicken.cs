using TCGBase;

namespace Genshin3_3
{
    public class Food_NorthernSmokedChicken : AbstractCardFoodSingle
    {
        public override CostInit Cost => new();

        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.UseDiceFromSkill,new PersistentDiceCostModifier<UseDiceFromSkillSender>(
                (me,p,s,v)=>me.TeamIndex==s.TeamID && s.Character.Index==p.PersistentRegion && s.Skill.Category==SkillCategory.A
                ,-1,1)},
            new PersistentPreset.RoundStepDecrease(),
        };
    }
}
