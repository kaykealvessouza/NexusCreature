namespace TamagochiCSharp
{
    public class Criatura
    {

        private static readonly Random random = new Random();

        public string Nome { get; private set; } = null!;

        public DateTime DataNascimento { get; private set; }
        public TimeSpan TempoDeVida { get; private set; }
        public int DiasDeVida {get; private set;}
        public int AnosDeVida { get; private set; }
        public int MesesDeVida { get; private set; }
        public int DiasRestantes { get; private set; }

        public int FomeAtual { get; private set; } // 0 - 10 Barrigao, 11 - 35 Cheio, 36 - 55 Satisfeito, 56 - 70 Fomezinha, 71 - 100 Faminto
        public int EnergiaAtual { get; private set; } // 100 - 85 Eletrico, 84 - 60 Disposto, 59 - 40 Normal, 39 - 16 Cansado, 15 - 1 Desmaiando, 0 Desmaiou

        public EstagioVida EstagioVidaAtual { get; private set; }
        public Humor HumorAtual { get; private set; }
        public Sentimento SentimentoAtual { get; private set; }

        public enum Humor
        {
            FELIZ,
            Alegre,
            Normal,
            Tristinho,
            Triste,
            Depressivo
        }

        public enum Sentimento
        {
            Confuso,
            Curioso,
            Medroso,
            Entendiado,
            Desanimado
            }

        public enum EstagioVida
        {
            Ovinho, // 0
            Filhote, // 1
            Pequenino, // 2
            Grandinho, // 3
            Adultinho, // 4
            Adultao, // 5
            Velhinho, //6
            Ancestral // 7
        }

        //Construtor
        public Criatura(string nome)
        {
            //Não Pode deixar o bixinho sem nome coitado
            DefinirNome(nome);

            DataNascimento = DateTime.Now;

            FomeAtual = 20;

            EnergiaAtual = 75;

            EstagioVidaAtual = EstagioVida.Ovinho;

            SentimentoAtual = SortearSentimento();

            HumorAtual = SortearHumor();
        }

        private Sentimento SortearSentimento()
        {
            Sentimento[] Sentimentos = Enum.GetValues<Sentimento>(); //Pega todos o valores do enum e bota num array

            int indiceAleatorio = random.Next(Sentimentos.Length); //Sorteia um indice

            return Sentimentos[indiceAleatorio]; //Retorna o sentimento num indice aleatório
        }

        private Humor SortearHumor()
        {
            Humor[] Humores = Enum.GetValues<Humor>();; //Pega todos o valores do enum e bota num array

            int indiceAleatorio = random.Next(Humores.Length); //Sorteia um indice

            return Humores[indiceAleatorio];
        }

        public void DefinirNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentException("O nome não pode ser vazio.", nameof(nome));
            }

            if (nome.Equals(this.Nome, StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("O nome é o mesmo.", nameof(nome));
            }

            Nome = nome;
        }

        public void ExibirNome()
        {
            Console.WriteLine($"O nome do seu bixinho é {Nome}");
        }

        public void ExibirDataNascimento()
        {
            Console.WriteLine($"O {Nome} nasceu na data {DataNascimento}");
        }

        public void ExibirHumor()
        {
            Console.WriteLine($"{Nome} está com se sentindo {HumorAtual}");
        }

        public void ExibirSentimento()
        {
            Console.WriteLine($"{Nome} está {SentimentoAtual}");
        }

        public void ExibirEstagioVida()
        {
            Console.WriteLine($"{Nome} está na fase {EstagioVidaAtual}");
        }

        public void ExibirFomeAtual()
        {
            if (FomeAtual <= 10)
            {
                Console.WriteLine($"{Nome} está com um barrigão");
            }
            else if (FomeAtual <= 35)
            {
                Console.WriteLine($"{Nome} está cheio");
            }
            else if (FomeAtual <= 55)
            {
                Console.WriteLine($"{Nome} está satisfeito");
            }
            else if (FomeAtual <= 70)
            {
                Console.WriteLine($"{Nome} está com fominha");
            }
            else
            {
                Console.WriteLine($"{Nome} está faminto coitado");
            }
        }

        public void ExibirEnergiaAtual() 
        {
            if (EnergiaAtual <= 100 && EnergiaAtual >= 85)
            {
                Console.WriteLine($"{Nome} está elétrico cuidado");
            }
            else if (EnergiaAtual >= 60)
            {
                Console.WriteLine($"{Nome} está bem disposto");
            }
            else if (EnergiaAtual >= 40)
            {
                Console.WriteLine($"{Nome} está normal");
            }
            else if (EnergiaAtual >= 16)
            {
                Console.WriteLine($"{Nome} está cansado");
            }
            else if (EnergiaAtual > 1)
            {
                Console.WriteLine($"{Nome} está desmaiando");
            }
            else if (EnergiaAtual == 1)
            {
                Console.WriteLine($"{Nome} desmaiou");
            }
        }

        private void CalculaTempoTotalDeVida()
        {
            TempoDeVida = DateTime.Now - DataNascimento;

            DiasDeVida = TempoDeVida.Days;

            AnosDeVida = DiasDeVida / 365;

            int resto = DiasDeVida % 365;

            MesesDeVida = resto / 28;

            DiasRestantes = resto % 28;
        }

        private string FormatarUnidade(int quantidade, string singular, string plural)
        {
            if (quantidade == 1)
            {
                return $"{quantidade} {singular}";
            }
            else if (quantidade > 1)
            {
                return $"{quantidade} {plural}";
            }
            else
            {
                return "";
            }
        }

        public void ExibirTempoDeVida()
        {

            CalculaTempoTotalDeVida();

            string anos = FormatarUnidade(AnosDeVida, "ano", "anos");
            string meses = FormatarUnidade(MesesDeVida, "mês", "meses");
            string dias = FormatarUnidade(DiasRestantes, "dia", "dias");
            string horas = FormatarUnidade(TempoDeVida.Hours, "hora", "horas");
            string minutos = FormatarUnidade(TempoDeVida.Minutes, "minuto", "minutos");

            int variaveisComValor = 0;
            int textosInseridos = 0;

            string textoFinal = "O seu bixinho possui: ";

            string[] unidades = { anos, meses, dias, horas, minutos };

            foreach (var texto in unidades)
            {
                if (!string.IsNullOrWhiteSpace(texto))
                {
                    variaveisComValor++;
                }
            }

            if (variaveisComValor == 0)
            {
                textoFinal += "Menos de um minuto de vida";
            }
            else
            {
                foreach (var texto in unidades)
                {
                    if (!string.IsNullOrWhiteSpace(texto))
                    {
                        textoFinal += texto; //Adiciona X anos/ano
                        textosInseridos++;

                        if (textosInseridos == (variaveisComValor - 1))
                        {
                            textoFinal += " e ";
                        }
                        else if (textosInseridos < variaveisComValor)
                        {
                            textoFinal += ", ";
                        }
                    }
                }
            }

            Console.WriteLine(textoFinal);
        }
    }
}