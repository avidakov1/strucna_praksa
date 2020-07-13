using System;


//abstract class DaniPosao
//{
//    public float KolicinaPosla;
//    public abstract float PortebnoVrijeme();
//    public void Zvuk()
//    {
//        Console.WriteLine("1,2,3.")
//    }
//}

//class ObavljanjePosla : DaniPosao
//{
//    private string NazivRadnje;
//    private float BrzinaObavljanjaPosla;
//    public override float PortebnoVrijeme() => KolicinaPosla / BrzinaObavljanjaPosla;

//    public string Name
//    {
//        get
//        {
//            return NazivRadnje;
//        }

//        set
//        {
//            NazivRadnje = value;
//        }
//    }
//    public float Speed
//    {
//        get
//        {
//            return BrzinaObavljanjaPosla;
//        }

//        set
//        {
//            BrzinaObavljanjaPosla = value;
//        }
//    }

//}

class Post
{
    public String BuildingAddress;
}
interface IPrintData
{
    void PrintData();
}
class Package : Post
{
    public String Sender;
    public String MailingAddress;
    public bool Express;
    public bool Delivered;
    public String DateMailed;
    public String DateDelivered;
    public Package(String S, String Ma, bool E, bool D, String Dm)
    {
        Sender = S; MailingAddress = Ma; Express = E; Delivered = D; DateMailed = Dm; 
    }
}

class Box : Package
{
    public char BoxSize;
    public float Weight;
    public bool Breakable;
    public Box(String S, String Ma, bool E, bool D, String Dm, char Bs, float W, bool Br): base(S,Ma,E,D,Dm)
    {
        BoxSize = Bs; Weight = W; Breakable = Br;
    }
}

class Letter : Package
{
    public char LetterSize;
    public bool Bill;
    public Letter(String S, String Ma, bool E, bool D, String Dm, char Ls, bool B) : base(S, Ma, E, D, Dm)
    {
        LetterSize = Ls; Bill = B;
    }
}

class MailMan : Post
{
    public String Name;
}

class Vehicle
{
    public virtual void HornSound()
    {
        Console.WriteLine("Generic horn sound.");
    }
}

class Motorcycle : Vehicle
{
    public override void HornSound()
    {
        Console.WriteLine("Bonk");
    }
}
class Van : Vehicle
{
    public override void HornSound()
    {
        Console.WriteLine("Honk");
    }
}

class VanMailman : Van
{
    MailMan mailman = new MailMan();
}
class MotorMailman : Motorcycle
{
    MailMan mailman = new MailMan();
}

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Obavljanje_posla obj = new Obavljanje_posla();
            //obj.Kolicina_posla = 50;
            //obj.Speed = 30;
            //obj.Name = "Bager";
            //Console.WriteLine("Količina posla: " + obj.Kolicina_posla);
            //Console.WriteLine("Ime obavljanja posla: "+ obj.Name);
            //Console.WriteLine("Brzina obavljanja posla: "+ obj.Speed);
            //Console.WriteLine("Potrebno vrijeme: "+ obj.Portebno_vrijeme());
        }
    }
}
