using TCGBase;

namespace Genshin3_5
{
    public class KojouSara : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleA(0,2,4),
            new CharacterSingleSummonE(4,1,new Summon_KojouSara_E()),
            new CharacterSimpleSkill(SkillCategory.Q,new CostCreate().Electro(4).MP(2).ToCostInit(),
                (skill,me,c,args)=>me.AddSummon(new Summon_KojouSara_Q()),new DamageVariable(4,1)),
        };

        public override ElementCategory CharacterElement => ElementCategory.Electro;

        public override WeaponCategory WeaponCategory => WeaponCategory.Bow;

        public override CharacterRegion CharacterRegion => CharacterRegion.INAZUMA;
    }
    public class Effect_KojouSara : AbstractCardPersistent
    {
        public override int MaxUseTimes => 2;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.DamageIncrease,(me,p,s,v)=>
            {
                if (PersistentFunc.IsCurrCharacterDamage(me,p,s,v,out var dv) && s is PreHurtSender hs && hs.RootSource is AbstractCardSkill skill &&( skill.Category==SkillCategory.E || skill.Category==SkillCategory.Q ))
                {
                    var kojo=me.Characters.Where(p=>p.Card is KojouSara);
                    if (kojo.Any(p=>p.Alive && p.Effects.Contains(-3))&&me.Characters[p.PersistentRegion].Card.CharacterElement==ElementCategory.Electro)
                    {
                        dv.Damage++;
                    }
                    dv.Damage++;
                    p.AvailableTimes--;
                }
            }
            }
        };
    }

    public class Summon_KojouSara_E : AbstractCardPersistentSummon
    {
        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundOver,(me,p,s,v)=>me.Enemy.Hurt(new(4,1),this,()=>me.AddPersistent(new Effect_KojouSara(),me.CurrCharacter))}
        };
    }
    public class Summon_KojouSara_Q : AbstractCardPersistentSummon
    {
        public override int MaxUseTimes => 2;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundOver,(me,p,s,v)=>me.Enemy.Hurt(new(4,2),this,()=>me.AddPersistent(new Effect_KojouSara(),me.CurrCharacter))}
        };
    }
    public class Talent_KojouSara : AbstractCardEquipmentOverrideSkillTalent
    {
        public override int Skill => 1;

        public override string CharacterNameID => "jojousara";

        public override CostInit Cost => new CostCreate().Electro(3).ToCostInit();
    }
}
