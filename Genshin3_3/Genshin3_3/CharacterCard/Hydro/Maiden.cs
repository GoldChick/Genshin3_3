using TCGBase;

namespace Genshin3_3
{
    public class Maiden : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleA(2,1),
            new E(),
            new CharacterSimpleQ(2,5)
        };

        public override ElementCategory CharacterElement => ElementCategory.Hydro;

        public override WeaponCategory WeaponCategory => WeaponCategory.Other;

        public override CharacterRegion CharacterRegion => CharacterRegion.Fatui;
        private class E : AbstractCardSkill
        {
            public override SkillCategory Category => SkillCategory.E;

            public override CostInit Cost => new CostCreate().Hydro(3).ToCostInit();

            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
            {
                me.Enemy.Hurt(new(2, 2), c.Card.Skills[1], () => me.Enemy.AddPersistent(new Effect_Maiden(false), me.Enemy.CurrCharacter));
            }
        }
    }
    public class Effect_Maiden : AbstractCardPersistent
    {
        public Effect_Maiden(bool talent)
        {
            Variant = talent ? 1 : 0;
            MaxUseTimes = talent ? 3 : 2;
            TriggerDic = new()
            {
                { SenderTag.DamageIncrease,(me,p,s,v)=>
                {
                    if (me.TeamIndex!=s.TeamID && v is DamageVariable dv && dv.TargetIndex==p.PersistentRegion && dv.Element==2)
                    {
                        dv.Damage++;
                    }
                } },
                { SenderTag.RoundStep,(me,p,s,v)=> {p.AvailableTimes--;p.Data=null; } }
            };
            if (talent)
            {
                TriggerDic.Add(SenderTag.UseDiceFromSwitch, new PersistentDiceCostModifier<UseDiceFromSwitchSender>(
                    (me, p, s, v) => s.TeamID == me.TeamIndex && s.Source == p.PersistentRegion && p.Data == null,
                    0, -1, false, (me, p, s) => p.Data = 1));
            }
        }

        public override int MaxUseTimes { get; }

        public override PersistentTriggerDictionary TriggerDic { get; }
    }
    public class Talent_Maiden : AbstractCardEquipmentOverrideSkillTalent
    {
        public override int Skill => 1;

        public override string CharacterNameID => "maiden";
        public override CostInit Cost => new CostCreate().Hydro(3).ToCostInit();
        public override void TalentTriggerAction(PlayerTeam me, Character c, int[] targetArgs)
        {
            me.Enemy.Hurt(new(2, 2), c.Card.Skills[1], () => me.Enemy.AddPersistent(new Effect_Maiden(true), me.Enemy.CurrCharacter));
        }
    }
}
