using TCGBase;

namespace Genshin3_3
{
    internal class Barbara : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleSkill(SkillCategory.A,new CostCreate().Void(2).Hydro(1).ToCostInit(),new DamageVariable(2,1)),
            new CharacterSimpleSkill(SkillCategory.E,new CostCreate().Hydro(3).ToCostInit(),(skill,me,c,args)=>me.AddSummon(new Summon_Barbara()),new DamageVariable(2,1)),
            new CharacterSimpleSkill(SkillCategory.Q,new CostCreate().Hydro(3).MP(3).ToCostInit(),
                (skill,me,p,ts)=>me.Heal(skill, new(4), new(4, 0, true)))
        };
        public override int MaxMP => 3;
        public override ElementCategory CharacterElement => ElementCategory.Hydro;

        public override WeaponCategory WeaponCategory => WeaponCategory.Catalyst;

        public override CharacterRegion CharacterRegion => CharacterRegion.MONDSTADT;
    }
    public class Summon_Barbara : AbstractCardSummon
    {
        public override int MaxUseTimes => 2;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundOver,(me,p,s,v)=>
            {
                me.Heal(this,new(1),new(1,0,true));
                me.AttachElement(this,2,0);
            }
            }
        };
    }
    public class Talent_Barbara : AbstractCardEquipmentOverrideSkillTalent
    {
        public override int Skill => 1;

        public override string CharacterNameID => "barbara";

        public override CostInit Cost => new CostCreate().Hydro(3).ToCostInit();
        public override int MaxUseTimes => 1;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            new PersistentPreset.RoundStepReset(),
            { SenderTag.UseDiceFromSwitch,new PersistentDiceCostModifier<UseDiceFromSwitchSender>(
                (me,p,s,v)=> me.TeamIndex==s.TeamID&&me.Summons.Find(typeof(Summon_Barbara)) is AbstractPersistent summon
                ,0,1)}
        };
    }
}
