using TCGBase;

namespace Genshin3_7
{
    internal class Partner_Rana : AbstractCardSupport
    {
        public override SupportTags SupportTag => SupportTags.Partner;
        public override CostInit Cost => new CostCreate().Same(2).ToCostInit();
        public override int MaxUseTimes => 1;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundStep,(me,p,s,v)=>p.AvailableTimes=MaxUseTimes},
            { SenderTag.AfterUseSkill,(me,p,s,v)=>
            {
                if (me.TeamIndex==s.TeamID && p.AvailableTimes>0 && s is AfterUseSkillSender ss && ss.Skill.Category==SkillCategory.E)
                {
                    var l=me.Characters.Length;
                    for (int i = 1; i < l; i++)
                    {
                        var c=me.Characters[(i+me.CurrCharacter)%l];
                        if (c.Alive)
                        {
                            me.AddDiceRange((int)c.Card.CharacterElement);
                            p.AvailableTimes--;
                            break;
                        }
                    }
                }
            }
            }
        };
    }
}
