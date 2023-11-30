using TCGBase;

namespace Genshin3_3
{
    public class Food_MintyMeatRolls : AbstractCardFoodSingle
    {
        public override CostInit Cost => new CostCreate().Same(1).ToCostInit();

        public override int MaxUseTimes => 3;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.UseDiceFromSkill,new PersistentDiceCostModifier<UseDiceFromSkillSender>(
                (me,p,s,v)=>me.TeamIndex==s.TeamID && s.Character.Index==p.PersistentRegion && s.Skill.Category==SkillCategory.A
                ,-1,1)},
            { SenderTag.RoundStep,(me,p,s,v)=>p.Active=false }
        };
    }
}
