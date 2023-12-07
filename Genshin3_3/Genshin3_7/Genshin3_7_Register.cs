using TCGBase;

namespace Genshin3_7
{
    public class Genshin3_7_Register : AbstractRegister
    {
        public override void RegisterActionCard(IRegistryConsumer<AbstractCardAction> consumer)
        {
            consumer.Accept(new Artifact_CapriciousVisage());
            consumer.Accept(new Artifact_EmblemOfSeveredFate());
            consumer.Accept(new Artifact_Helm_Big());
            consumer.Accept(new Artifact_ShimenawaReminiscence());
            consumer.Accept(new Artifact_ThunderingPoise());
            consumer.Accept(new Artifact_VermillionHereafter());

            consumer.Accept(new Weapon_Ams());
            consumer.Accept(new Weapon_AThousandFloatingDreams());
            consumer.Accept(new Weapon_ElegyForTheEnd());
            consumer.Accept(new Weapon_EngulfingLightning());
            consumer.Accept(new Weapon_TheBell());
            consumer.Accept(new Weapon_VortexVanquisher());

            consumer.Accept(new Location_ChinjuForest());
            consumer.Accept(new Location_SangonomiyaShrine());
            consumer.Accept(new Location_SumeruCity());
            consumer.Accept(new Location_Tenshukaku());
            consumer.Accept(new Location_Vanarana());

            consumer.Accept(new Partner_Dunyarzad());
            consumer.Accept(new Partner_KidKujirai());
            consumer.Accept(new Partner_Rana());

            consumer.Accept(new Item_RedFeatherFan());
            consumer.Accept(new Item_TreasureSeekingSeelie());

            consumer.Accept(new Talent_Amber());
            consumer.Accept(new Talent_HuTao());
            consumer.Accept(new Talent_Nahida());
            consumer.Accept(new Talent_Shenhe());
            consumer.Accept(new Talent_Tartaglia());
            consumer.Accept(new Talent_Venti());
            consumer.Accept(new Talent_Yae());
        }

        public override void RegisterCharacter(IRegistryConsumer<AbstractCardCharacter> consumer)
        {
            consumer.Accept(new Amber());
            consumer.Accept(new HuTao());
            consumer.Accept(new Nahida());
            consumer.Accept(new Shenhe());
            consumer.Accept(new Tartaglia());
            consumer.Accept(new Venti());
            consumer.Accept(new Yae());
        }
    }
}
