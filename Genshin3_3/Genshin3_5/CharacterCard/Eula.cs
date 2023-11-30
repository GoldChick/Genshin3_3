using Genshin3_3;
using TCGBase;

namespace Genshin3_5
{
    public class Eula : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleA(0,2,1),
            new CharacterEffectE(1,2,new Effect_Eula()),
            new CharacterSimpleSkill(SkillCategory.Q,new CostCreate().Cryo(3).MP(2).ToCostInit(),
                (skill,me,c,args)=>me.AddSummon(new Summon_Eula()),new DamageVariable(1,2)),
        };
        //TODO:有剑不加充能
        public override ElementCategory CharacterElement => ElementCategory.Cryo;

        public override WeaponCategory WeaponCategory => WeaponCategory.Claymore;

        public override CharacterRegion CharacterRegion => CharacterRegion.MONDSTADT;
    }
    public class Effect_Eula : AbstractCardPersistent
    {
        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.DamageIncrease,(me,p,s,v)=>
            {
                if (PersistentFunc.IsCurrCharacterDamage(me,p,s,v,out var dv) && s is PreHurtSender hs && hs.RootSource is AbstractCardSkill skill && skill.Category==SkillCategory.E)
                {
                    dv.Damage+=3;
                }
            }
            }
        };
        public override void Update<T>(PlayerTeam me, Persistent<T> persistent)
        {
            persistent.Active = false;
        }
    }
    public class Summon_Eula : AbstractCardPersistentSummon
    {
        public override int MaxUseTimes => 0;
        public override bool CustomDesperated => true;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.AfterUseSkill,(me,p,s,v)=>
            {
                if (me.TeamIndex==s.TeamID &&s is AfterUseSkillSender ss && ss.Character.Card is Eula)
                {
                    if (ss.Skill.Category==SkillCategory.A)
                    {
                        p.AvailableTimes+=2;
                    }else if (ss.Skill.Category==SkillCategory.E)
                    {
                        p.AvailableTimes+=ss.Character.Effects.Contains(typeof(Talent_Eula))?3:2;
                    }
                }
            }
            },
            { SenderTag.RoundOver,(me,p,s,v)=>{ me.Enemy.Hurt(new(0,p.AvailableTimes+3),this); p.Active=false; } }
        };
    }

    public class Talent_Eula : AbstractCardEquipmentOverrideSkillTalent
    {
        public override int Skill =>2;

        public override string CharacterNameID => "eula";

        public override CostInit Cost => new CostCreate().Cryo(3).MP(2).ToCostInit();
    }
}
