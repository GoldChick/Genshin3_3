using TCGBase;

namespace Genshin3_3
{
    public class Food_Jueyunguoba : AbstractCardFoodSingle
    {
        public override CostInit Cost => new();

        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.DamageIncrease,(me,p,s,v)=>
            {
                if (me.TeamIndex==s.TeamID && v is DamageVariable dv && dv.DirectSource==DamageSource.Character && s is PreHurtSender hs&&hs.RootSource is AbstractCardSkill sk && sk.Category==SkillCategory.A)
                {
                    dv.Damage++;
                    p.AvailableTimes--;
                }
            }
            },
            { SenderTag.RoundStep,(me,p,s,v)=>p.AvailableTimes-- }
        };
    }
}
