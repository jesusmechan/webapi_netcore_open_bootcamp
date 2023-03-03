using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ClaseResultado<T>
    {
        private T? _Entidad;
        private List<T>? _Lista;

        public T Entidad
        {
            get { return _Entidad; }
            set { _Entidad = value; }
        }

        public List<T> Lista
        {
            get { return _Lista; }
            set { _Lista = value; }


        }

        public bool HuboError { get; set; }
        public string Mensaje { get; set; }

        public int UltimoId { get; set; }

    }
}
