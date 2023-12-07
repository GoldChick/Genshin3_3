using TCGBase;

namespace Genshin4_0
{
    public class Weapon_KingSquire : AbstractCardWeapon
    {
        public override WeaponCategory WeaponCategory => WeaponCategory.Bow;

        public override int MaxUseTimes => 0;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
           new PersistentPreset.WeaponDamageIncrease()
        };

        public override CostInit Cost => new CostCreate().Same(3).ToCostInit();
        public override void AfterUseAction(PlayerTeam me, int[] targetArgs)
        {
            base.AfterUseAction(me, targetArgs);
            me.AddPersonalEffect(new Effect_ForestBook(), targetArgs[0] - me.CurrCharacter);
        }
    }
    public class Effect_ForestBook : AbstractCardEffect
    {
        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            new PersistentPreset.RoundStepDecrease(),
            { SenderTag.UseDiceFromSkill,(me,p,s,v)=>
            {
                if (me.TeamIndex==s.TeamID && s is UseDiceFromSkillSender ss && ss.Character.Index==p.PersistentRegion && ss.Skill.Category==SkillCategory.E)
                {
                    if (v is CostVariable cv)
                    {
                        CostModifier mod=new(DiceModifierType.Same,2);
                        mod.Modifier(cv);
                        p.AvailableTimes--;
                    }
                }
            } },
            { SenderTag.UseDiceFromCard,(me,p,s,v)=>
            {
                if (me.TeamIndex==s.TeamID && s is UseDiceFromCardSender ss && ss is ICardTalent t && t.CharacterNameID==me.Characters[p.PersistentRegion].Card.NameID)
                {
                    if (v is CostVariable cv)
                    {
                        CostModifier mod=new(DiceModifierType.Same,2);
                        mod.Modifier(cv);
                        p.AvailableTimes--;
                    }
                }
            } }
        };
    }
}
