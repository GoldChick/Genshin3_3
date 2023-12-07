using TCGBase;

namespace Genshin3_3
{
    public class Chongyun : AbstractCardCharacter
    {
        public override int MaxMP => 3;
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleSkill(SkillCategory.A,new CostCreate().Void(2).Cryo(1).ToCostInit(),new DamageVariable(0,2)),
            new CharacterSimpleSkill(SkillCategory.E,new CostCreate().Cryo(3).ToCostInit(),(skill,me,c,args)=>me.AddPersistent(new Effect_Chongyun()),new DamageVariable(1,3)),
            new CharacterSimpleSkill(SkillCategory.Q,new CostCreate().Cryo(3).MP(3).ToCostInit(),new DamageVariable(1,7))
        };
        public override ElementCategory CharacterElement => ElementCategory.Cryo;

        public override WeaponCategory WeaponCategory => WeaponCategory.Claymore;

        public override CharacterRegion CharacterRegion => CharacterRegion.LIYUE;

        public override string NameID => "chongyun";

    }
    public class Effect_Chongyun : AbstractCardEffect
    {
        public override string NameID => "effect_chongyun";
        public Effect_Chongyun(bool talent = false)
        {
            Variant = talent ? 1 : 0;
            TriggerDic = new()
            {
                new PersistentPreset.RoundStepDecrease(),
                { SenderTag.ElementEnchant,(me,p,s,v)=>
                {
                    if (PersistentFunc.IsCurrCharacterDamage(me,p,s,v,out var dv))
                    {
                        var wp=me.Characters[me.CurrCharacter].Card.WeaponCategory;
                        if (wp==WeaponCategory.Sword || wp==WeaponCategory.Claymore || wp==WeaponCategory.Longweapon)
                        {
                            dv.Element=1;
                            if (talent && s is PreHurtSender hs && hs.RootSource is AbstractCardSkill skill && skill.Category==SkillCategory.A)
                            {
                                dv.Damage++;
                            }
                        }
                    }
                }
                }
            };
        }
        public override int MaxUseTimes => 2;

        public override PersistentTriggerDictionary TriggerDic { get; }
    }
    public class Talent_Chongyun : AbstractCardEquipmentOverrideSkillTalent
    {
        public override string CharacterNameID => "chongyun";
        public override int Skill => 1;
        public override CostInit Cost => new CostCreate().Cryo(3).ToCostInit();
        public override void TalentTriggerAction(PlayerTeam me, Character c, int[] targetArgs)
        {
            me.Enemy.Hurt(new DamageVariable(1, 3), c.Card.Skills[1], () =>
            {
                me.AddPersistent(new Effect_Chongyun(true));
            });
        }
    }
}
