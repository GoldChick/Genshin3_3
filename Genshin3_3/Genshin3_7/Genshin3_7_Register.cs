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


        }

        public override void RegisterCharacter(IRegistryConsumer<AbstractCardCharacter> consumer)
        {

        }
    }
}
