using TCGBase;

namespace Genshin3_7
{
    public class Artifact_CapriciousVisage : AbstractCardArtifact
    {
        public override int MaxUseTimes => 1;

        public override CostInit Cost => new CostCreate().Void(2).ToCostInit();
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundStep,(me,p,s,v)=>p.AvailableTimes=MaxUseTimes},
            { SenderTag.UseDiceFromSkill,new PersistentDiceCostModifier<UseDiceFromSkillSender>(
                (me,p,s,v)=>me.TeamIndex==s.TeamID&&s.Character.Index==p.PersistentRegion&&s.Skill.Category==SkillCategory.E,0,1)},
            { SenderTag.UseDiceFromCard,new PersistentDiceCostModifier<UseDiceFromCardSender>(
                (me,p,s,v)=>me.TeamIndex==s.TeamID&&s.Card is ICardTalent ict && me.Characters[p.PersistentRegion].Card is AbstractCardCharacter abcc &&
                ict.CharacterNameID==abcc.NameID && ict.CharacterNamespace==abcc.Namespace,0,1)}
        };

    }
}
