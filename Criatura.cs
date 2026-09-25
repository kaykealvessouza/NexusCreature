namespace TamagochiCSharp
{
    public class Criatura
    {
        public string Nome { get; private set; }

        public DateTime Datanascimento { get; private set; }

        public int FomeAtual { get; private set; } // 0 - 10 Barrigao, 11 - 35 Cheio, 36 - 55 Satisfeito, 56 - 70 Fomezinha, 71 - 100 Faminto

        public int EnergiaAtual { get; private set; } // 100 - 85 Eletrico, 84 - 60 Disposto, 59 - 40 Normal, 39 - 15 Cansado, 15 - 1 Desmaiando, 0 Desmaiou

        public enum HumorAtual
        {
            FELIZ,
            Alegre,
            Normal,
            Tristinho,
            Triste,
            Depressivo
        }

        public enum sentimentoAtual
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
    }
}