namespace ex01_Garage
{
    internal class Program
    {

        public static void Main(string[] args)
        {

            Garage garage = new Garage();

            Console.WriteLine(garage);


            Vehicule lag1 = new V_Lagouna();

            lag1.SetMoteur(new MoteurEssence("150 Chevaux", 1000));

            lag1.AddOption(new Opt_GPS());

            lag1.AddOption(new Opt_SiegeChauffant());

            lag1.AddOption(new Opt_VitreElectrique());

            garage.AddVoiture(lag1);


            Vehicule A300B_2 = new V_A300B();

            A300B_2.SetMoteur(new MoteurElectrique("1500 W", 2000));

            A300B_2.AddOption(new Opt_Climatisation());

            A300B_2.AddOption(new Opt_BarreDeToit());

            A300B_2.AddOption(new Opt_SiegeChauffant());

            garage.AddVoiture(A300B_2);


            Vehicule d4_1 = new V_D4();

            d4_1.SetMoteur(new MoteurDiesel("200 Hdi", 3000));

            d4_1.AddOption(new Opt_BarreDeToit());

            d4_1.AddOption(new Opt_Climatisation());

            d4_1.AddOption(new Opt_GPS());

            garage.AddVoiture(d4_1);


            Vehicule lag2 = new V_Lagouna();

            lag2.SetMoteur(new MoteurDiesel("500 Hdi", 4000));

            garage.AddVoiture(lag2);


            Vehicule A300B_1 = new V_A300B();

            A300B_1.SetMoteur(new MoteurHybride("ESSENCE/Electrique", 5000));

            A300B_1.AddOption(new Opt_VitreElectrique());

            A300B_1.AddOption(new Opt_BarreDeToit());

            garage.AddVoiture(A300B_1);


            Vehicule d4_2 = new V_D4();

            d4_2.SetMoteur(new MoteurElectrique("100 KW", 6000));

            d4_2.AddOption(new Opt_SiegeChauffant());

            d4_2.AddOption(new Opt_BarreDeToit());

            d4_2.AddOption(new Opt_Climatisation());

            d4_2.AddOption(new Opt_GPS());

            d4_2.AddOption(new Opt_VitreElectrique());

            garage.AddVoiture(d4_2);

            Console.WriteLine(garage);

        }

    }
}
