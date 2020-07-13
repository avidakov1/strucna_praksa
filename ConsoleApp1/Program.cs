using System;
abstract class Dani_posao
{
    public float Kolicina_posla;
    public abstract float Portebno_vrijeme();
}

class Obavljanje_posla : Dani_posao
{
    private string Naziv_radnje;
    private float Brzina_obavljanja_posla;
    public override float Portebno_vrijeme() => Kolicina_posla / Brzina_obavljanja_posla;

    public string Name
    {
        get
        {
            return Naziv_radnje;
        }

        set
        {
            Naziv_radnje = value;
        }
    }
    public float Speed
    {
        get
        {
            return Brzina_obavljanja_posla;
        }

        set
        {
            Brzina_obavljanja_posla = value;
        }
    }
    
}


namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Obavljanje_posla obj = new Obavljanje_posla();
            obj.Kolicina_posla = 50;
            obj.Speed = 30;
            obj.Name = "Bager";
            Console.WriteLine("Količina posla: " + obj.Kolicina_posla);
            Console.WriteLine("Ime obavljanja posla: "+ obj.Name);
            Console.WriteLine("Brzina obavljanja posla: "+ obj.Speed);
            Console.WriteLine("Potrebno vrijeme: "+ obj.Portebno_vrijeme());
        }
    }
}
