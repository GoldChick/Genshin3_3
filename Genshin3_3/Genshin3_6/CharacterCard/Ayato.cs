using TCGBase;

namespace Genshin3_6
{
    public class Ayato : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleA(0,2,2),
            new CharacterEffectE(2,2,new Effect_Ayato()),
            new CharacterSingleSummonQ(2,1,new Summon_Ayato())
        };

        public override ElementCategory CharacterElement => ElementCategory.Hydro;

        public override WeaponCategory WeaponCategory => WeaponCategory.Sword;

        public override CharacterRegion CharacterRegion => CharacterRegion.INAZUMA;
    }
    public class Effect_Ayato : AbstractCardPersistent
    {
        public override int MaxUseTimes => 3;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.ElementEnchant,(me,p,s,v)=>
            {
                if (PersistentFunc.IsCurrCharacterDamage(me,p,s,v,out var dv) && s is PreHurtSender hs && hs.RootSource is AbstractCardSkill skill && skill.Category==SkillCategory.A)
                {
                    dv.Element=2;
                    dv.Damage++;
                    if (me.Characters[p.PersistentRegion].Effects.Contains(typeof(Talent_Ayato)) &&me.Enemy.Characters[dv.TargetIndex].HP<=6)
                    {
                         dv.Damage++;
	                }
	            }
            } 
            }
        };
    }
    public class Summon_Ayato : AbstractCardPersistentSummon
    {
        public override int MaxUseTimes => 2;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundOver,(me,p,s,v)=>me.Enemy.Hurt(new(2,2),this,()=>p.AvailableTimes--) },
            { SenderTag.DamageIncrease,(me,p,s,v)=>
            {
                if (s is PreHurtSender hs && hs.RootSource is AbstractCardSkill skill&&skill.Category==SkillCategory.A && v is DamageVariable dv && dv.DirectSource ==DamageSource.Character)
                {
                    dv.Damage++;
	            }
            } 
            }
        };
    }
    public class Talent_Ayato : AbstractCardEquipmentOverrideSkillTalent
    {
        public override int Skill => 1;

        public override string CharacterNameID => "ayato";

        public override CostInit Cost => new CostCreate().Hydro(3).ToCostInit();
    }
}
