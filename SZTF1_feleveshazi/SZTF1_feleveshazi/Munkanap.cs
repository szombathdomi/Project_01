using System;
namespace SZTF1_feleveshazi
{
    public class Munkanap
    {
        public bool janos; //true ha bent van, az az nincs szabadságon
        public bool terez; //true ha bent van az az nincs szabadságon

        public Munkanap(bool janos, bool terez)
        {
            this.janos = janos;
            this.terez = terez;
        }

        public bool Biztonsagos
        {
            get
            {
                return this.janos && this.terez;
            }
        }

        public bool Veszelyes
        {
            get
            {
                //=false
                return !this.janos && !this.terez;
            }
        }
    }
}