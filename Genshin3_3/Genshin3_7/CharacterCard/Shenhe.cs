using TCGBase;

namespace Genshin3_7
{
    public class Shenhe : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleA(0,2,1),
            new CharacterEffectE(1,2,new Effect_Shenhe(),false),
            new CharacterSimpleSkill(SkillCategory.Q,new CostCreate().Cryo(3).MP(2).ToCostInit(),
                (skill,me,c,args)=>me.AddSummon(new Summon_Shenhe()),new DamageVariable(1,1)),
        };

        public override ElementCategory CharacterElement => ElementCategory.Cryo;

        public override WeaponCategory WeaponCategory => WeaponCategory.Longweapon;

        public override CharacterRegion CharacterRegion => CharacterRegion.LIYUE;
    }
    public class Effect_Shenhe : AbstractCardPersistent
    {
        public override int MaxUseTimes => 2;
        public Effect_Shenhe(bool talent = false)
        {
            Variant = talent ? 1 : 0;
            TriggerDic = new()
            {
                { SenderTag.RoundStep,(me,p,s,v)=>p.Data=null},
                { SenderTag.DamageIncrease,(me,p,s,v)=>
                {
                    if (me.TeamIndex==s.TeamID && s is PreHurtSender hs && hs.RootSource is AbstractCardSkill skill && v is DamageVariable dv)
                    {
                        if (dv.Element==1)
                        {
                            dv.Damage++;
                            if (talent && p.Data==null && skill.Category==SkillCategory.A && dv.DirectSource==DamageSource.Character)
                            {
                                p.Data=114;
	                        }else
                            {
                                p.AvailableTimes--;
                            }
                        }
	                }
                } 
                }
            };
        }
        public override PersistentTriggerDictionary TriggerDic { get; }
    }
    public class Summon_Shenhe : AbstractCardPersistentSummon
    {
        public override int MaxUseTimes => throw new NotImplementedException();

        public override PersistentTriggerDictionary TriggerDic => throw new NotImplementedException();
    }

    public class Talent_Shenhe : AbstractCardEquipmentOverrideSkillTalent
    {
        public override int Skill => throw new NotImplementedException();

        public override string CharacterNameID => throw new NotImplementedException();

        public override CostInit Cost => throw new NotImplementedException();
    }
}
