using TCGBase;

namespace Genshin3_3
{
    public class ElementArtifact : AbstractCardArtifact
    {
        public override string NameID { get; }
        public override int[] Costs => new int[] { 2 };
        public override bool CostSame { get; }
        public ElementArtifact(int element, bool big)
        {
            NameID = $"artifact_{((ElementCategory)element).ToString().ToLower()}_{(big ? "big" : "small")}";
            CostSame = big;

            TriggerDic = new()
            {
                { SenderTag.RoundOver,(me,p,s,v)=>p.AvailableTimes=1},
                { SenderTag.UseDiceFromSkill,new PersistentDiceCostModifier<UseDiceFromSkillSender>(
                    (me,p,s,v)=>s.ChaIndex==p.PersistentRegion,element,1)},
                { SenderTag.UseDiceFromCard,new PersistentDiceCostModifier<UseDiceFromCardSender>(
                    (me,p,s,v)=>me.GetCards()[s.CardIndex] is AbstractCardEquipmentTalent abct && me.Characters[p.PersistentRegion].Card is AbstractCardCharacter abcc 
                    //&&
                    //(me.CurrCharacter==p.PersistentRegion || abcc.Skills[abct.Skill] is AbstractPassiveSkill) &&abcc.NameID==abct.CharacterNameID && abcc.Namespace==(abct.CharacterNamespace??abct.Namespace)
                    ,
                    element,1)}
                //TODO:减费？
            };
            if (big)
            {
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
