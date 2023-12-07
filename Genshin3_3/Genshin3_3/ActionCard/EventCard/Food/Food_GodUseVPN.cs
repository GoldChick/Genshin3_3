using TCGBase;

namespace Genshin3_3
{
    public class Food_GodUseVPN : AbstractCardFoodSingle
    {
        public override CostInit Cost => new CostCreate().Void(2).ToCostInit();
        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.DamageIncrease,(me,p,s,v)=>
            {
                if (me.TeamIndex==s.TeamID && v is DamageVariable dv && dv.DirectSource==DamageSource.Character && s is PreHurtSender hs&&hs.RootSource is AbstractCardSkill sk && sk.Category==SkillCategory.Q)
                {
                    dv.Damage+=3;
                    p.AvailableTimes--;
                }
            }
            },
            new PersistentPreset.RoundStepDecrease(),
        };
    }
}
