using System;

namespace EversonTGS
{
    public class Foguete
    {
        public int? IdVoo { get; set; }
        public DateTime DataVoo { get; set; }
        public double Custo { get; set; }
        public int Distancia { get; set; }
        public string Captura { get; set; }
        public int? NivelDor { get; set; }

        public Foguete()
        {

        }
    }
}