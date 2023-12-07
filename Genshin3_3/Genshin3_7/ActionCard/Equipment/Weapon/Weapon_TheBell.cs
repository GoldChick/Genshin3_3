using TCGBase;

namespace Genshin3_7
{
    public class Weapon_TheBell : AbstractCardWeapon
    {
        public override WeaponCategory WeaponCategory => WeaponCategory.Claymore;
        public override CostInit Cost => new CostCreate().Same(3).ToCostInit();
        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            new PersistentPreset.RoundStepReset(),
            new PersistentPreset.WeaponDamageIncrease(),
            { SenderTag.AfterUseSkill,(me,p,s,v)=>
            {
                if (p.AvailableTimes>0&&s.TeamID==me.TeamIndex && s is AfterUseSkillSender ss && ss.Character.Index==p.PersistentRegion)
                {
                    me.AddPersistent(new Effect_TheBell());
                }
            }
            }
        };
    }
    public class Effect_TheBell : AbstractPersistentShieldYellow
    {
        public override int InitialUseTimes => 1;
        public override int MaxUseTimes => 2;
        public override void Update<T>(PlayerTeam me, Persistent<T> persistent)
        {
            if (persistent.AvailableTimes < MaxUseTimes)
            {
                persistent.AvailableTimes = int.Min(persistent.AvailableTimes + 1, MaxUseTimes);
            }
        }
    }
}
