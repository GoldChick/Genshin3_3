using TCGBase;

namespace Genshin3_3
{
    public class ElementArtifact : AbstractCardArtifact
    {
        public override string NameID { get; }
        public override CostInit Cost { get; }
        public ElementArtifact(int element, bool big)
        {
            NameID = $"artifact_{((ElementCategory)element).ToString().ToLower()}_{(big ? "big" : "small")}";
            Cost = new CostCreate().Void(2).ToCostInit();
            TriggerDic = new()
            {
                { SenderTag.RoundStep,(me,p,s,v)=>p.AvailableTimes=1},
                { SenderTag.UseDiceFromSkill,new PersistentDiceCostModifier<UseDiceFromSkillSender>(
                    (me,p,s,v)=>me.TeamIndex==s.TeamID&&s.Character.Index==p.PersistentRegion,element,1)},
                { SenderTag.UseDiceFromCard,new PersistentDiceCostModifier<UseDiceFromCardSender>(
                (me,p,s,v)=>me.TeamIndex==s.TeamID&&s.Card is ICardTalent ict && me.Characters[p.PersistentRegion].Card is AbstractCardCharacter abcc &&
                ict.CharacterNameID==abcc.NameID && ict.CharacterNamespace==abcc.Namespace,element,1)},
            };
            if (big)
            {
                Cost.Same(2).ToCostInit();
                TriggerDic.Add(SenderTag.BeforeRerollDice, (me, p, s, v) =>
                {
                    if (me.TeamIndex == s.TeamID && v is DiceRollingVariable drv)
                    {
                        drv.InitialDices.Add(element);
                        drv.InitialDices.Add(element);
                    }
                });
            }
        }
        public override int MaxUseTimes => 1;
        public override PersistentTriggerDictionary TriggerDic { get; }
    }
}
