﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsMVVM.Model
{
    public class Client
    {
        string id;
        string nom;
        string cognom;
        decimal saldo;

        // Añadir foto
        public string Foto {  get; set; }
        public string Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Cognom { get => cognom; set => cognom = value; }
        public decimal Saldo { get => saldo; set => saldo = value; }

        public override string ToString()
        {
            return Nom + " " + Cognom;
        }
    }
}
