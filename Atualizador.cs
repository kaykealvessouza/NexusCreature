namespace TamagochiCSharp
{
    public class Atualizador
    {

        DateTime dataUltimaAtualizacaoFome;
        DateTime dataUltimaAtualizacaoEnergia;

        Criatura criaturaParaAtulizar;


        public Atualizador(Criatura criatura)
        {
            if (criatura == null)
            {
                throw new ArgumentException("Não pode ser uma criatura inexistente");
            }

            criaturaParaAtulizar = criatura;    
            
            dataUltimaAtualizacaoFome = DateTime.Now;
            dataUltimaAtualizacaoEnergia = DateTime.Now;
        }

        public void AtualizarSistema()
        {
            AtualizarFome(criaturaParaAtulizar.GetFomeAtual());
            AtualizarEnergia(criaturaParaAtulizar.GetEnergiaAtual());
        }

        public void AtualizarFome(int fomeAtual)
        {     
            TimeSpan tempoPassado = DateTime.Now - dataUltimaAtualizacaoFome;

            int instanciaQuinzeMin = 0;

            if (tempoPassado.TotalMinutes >= 15)
            {
                instanciaQuinzeMin = (int)(tempoPassado.TotalMinutes / 15); //Veridica quantas instancias de 15 min tem
                
                dataUltimaAtualizacaoFome = dataUltimaAtualizacaoFome.AddMinutes(instanciaQuinzeMin * 15); 
                //Já que verificou as instâncias de 15, adiciona elas pro horario
            }

            int fomeAtualizada = Math.Min(
                fomeAtual + (instanciaQuinzeMin * 2),
                100
            );

            criaturaParaAtulizar.DefinirFomeAtual(fomeAtualizada);
        }

        public void AtualizarEnergia(int energiaAtual)
        {
            TimeSpan tempoPassado = DateTime.Now - dataUltimaAtualizacaoEnergia;

            int energiaAtualizada = energiaAtual + (int)(tempoPassado.TotalHours * 2);

            criaturaParaAtulizar.DefinirEnergiaAtual(energiaAtualizada);
        }
    }
}