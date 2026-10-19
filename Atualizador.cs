namespace TamagochiCSharp
{
    public class Atualizador
    {

        DateTime dataUltimaAtualizacao;

        Criatura criaturaParaAtulizar;

        public Atualizador(Criatura criatura)
        {
            if (criatura == null)
            {
                throw new ArgumentException("Não pode ser uma criatura inexistente");
            }

            criaturaParaAtulizar = criatura;

            dataUltimaAtualizacao = DateTime.Now;

            //Chama os métodos e atualiza assim que criado, ja definindo 
            //sua última atualizção para quando criado
            AtualizarFome(criaturaParaAtulizar.FomeAtual);
        }

        public int AtualizarFome(int fomeAtual)
        {
            int fomeAtualizada = 0;

            return fomeAtualizada;
        }
    }
}