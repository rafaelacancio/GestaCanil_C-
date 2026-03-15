using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoCanil
{
    public class Animal
    {
        private int id;
        private string nome;
        private int idade;
        private double peso;
        private DateTime dataRegistro;

        public Animal()
        {
            nome = string.Empty;
            dataRegistro = DateTime.Now;
        }

        public Animal(int id, string nome, int idade, double peso)
        {
            this.id = id;
            this.nome = nome;
            this.idade = idade;
            this.peso = peso;
            this.dataRegistro = DateTime.Now;
        }

        public Animal(string nome, int idade)
        {
            this.nome = nome;
            this.idade = idade;
            this.dataRegistro = DateTime.Now;
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public int Idade
        {
            get { return idade; }
            set { idade = value; }
        }

        public double Peso
        {
            get { return peso; }
            set { peso = value; }
        }

        public DateTime DataRegistro
        {
            get { return dataRegistro; }
            set { dataRegistro = value; }
        }

        public override string ToString()
        {
            return $"Id: {id}\nNome: {nome}\nIdade: {idade}\nPeso: {peso}\nRegistado em: {dataRegistro}";
        }
    }
}
