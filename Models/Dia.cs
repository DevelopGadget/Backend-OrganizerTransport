using System;

namespace OrganizerTransport.Models
{
    public class Dia
    {
        public DateTime Hoy { get; set; }
        public int Ida { get; set; }
        public int Venida { get; set; }

        public Dia(DateTime hoy, int ida, int venida)
        {
            this.Hoy = hoy;
            this.Ida = ida;
            this.Venida = venida;
        }
    }
}
