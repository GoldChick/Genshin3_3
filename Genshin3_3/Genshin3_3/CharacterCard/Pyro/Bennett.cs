using TCGBase;

namespace Genshin3_3
{
    public class Bennett : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
        new CharacterSimpleA(0,2,3),
        new CharacterSimpleE(3,3),
        new CharacterSimpleSkill(SkillCategory.Q,new CostCreate().Pyro(4).MP(2).ToCostInit(),
            (skill,me,c,args)=>me.AddPersistent(new Effect_Bennett()),new DamageVariable(3,2)),
        };

        public override string NameID => "bennett";

        public override ElementCategory CharacterElement => ElementCategory.Pyro;

        public override WeaponCategory WeaponCategory => WeaponCategory.Sword;

        public override CharacterRegion CharacterRegion => CharacterRegion.LIYUE;
    }
    public class Effect_Bennett : AbstractCardPersistent
    {
        public Effect_Bennett(bool talent = false)
        {
            Variant = talent ? 1 : 0;
            TriggerDic = new()
            {
                { SenderTag.RoundStep,(me,p,s,v)=>p.AvailableTimes-- },
                { SenderTag.DamageIncrease,(me,p,s,v)=>
                    {
                        if (PersistentFunc.IsCurrCharacterDamage(me,p,s,v,out var dv))
                        {
                            if (talent || me.Characters[me.CurrCharacter].HP>=7)
                            {
                                dv.Damage+=2;
                            }
                        }
                    }
                },
                { SenderTag.AfterUseSkill,(me, p, s, v) =>
                    {
                        if (me.TeamIndex==s.TeamID && s is AfterUseSkillSender uss && uss.Character.HP<7)
                        {
                            me.Heal(this,new HealVariable(2,uss.Character.Index-me.CurrCharacter));
                        }
                    }
                }
            };
        }
        public override int MaxUseTimes => 2;
        public override PersistentTriggerDictionary TriggerDic { get; }
    }

    public class Talent_Bennett : AbstractCardEquipmentOverrideSkillTalent
    {
        public override string CharacterNameID => "bennett";

        public override int Skill => 2;

        public override CostInit Cost => new CostCreate().Pyro(4).MP(2).ToCostInit();

        public override void TalentTriggerAction(PlayerTeam me, Character c, int[] targetArgs)
        {
            me.Enemy.Hurt(new(3, 2), c.Card.Skills[2], () => me.AddPersistent(new Effect_Bennett(true), c.Index));
        }
    }
}
