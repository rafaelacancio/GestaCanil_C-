using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoCanil
{
    public class Cao : Animal
    {
        private string raca = string.Empty;
        private string porte = string.Empty;

        public Cao() { }

        public Cao(int id, string nome, int idade, double peso, string raca, string porte)
            : base(id, nome, idade, peso)
        {
            this.raca = raca;
            this.porte = porte;
        }
        public Cao(string nome, int idade, string raca) : base()
        {
            this.Nome = nome;
            this.Idade = idade;
            this.Raca = raca;
        }

        public string Raca
        {
            get { return raca; }
            set { raca = value; }
        }

        public string Porte
        {
            get { return porte; }
            set { porte = value; }
        }



        public override string ToString()
        {
            return base.ToString() + $"\nRaça: {raca}\nPorte: {porte}";
        }
    }
}
