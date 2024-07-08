using System;
namespace SZTF1_feleveshazi
{
    public class Idoszak
    {
        //idoszak osztály: a szabadságok kezdő és végnapja
        public int kezdonap;
        public int vegnap;

        public Idoszak(int kezdonap, int vegnap)
        {
            this.kezdonap = kezdonap;
            this.vegnap = vegnap;
        }

        //ToString metodus fellulirasa az outpothoz
        public override string ToString()
        {
            return $"{this.kezdonap} {this.vegnap}";
        }

    }
}