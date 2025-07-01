namespace ex03_LINQ
{
    internal class Dog
    {
        public string Breed { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Weight { get; set; }

        public Dog(string breed, string name, int age, int weight)
        {
            Breed = breed;
            Name = name;
            Age = age;
            Weight = weight;
        }
    }
}