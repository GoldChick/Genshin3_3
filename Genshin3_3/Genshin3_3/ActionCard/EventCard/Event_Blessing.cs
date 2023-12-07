using TCGBase;
namespace Genshin3_3
{
    public class Event_Blessing : AbstractCardEvent
    {
        public override CostInit Cost => new();
        public override TargetDemand[] TargetDemands => new TargetDemand[]
        {
            new(TargetEnum.Character_Me,(me,ts)=>me.Characters[ts[0]].Effects.Contains(-2)),
            new(TargetEnum.Character_Me,(me,ts)=>ts[1]!=ts[0])
        };

        public override void AfterUseAction(PlayerTeam me, int[] targetArgs)
        {
            var artifact = me.Characters[targetArgs[0]].Effects.Find(-2);
            if (artifact != null)
            {
                me.Characters[targetArgs[0]].Effects.TryRemove(-2);
                me.AddPersonalEffect(artifact, targetArgs[1] - me.CurrCharacter);
                if (artifact.CardBase.TriggerDic.TryGetValue(SenderTag.RoundStep.ToString(), out var handler))
                {
                    handler.Invoke(me, artifact, new SimpleSender(SenderTag.RoundStep), null);
                }
            }
        }
    }
}
