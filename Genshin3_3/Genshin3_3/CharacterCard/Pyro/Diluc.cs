using TCGBase;

namespace Genshin3_3
{
    public class Diluc : AbstractCardCharacter
    {
        public override int MaxMP => 3;
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleSkill(SkillCategory.A,new CostCreate().Void(2).Pyro(1).ToCostInit(),new DamageVariable(0,2)),
            new E(),
            new CharacterSimpleSkill(SkillCategory.Q,new CostCreate().Pyro(4).MP(3).ToCostInit(),new DamageVariable(3,8)),
        };

        public override ElementCategory CharacterElement => ElementCategory.Pyro;

        public override WeaponCategory WeaponCategory => WeaponCategory.Claymore;

        public override CharacterRegion CharacterRegion => CharacterRegion.MONDSTADT;
        private class E : AbstractCardSkill
        {
            public override SkillCategory Category => SkillCategory.E;

            public override CostInit Cost =>new CostCreate().Pyro(3).ToCostInit();

            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
            {
                int damage = 3;
                if (c.Effects.Find(-4)?.Data is Dictionary<int, int> map && map.TryGetValue(1, out var times) && times == 2)
                {
                    damage = 5;
                }
                me.Enemy.Hurt(new(3, damage), this);
            }
        }
    }

    public class Effect_Diluc : AbstractCardEffect
    {
        public override int MaxUseTimes => 2;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            new PersistentPreset.RoundStepDecrease(),
            { SenderTag.ElementEnchant,new PersistentElementEnchant(3)}
        };
    }
    public class Talent_Diluc : AbstractCardEquipmentOverrideSkillTalent
    {
        public override int Skill => 1;
        public override string CharacterNameID => "diluc";
        public override CostInit Cost => new CostCreate().Pyro(3).ToCostInit();
        public override int MaxUseTimes => 1;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.UseDiceFromSkill,new PersistentDiceCostModifier<UseDiceFromSkillSender>(
                (me,p,s,v)=>me.TeamIndex==s.TeamID && p.PersistentRegion==s.Character.Index && s.Skill.Category==SkillCategory.E &&  s.Character.Effects.Find(-4)?.Data is Dictionary<int,int> map && map.TryGetValue(1,out var cnt) && cnt==1,
                3,1,false)}
        };
    }
}
