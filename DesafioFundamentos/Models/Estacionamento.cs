using System.Text.RegularExpressions;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal PrecoInicial = 0;
        private decimal PrecoPorHora = 0;
        private List<string> Veiculos = new List<string>();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.PrecoInicial = precoInicial;
            this.PrecoPorHora = precoPorHora;
        }

        public void AdicionarVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para estacionar:");
            var placa = FormatarPlaca(Console.ReadLine());

            var placaMercosul = Regex.IsMatch(placa, "^[A-Z]{3}-[0-9]{1}[A-Z]{1}[0-9]{2}$");
            var placaAntiga = Regex.IsMatch(placa, "^[A-Z]{3}-[0-9]{4}$");

            if (placaMercosul || placaAntiga)
            {
                if (Veiculos.Contains(placa))
                {
                    Console.WriteLine("Placa já está cadastrada no estacionamento!");
                }
                else
                {
                    Veiculos.Add(placa);
                    Console.WriteLine("Placa adicionada com sucesso!");
                }
            }
            else
            {
                Console.WriteLine("Placa é inválida!");
            }
        }

        public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para remover:");
            var placa = FormatarPlaca(Console.ReadLine());

            if (Veiculos.Any(veiculo => veiculo == placa))
            {
                Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");

                if (!int.TryParse(Console.ReadLine(), out int horas))
                {
                    Console.WriteLine("O valor de horas informado é inválido!");
                    return;
                }

                decimal valorTotal = PrecoInicial + (PrecoPorHora * horas);
                Veiculos.Remove(placa);

                Console.WriteLine($"O veículo {placa} foi removido e o preço total foi de: R$ {valorTotal}");
            }
            else
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
            }
        }

        public void ListarVeiculos()
        {
            if (Veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:");
                foreach (var veiculo in Veiculos)
                {
                    Console.WriteLine(veiculo);
                }
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }

        private string FormatarPlaca(string placa)
        {
            if (placa.IndexOf('-') != 3)
            {
                placa = placa.Replace("-", string.Empty);
                placa = placa.Insert(3, "-");
            }

            return placa.ToUpper();
        }
    }
}
