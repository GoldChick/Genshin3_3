using TCGBase;

namespace Genshin3_3
{
    public class Chongyun : AbstractCardCharacter
    {
        public override int MaxMP => 3;
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleA(0,2,1),
            new CharacterEffectE(1,3,new Enchant(1,2),false)
        };

        public override ElementCategory CharacterElement => ElementCategory.Cryo;

        public override WeaponCategory WeaponCategory => WeaponCategory.Claymore;

        public override CharacterRegion CharacterRegion => CharacterRegion.LIYUE;

        public override string NameID => "chongyun";
    }
    public class Talent_Chongyun : AbstractCardTalent
    {
        public override CardPersistentTalent Effect => new E();

        public override string CharacterNameID => "keqing";

        public override int Skill => 1;

        public override int[] Costs => new int[] { 0, 3 };
        private class E : CardPersistentTalent
        {
            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
            {
                base.AfterUseAction(me, c, targetArgs);
                //TODO:附魔伤害+1
            }
        }
    }
}
