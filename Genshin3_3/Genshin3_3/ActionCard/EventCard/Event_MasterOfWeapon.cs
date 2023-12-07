using TCGBase;
namespace Genshin3_3
{
    public class Event_MasterOfWeapon : AbstractCardEvent
    {
        public override CostInit Cost => new();
        public override TargetDemand[] TargetDemands => new TargetDemand[]
        {
            new(TargetEnum.Character_Me,(me,ts)=>me.Characters[ts[0]].Effects.Contains(-1)),
            new(TargetEnum.Character_Me,(me,ts)=>me.Characters[ts[1]].Card.WeaponCategory==me.Characters[ts[0]].Card.WeaponCategory && ts[1]!=ts[0])
        };

        public override void AfterUseAction(PlayerTeam me, int[] targetArgs)
        {
            var weapon = me.Characters[targetArgs[0]].Effects.Find(-1);
            if (weapon != null)
            {
                me.Characters[targetArgs[0]].Effects.TryRemove(-1);
                me.AddPersonalEffect(weapon, targetArgs[1] - me.CurrCharacter);
                if (weapon.CardBase.TriggerDic.TryGetValue(SenderTag.RoundStep.ToString(), out var handler))
                {
                    handler.Invoke(me, weapon, new SimpleSender(SenderTag.RoundStep), null);
                }
            }
        }
    }
}
