using TCGBase;


namespace Genshin3_3
{
    public class ChangingShifts : AbstractCardEvent
    {
        public override string NameID => "event_changingshifts";
        public override int[] Costs => Array.Empty<int>();

        public override void AfterUseAction(PlayerTeam me, int[] targetArgs)
        {
            me.AddPersistent(new E());
        }
        public class E : AbstractCardPersistentEffect
        {
            public override int MaxUseTimes => 1;

            public override string TextureNameID => PersistentTextures.Buff;

            public override PersistentTriggerDictionary TriggerDic => new() 
            {
                { SenderTag.UseDiceFromSwitch,new PersistentDiceCostModifier<UseDiceFromSwitchSender>((me,p,s,v)=>true,0,1) }
            };
        }
    }
}
