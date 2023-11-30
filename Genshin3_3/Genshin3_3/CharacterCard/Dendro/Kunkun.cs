using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCGBase;

namespace Genshin3_3
{
    public class Kunkun : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleSkill(SkillCategory.A,new CostCreate().Void(2).Dendro(1).ToCostInit(),new DamageVariable(0,2)),
            new CharacterSimpleE(6,3),
            new Q(),
            new Effect_Kunkun_Passive()
        };

        public override ElementCategory CharacterElement => ElementCategory.Dendro;

        public override WeaponCategory WeaponCategory => WeaponCategory.Other;

        public override CharacterRegion CharacterRegion => CharacterRegion.None;
        public override CharacterCategory CharacterCategory => CharacterCategory.Mob;
        private class Q : AbstractCardSkill
        {
            public override SkillCategory Category => SkillCategory.Q;

            public override CostInit Cost => new CostCreate().Dendro(3).ToCostInit();

            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
            {
                var passive = c.Effects.Find(typeof(Effect_Kunkun_Passive));
                me.Enemy.Hurt(new DamageVariable(6, 4 + passive?.AvailableTimes ?? 0), this, () =>
                {
                    if (passive != null)
                    {
                        passive.AvailableTimes = 0;
                    }
                });
            }
        }
    }
    public class Effect_Kunkun_Passive : AbstractCardSkillPassive
    {
        public override bool TriggerOnce => false;
        private static bool IsFullUseTimes(PlayerTeam me, AbstractPersistent p) => p.AvailableTimes == (me.Characters[p.PersistentRegion].Effects.Find("genshin3_3", "talent_kunkun", -3) == null ? 3 : 4);
        public override int MaxUseTimes => 0;
        public override bool CustomDesperated => true;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundOver,(me,p,s,v)=>
            {
                if (IsFullUseTimes(me,p))
                {
                    me.Characters[p.PersistentRegion].MP=0;
                    p.AvailableTimes=0;
                }
            }
            },
            { SenderTag.AfterHurt, (me,p,s,v)=>
            {
                if (s is HurtSender hs)
                {
                    if (
                        (hs.TeamID==me.TeamIndex && hs.TargetIndex==p.PersistentRegion && hs.Element>0) ||
                        (hs.TeamID!=me.TeamIndex && hs.DirectSource==DamageSource.Character && me.Characters[p.PersistentRegion].Card.Skills.Contains(hs.RootSource))
                        )
                    {
                        if (!IsFullUseTimes(me,p))
                        {
                             p.AvailableTimes++;
                        }
                    }
                }
            }
            }
        };
    }
    public class Talent_Kunkun : AbstractCardEquipmentOverrideSkillTalent
    {
        public override int Skill => 1;

        public override string CharacterNameID => "kunkun";

        public override CostInit Cost => new CostCreate().Dendro(3).ToCostInit();
    }
}
