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
            AtualizarEnergia();
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

        public void AtualizarEnergia()
        {
            DateTime agora = DateTime.Now;

            int energiaAtualizada;

            while (true)
            {
                TimeSpan tempoPassado = agora - dataUltimaAtualizacaoEnergia;

                if (tempoPassado < TimeSpan.Zero)
                {
                    dataUltimaAtualizacaoEnergia = agora;
                    return;
                }

                int energiaAtual = criaturaParaAtulizar.GetEnergiaAtual();  
                var atividade = criaturaParaAtulizar.EstadoAtividadeAtual;

                // Se chegou a zero, precisa dormir.
                if (energiaAtual == 0 &&
                    atividade != Criatura.EstadoAtividade.Dormindo)
                {
                    criaturaParaAtulizar.DefinirEstadoAtividadeAtual(
                        Criatura.EstadoAtividade.Dormindo);

                    dataUltimaAtualizacaoEnergia = agora;
                    return;
                }

                // Recuperação especial durante o sono.
                if (atividade == Criatura.EstadoAtividade.Dormindo)
                {
                    if (energiaAtual == 0)
                    {
                        TimeSpan tempoParaRecuperar =
                            TimeSpan.FromHours(4);

                        if (tempoPassado < tempoParaRecuperar)
                        {
                            return;
                        }

                        criaturaParaAtulizar.DefinirEnergiaAtual(1);

                        dataUltimaAtualizacaoEnergia =
                            dataUltimaAtualizacaoEnergia.Add(tempoParaRecuperar);
                        energiaAtual = 1;
                        // Continua para processar o tempo restante do sono.
                        continue;
                    }

                    int horasCompletas = (int)(
                        tempoPassado.Ticks / TimeSpan.TicksPerHour);

                    if (horasCompletas == 0)
                    {
                        return;                        
                    }

                    energiaAtualizada = (int)Math.Min(
                        100L,
                        energiaAtual + ((long)horasCompletas * 10)
                    );

                    criaturaParaAtulizar.DefinirEnergiaAtual(energiaAtualizada);

                    dataUltimaAtualizacaoEnergia =
                        dataUltimaAtualizacaoEnergia.AddHours(horasCompletas);

                    return;
                }

                TimeSpan intervalo;
                int custoBase;
                bool recuperandoEnergia = false;

                // Regras de cada atividade.
                switch (atividade)
                {
                    case Criatura.EstadoAtividade.Acordado:
                        intervalo = TimeSpan.FromMinutes(5);
                        custoBase = 1;
                        break;

                    case Criatura.EstadoAtividade.Brincando:
                        intervalo = TimeSpan.FromMinutes(3);
                        custoBase = 1;
                        break;

                    case Criatura.EstadoAtividade.trabalhando:
                        intervalo = TimeSpan.FromMinutes(5);
                        custoBase = 4;
                        break;

                    case Criatura.EstadoAtividade.exercitando:
                        intervalo = TimeSpan.FromMinutes(5);
                        custoBase = 2;
                        break;

                    case Criatura.EstadoAtividade.estudando:
                        intervalo = TimeSpan.FromMinutes(7);
                        custoBase = 2;
                        break;

                    case Criatura.EstadoAtividade.Descansando:
                        intervalo = TimeSpan.FromMinutes(2);
                        custoBase = 1;
                        recuperandoEnergia = true;
                        break;

                    default:
                        return;
                }

                // Só processa um intervalo quando ele estiver completo.
                if (tempoPassado < intervalo)
                    return;

                int modificador = recuperandoEnergia
                    ? 0
                    : ObterModificadorConsumoEnergia();


                if (recuperandoEnergia)
                {
                    energiaAtualizada = Math.Min(100, energiaAtual + 1);
                }
                else
                {
                    int custoTotal = custoBase + modificador;

                    energiaAtualizada = Math.Max(
                        0,
                        energiaAtual - custoTotal
                    );
                }

                criaturaParaAtulizar.DefinirEnergiaAtual(energiaAtualizada);

                energiaAtual = energiaAtualizada;

                // Avança somente o intervalo que acabou de ser processado.
                // O tempo restante continua acumulado.
                dataUltimaAtualizacaoEnergia =
                    dataUltimaAtualizacaoEnergia.Add(intervalo);

                // Se desmaiou, passa a dormir.
                // O próximo ciclo do while já considera o sono.
                if (!recuperandoEnergia && energiaAtualizada == 0)
                {
                    criaturaParaAtulizar.DefinirEstadoAtividadeAtual(
                        Criatura.EstadoAtividade.Dormindo);
                }
            }
        }


        private int ObterModificadorConsumoEnergia()
        {
            var atividade = criaturaParaAtulizar.EstadoAtividadeAtual;
            var estagio = criaturaParaAtulizar.EstagioVidaAtual;

            return (atividade, estagio) switch
            {
                (Criatura.EstadoAtividade.Acordado,
                Criatura.EstagioVida.Adultao) => 1,

                (Criatura.EstadoAtividade.Acordado,
                Criatura.EstagioVida.Velhinho) => 2,

                (Criatura.EstadoAtividade.Acordado,
                Criatura.EstagioVida.Ancestral) => 3,

                (Criatura.EstadoAtividade.Brincando,
                Criatura.EstagioVida.Adultao) => 2,

                (Criatura.EstadoAtividade.Brincando,
                Criatura.EstagioVida.Velhinho) => 2,

                (Criatura.EstadoAtividade.Brincando,
                Criatura.EstagioVida.Ancestral) => 3,

                (Criatura.EstadoAtividade.trabalhando,
                Criatura.EstagioVida.Velhinho) => 2,

                (Criatura.EstadoAtividade.exercitando,
                Criatura.EstagioVida.Filhote) => 1,

                (Criatura.EstadoAtividade.exercitando,
                Criatura.EstagioVida.Velhinho) => 2,

                _ => 0
            };
        }
    }
}