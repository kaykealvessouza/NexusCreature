namespace TamagochiCSharp
{
    public class Criatura
    {

        private static readonly Random random = new Random();

        public string Nome { get; private set; }

        public DateTime DataNascimento { get; private set; }
        public TimeSpan TempoDeVida { get; private set; }
        public int DiasDeVida {get; private set;}

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
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentException("O nome não pode ser vazio.");
            }

            Nome = nome;
            DataNascimento = DateTime.Now;

            FomeAtual = 20;

            EnergiaAtual = 75;

            EstagioVidaAtual = EstagioVida.Ovinho;

            SentimentoAtual = SortearSentimento();

            HumorAtual = SortearHumor();

        }

        public Sentimento SortearSentimento()
        {
            Sentimento[] Sentimentos = Enum.GetValues<Sentimento>(); //Pega todos o valores do enum e bota num array

            int indiceAleatorio = random.Next(Sentimentos.Length); //Sorteia um indice

            return Sentimentos[indiceAleatorio]; //Retorna o sentimento num indice aleatório
        }

        public Humor SortearHumor()
        {
            Humor[] Humores = Enum.GetValues<Humor>();; //Pega todos o valores do enum e bota num array

            int indiceAleatorio = random.Next(Humores.Length); //Sorteia um indice

            return Humores[indiceAleatorio];
        }

        public void DefinirNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentException("O nome não pode ser vazio.");
            }

            Nome = nome;
        }

        public void ExibirNome()
        {
            Console.WriteLine($"O nome do seu bixinho é {Nome}");
        }

        public void CalculaTempoTotalDeVida()
        {
            TempoDeVida = DateTime.Now - DataNascimento;

            DiasDeVida = TempoDeVida.Days;
        }

        public List<int> CalculandoListaTempoTotalDeVida()
        {

            CalculaTempoTotalDeVida(); //Chama método que atualiza o tempo de vida e transforma em dias

            List<int> ListaTemposDeVida = new List<int>();

            int AnosDeVida = DiasDeVida / 365; //Calcula os anos

            ListaTemposDeVida.Add(AnosDeVida); //Insere anos na posição 0

            int resto = DiasDeVida % 365; //Pega o que sobrou que não virou ano

            int MesesDeVida = resto / 28; //Vira meses

            ListaTemposDeVida.Add(MesesDeVida); //Coloca meses na posição 1

            int DiasRestantes = resto % 28; //Dias restante são o que sobrou

            ListaTemposDeVida.Add(DiasRestantes); //Coloca na posição 2

            return ListaTemposDeVida;
        }

        public void ExibirTempoDeVida()
        {

            List<int> ListaTempoTotalDeVida = CalculandoListaTempoTotalDeVida();

            Console.WriteLine("O seu bixinho possui:");

            if(ListaTempoTotalDeVida[0] == 1)//0 é ano
            {
                Console.WriteLine($"1 ano;");
            }
            else if(ListaTempoTotalDeVida[0] > 1)
            {
                Console.WriteLine($"{ListaTempoTotalDeVida[0]} anos;");
            }

            if(ListaTempoTotalDeVida[1] == 1)//1 é mes
            {
                Console.WriteLine($"1 mês;");
            }
            else if (ListaTempoTotalDeVida[1] > 1)
            {
                Console.WriteLine($"{ListaTempoTotalDeVida[1]} meses;");
            }

            if(ListaTempoTotalDeVida[2] == 1)//2 é dia
            {
                Console.WriteLine($"1 dia;");
            }
            else if (ListaTempoTotalDeVida[2] > 1)
            {
                Console.WriteLine($"{ListaTempoTotalDeVida[2]} dias;");
            }

            if(TempoDeVida.Hours == 1)
            {
                Console.WriteLine($"1 hora;");
            }
            else if (TempoDeVida.Hours > 1)
            {
                Console.WriteLine($"{TempoDeVida.Hours} horas;");
            }

            if(TempoDeVida.Minutes == 1)
            {
                Console.WriteLine($"1 minuto;");
            }
            else if(TempoDeVida.Minutes > 1)
            {
                Console.WriteLine($"{TempoDeVida.Minutes} minutos;");
            }

            if(TempoDeVida.Seconds == 1)
            {
                Console.WriteLine($"e 1 segundo de vida.");
            }
            else if (TempoDeVida.Seconds > 1)
            {
                Console.WriteLine($"e {TempoDeVida.Seconds} segundos de vida.");
            }
        }
    }
}