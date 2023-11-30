using TCGBase;

namespace Genshin3_6
{
    public class Genshin3_6_Register : AbstractRegister
    {
        public override void RegisterActionCard(IRegistryConsumer<AbstractCardAction> consumer)
        {
            consumer.Accept(new Location_GrandNarukamiShrine());
            consumer.Accept(new Weapon_Favonius(WeaponCategory.Sword));

            consumer.Accept(new Talent_Ayato());
            consumer.Accept(new Talent_Itto());
            consumer.Accept(new Talent_Tighnari());
        }

        public override void RegisterCharacter(IRegistryConsumer<AbstractCardCharacter> consumer)
        {
            consumer.Accept(new Ayato());
            consumer.Accept(new Itto());
            consumer.Accept(new Tighnari());
        }
    }
}
