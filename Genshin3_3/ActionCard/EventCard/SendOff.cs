using TCGBase;
namespace Genshin3_3
{
    public class SendOff : AbstractCardEvent,ITargetSelector
    {
        public override string NameID => "event_sendoff";
        public override int[] Costs => new int[] {2};

        public TargetEnum[] TargetEnums => new TargetEnum[] { TargetEnum.Summon_Enemy};

        public override void AfterUseAction(PlayerTeam me, int[] targetArgs)
        {
            me.Enemy.Summons[targetArgs[0]].Active=false;
        }
    }
}
