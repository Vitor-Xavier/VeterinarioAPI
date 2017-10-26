
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VeterinarioAPI.Models;

namespace VeterinarioAPI.Utils
{
    public class ServicoComparer : IEqualityComparer<Servico>
    {
        public bool Equals(Servico x, Servico y)
        {
            return x.ServicoId == y.ServicoId;
        }

        public int GetHashCode(Servico obj)
        {
            return obj.GetHashCode();
        }
    }
}