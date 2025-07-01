using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Runtime.ConstrainedExecution;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace ex03_LINQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Exercice 1 : Requêtes simples");

            List<int> entiers = new List<int> { 4, 5, 2, 3, 1, 1, 0, 5, 8, 9, 10, 15, 16, 20, 21, 4, 5 };

            Console.WriteLine("\r\n1.1: Récupérer et afficher tous les nombres supérieurs à 5");
            entiers.Where(x => x > 5)
                .ToList().ForEach(x => Console.Write($"{x} "));

            Console.WriteLine("\r\n\n1.2: Récupérer et afficher les nombres supérieurs ou égaux à 15 et inférieurs à 20");
            entiers.Where(x => x >= 15 && x < 20)
                .ToList().ForEach(x => Console.Write($"{x} "));

            Console.WriteLine("\r\n\n1.3: Récupérer et afficher les nombres supérieurs à 2, qui sont des multiples de 2, inférieurs à 20, différents de 8");
            entiers.Where(x => x > 2 && x % 2 == 0 && x < 20 && x != 8)
                .ToList().ForEach(x => Console.Write($"{x} "));

            List<string> fruits = new List<string> { "Banane", "Ananas", "Cerise", "Framboise", "Groseilles", "Pomme", "Poire", "Tomate", "Kiwi", "Raisin", "Mangue", "Datte" };

            Console.WriteLine("\r\n\n1.4: Récupérer et afficher tous les fruits dont le nom contient plus de 5 lettres");
            fruits.Where(fruit => fruit.Length > 5)
                .ToList().ForEach(fruit => Console.Write($"{fruit} "));

            Console.WriteLine("\r\n\n1.5: Récupérer et afficher tous les fruits dont le nom commence par 'P', dont la longueur du nom est supérieure à 4, qui contiennent un 'o' mais pas de 'm'");
            fruits.Where(fruit => fruit.StartsWith("P") && fruit.Length > 4 && fruit.Contains("o") && !fruit.Contains("m"))
                .ToList().ForEach(fruit => Console.Write($"{fruit} "));

            Console.WriteLine("\r\n\n1.6: Trier et afficher les fruits selon leur longueur");
            fruits.OrderBy(fruit => fruit.Length)
                .ToList().ForEach(fruit => Console.Write($"{fruit} "));



            Console.WriteLine("\r\n__________________________________________________");

            Console.WriteLine("\r\nExercice 2 : Requêtes sur les objets");

            List<Dog> dogs = new List<Dog>
            {
                new Dog("Berger Australien", "Banzaï", 1, 28),
                new Dog("Berger Australien", "Letto", 3, 30),
                new Dog("Berger Australien", "Princesse", 8, 32),
                new Dog("Berger Allemand", "Floyd", 10, 32),
                new Dog("Caniche", "Igor", 13, 9),
                new Dog("Labrador", "Swing", 15, 25),
                new Dog("Teckel", "Wonki", 2, 5),
                new Dog("Terre Neuve", "Albator", 1, 50),
                new Dog("Carlin", "Pataud", 13, 10),
                new Dog("Boxer", "Frank", 6, 25),
                new Dog("Lévrier Afghan", "Précieuse", 9, 26),
                new Dog("Yorkshire", "Kakou", 3, 6)
            };

            Console.WriteLine("\r\n2.1: Récupérer et afficher tous les noms des chiens qui sont de la race 'Berger Australien'");
            dogs.Where(dog => dog.Breed == "Berger Australien")
                .Select(dog => dog.Name)
                .ToList().ForEach(name => Console.Write($"{name} "));

            Console.WriteLine("\r\n\n2.2: Récupérer et afficher tous les noms des chiens qui sont de la race 'Berger Australien' et les trier par leur nom");
            dogs.Where(dog => dog.Breed == "Berger Australien")
                .OrderBy(dog => dog.Name)
                .Select(dog => dog.Name)
                .ToList().ForEach(name => Console.Write($"{name} "));

            Console.WriteLine("\r\n\n2.3: Récupérer et afficher tous les noms des chiens âgés de 5 ans et plus, dont la longueur du nom est supérieure à 5 lettres. Les trier par leur poids");
            dogs.Where(dog => dog.Age >= 5 && dog.Name.Length > 5)
                .OrderBy(dog => dog.Weight)
                .Select(dog => dog.Name)
                .ToList().ForEach(name => Console.Write($"{name} "));

            Console.WriteLine("\r\n\n2.4: Récupérer et afficher tous les noms des chiens par leur âge(tri décroissant) puis leur poids(tri croissant)");
            dogs.Where(dog => dog.Age >= 0)
                .OrderByDescending(dog => dog.Age)
                .ThenBy(dog => dog.Weight)
                .Select(dog => dog.Name)
                .ToList().ForEach(name => Console.Write($"{name} "));

            Console.WriteLine("\r\n\n2.5: Récupérer et afficher tous les noms des chiens dont le nom de race tient en un seul mot, leur poids doit être supérieur à 15 kilos, leur nom doit contenir un 'i' et les trier par la longueur de leur prénom");
            dogs.Where(dog => !dog.Breed.Contains(" ") && dog.Weight > 15 && dog.Name.Contains("i"))
                .OrderBy(dog => dog.Name.Length)
                .Select(dog => dog.Name)
                .ToList().ForEach(name => Console.Write($"{name} "));



            Console.WriteLine("\r\n__________________________________________________");

            Console.WriteLine("\r\nExercice 3 : Requêtes créant de nouveaux objets");

            List<Personne3> personnes3 = new List<Personne3>
            {
                new Personne3("Hallyday", "Johnny", false),
                new Personne3("Vartan", "Sylvie", false),
                new Personne3("Drucker", "Michel", false),
                new Personne3("Antoine", "Antoine", true),
                new Personne3("Philippe", "Edouard", false),
                new Personne3("Demorand", "Patricia", true),
                new Personne3("Ulysse", "Margareth", true),
                new Personne3("Zenith", "Méryl", true),
                new Personne3("Bobo", "Jojo", false)
            };

            Console.WriteLine("\r\n3.1: Créer un itérable d'ingénieurs, trier par nom, et ensuite par prénom");
            personnes3.Where(p => p.EstIngenieur)
                .OrderBy(p => p.Nom)
                .ThenBy(p => p.Prenom)
                .ToList().ForEach(p => Console.WriteLine($"{p.Nom} {p.Prenom}"));

            Console.WriteLine("\r\n\n3.2: Récupérer et afficher la liste des personnes qui ne sont pas ingénieures");
            personnes3.Where(p => !p.EstIngenieur)
                .ToList().ForEach(p => Console.WriteLine($"{p.Nom} {p.Prenom}"));

            Console.WriteLine("\r\n\n3.3: Créer une liste d'objets anonymes (Ingénieurs + techniciens)");
            var ingenieursEtTechniciens = personnes3
                .Where(p => p.EstIngenieur)
                .Select(p => new { p.Nom, p.Prenom, p.EstIngenieur })
                .ToList();
            ingenieursEtTechniciens.ForEach(p => Console.WriteLine($"{p.Nom} {p.Prenom}"));



            Console.WriteLine("\r\n__________________________________________________");

            Console.WriteLine("\r\nExercice 4 : Requêtes et variables");

            List<Personne4> personnes4 = new List<Personne4>
            {
                new Personne4("Beauvoir", "Simon", 16, "M"),
                new Personne4("Beauvoir", "Simone", 25, "F"),
                new Personne4("De Caunes", "Richard", 41, "M"),
                new Personne4("Sullivan", "Sullivan", 31, "M"),
                new Personne4("Rémy", "Camille", 22, "F"),
                new Personne4("Manchon", "Camille", 19, "M"),
                new Personne4("Thiebaud", "Marie", 61, "F"),
                new Personne4("Crouchon", "Mélanie", 55, "F"),
                new Personne4("Baline", "Mélodie", 74, "F"),
                new Personne4("Karine", "Pascal", 31, "M"),
                new Personne4("Katherine", "Pascale", 36, "F"),
                new Personne4("Zoula", "Charles", 20, "M"),
                new Personne4("Romain", "Collin", 34, "M"),
                new Personne4("Fouchard", "Aïcha", 48, "F"),
                new Personne4("Blandine", "Maëva", 18, "F")
            };

            Console.WriteLine("\r\n4.1: Créer une variable nom_complet = Nom + ' ' + Prenom et la mettre comme seule attribut de l'objet créé dans le select et les afficher");
            personnes4.Select(p => new { nom_complet = p.Nom + " " + p.Prenom })
                .ToList().ForEach(p => Console.WriteLine(p.nom_complet));

            Console.WriteLine("\r\n\n4.2: Pour les personnes majeures ayant moins de 50 ans:"
                        + "\r\n- Créer une variable 'initiale' qui contient seulement les initiales du nom et du prénom : p.Nom[0] + '.' + p.Prenom[0]"
                        + "\r\n- Créer une variable taille_nom_complet = longueur du prénom +longueur du nom"
                        + "\r\n- Créer un objet anonyme avec les attributs: Nom, prénom, initiale, taille_nom_complet, age"
                        + "\r\n- Et les afficher");
            personnes4.Where(p => p.Age >= 18 && p.Age < 50).ToList()
                .Select(p => new
                {
                    p.Nom,
                    p.Prenom,
                    initiale = $"{p.Nom[0]}.{p.Prenom[0]}",
                    taille_nom_complet = p.Nom.Length + p.Prenom.Length,
                    p.Age
                })
                .ToList().ForEach(p => Console.WriteLine($"{p.Nom} {p.Prenom}, {p.initiale}, {p.taille_nom_complet}, {p.Age}"));




            Console.WriteLine("\r\n__________________________________________________");

            Console.WriteLine("\r\nExercice 5 : Requêtes et listes à 2 dimensions");

            List<List<Personne5>> personnes5 = new List<List<Personne5>>
            {
                new List<Personne5>() { new Personne5("Drucker", "Michel"),
                                        new Personne5("Bedia", "Ramzy"),
                                        new Personne5("Judor", "Eric") },

                new List<Personne5>() { new Personne5("Diaz", "Cameron"),
                                        new Personne5("Depardieu", "Gerard"),
                                        new Personne5("Stallone", "Sylvester"),
                                        new Personne5("Macron", "Emmanuel") },

                new List<Personne5>() { new Personne5("Benzema", "Karim"),
                                        new Personne5("Antoine", "Eric"),
                                        new Personne5("Ruiz", "Olivia"),
                                        new Personne5("Clavier", "Christian"),
                                        new Personne5("Einstein", "Albert") }
            };

            Console.WriteLine("\r\n5.1: Récupérer et afficher tous les noms dont la longueur est supérieure à 5");
            personnes5.SelectMany(liste => liste)
                .Where(p => p.Nom.Length > 5)
                .Select(p => p.Nom)
                .ToList().ForEach(nom => Console.Write($"{nom} "));

            Console.WriteLine("\r\n\n5.2: Récupérer toutes personnes dont le nom contient un 'e' et dont le prénom contient un 'a'"
                + "\r\n- Trier les personnes par leur nom (décroissant)"
                + "\r\n- Leur réer un objet anonyme avec un attribut identite = prénom+' '+nom"
                + "\r\n- Et afficher tous les 'identite'");
            personnes5.SelectMany(liste => liste)
                .Where(p => p.Nom.Contains("e") && p.Prenom.Contains("a"))
                .OrderByDescending(p => p.Nom)
                .Select(personnes5 => new { identite = personnes5.Prenom + " " + personnes5.Nom })
                .ToList().ForEach(p => Console.WriteLine(p.identite));

            Console.WriteLine("\r\n\n5.3: Récupérer toutes les listes qui contiennent plus de 4 personnes"
                + "\r\n- En extraire les personnes dont le nom commence par 'A', 'B' ou 'C'"
                + "\r\n- Trier les personnes par leur prénom (croissant)"
                + "\r\n- Leur créer un objet anonyme avec l'attribut 'initiale' = 1ère lettre du prénom+'.'+1ère lettre du nom"
                + "\r\n- Et afficher tous les 'initiale'");
            personnes5.Where(liste => liste.Count > 4).ToList()
                .SelectMany(liste => liste)
                .Where(p => p.Nom.StartsWith("A") || p.Nom.StartsWith("B") || p.Nom.StartsWith("C"))
                .OrderBy(p => p.Prenom)
                .Select(p => new { initiale = $"{p.Prenom[0]}.{p.Nom[0]}" })
                .ToList().ForEach(p => Console.WriteLine(p.initiale));

            Console.WriteLine("\r\n\n5.4: Récupérer toutes les listes qui contiennent moins de 5 personnes et afficher toutes les personnes comme ceci : Nom+' '+Prenom");
            personnes5.Where(liste => liste.Count < 5)
                .SelectMany(liste => liste)
                .ToList().ForEach(p => Console.WriteLine($"{p.Nom} {p.Prenom}"));



            Console.WriteLine("\r\n__________________________________________________");

            Console.WriteLine("\r\nExercice 6 : Requêtes et group by");

            List<Personne4> personnes6 = new List<Personne4>
            {
                new Personne4("Garett", "Ramzy", 45, "M"),
                new Personne4("Caire", "Joe", 35, "M"),
                new Personne4("Clay", "Alicia", 18, "F"),
                new Personne4("Bavette", "Simone", 68, "F"),
                new Personne4("Henry", "Thierry", 44, "M"),
                new Personne4("Jacquesonne", "Janett", 25, "F"),
                new Personne4("Buveur", "Joe", 25, "M"),
                new Personne4("Louet", "Karim", 31, "M"),
                new Personne4("Louette", "Karima", 31, "F"),
                new Personne4("Caire", "Paul", 19, "M"),
                new Personne4("Mille", "Camille", 20, "F"),
                new Personne4("Cent", "Camille", 40, "F"),
                new Personne4("Million", "Camille", 60, "M"),
                new Personne4("Gold", "Roger", 17, "M"),
                new Personne4("Lion", "Sandra", 8, "F"),
                new Personne4("René", "Jean", 6, "M")
            };

            Console.WriteLine("\r\n6.1: Faire un group by sur le genre (sexe) des personnes présentes dans la liste d'objets Personne()");
            personnes6.GroupBy(p => p.Genre)
                .ToList().ForEach(g => Console.WriteLine($"Genre: {g.Key}, Nombre de personnes: {g.Count()}"));

            Console.WriteLine("\r\n\n6.2: Faire un group by sur l'âge des personnes les trier par âge croissant");
            personnes6.GroupBy(p => p.Age)
                .OrderBy(g => g.Key)
                .ToList().ForEach(g => Console.WriteLine($"Âge: {g.Key}, Nombre de personnes: {g.Count()}"));

            Console.WriteLine("\r\n\n6.3: Faire un group by sur le prénom des personnes, et afficher les noms de famille par prénom."
                + "\r\n- Récupérer les personnes majeures (18 ans et plus)"
                + "\r\n- Les trier par leur prénom en ordre décroissant"
                + "\r\n- Et les afficher");
            personnes6.GroupBy(p => p.Prenom)
                .Where(g => g.Any(p => p.Age >= 18))
                .OrderByDescending(g => g.Key)
                .ToList().ForEach(g => 
                {
                    Console.WriteLine($"Prénom: {g.Key}");
                    g.ToList().ForEach(p => Console.WriteLine($"  Nom: {p.Nom}"));
                });


            List<int> nombres = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 20, 11, 13, 12, 14, 18, 17, 16, 14, 14 };

            Console.WriteLine("\r\n\n6.4: Grouper les éléments d'une liste de nombres. D'un côté les chiffres/nombres pairs, de l'autre ceux impairs");
            nombres.GroupBy(n => n % 2 == 0 ? "Pairs" : "Impairs")
                .ToList().ForEach(g => Console.WriteLine($"{g.Key}: {string.Join(", ", g)}"));

            Console.WriteLine("\r\n\n6.5: Grouper les individus par la première lettre de leur nom et faire un tri croissant sur l'attribut Nom de la classe Personne");
            personnes6.OrderBy(p => p.Nom).GroupBy(p => p.Nom[0])
                .ToList().ForEach(g => Console.WriteLine($"{g.Key}, Nombre de personnes: {g.Count()}"));



            Console.WriteLine("\r\n__________________________________________________");

            Console.WriteLine("\r\nExercice 7 : Requêtes et group by multiple");

            List<Chien> chiens7 = new List<Chien>()
            {
                new Chien("Gnocci", "Gnoc Gnoc", "Labrador", "Sable", "M", 1, 20),
                new Chien("Vagabond", "Gros Loup", "Labrador", "Noir", "M", 8, 25),
                new Chien("Milou", "Milos", "Labrador", "Sable", "M", 10, 24),
                new Chien("Sirène", "Sissy", "Labrador", "Sable","F", 4, 19),
                new Chien("Félicia", "Felicci", "Labrador", "Sable", "F", 6, 20),
                new Chien("Kratos", "Mon tueur", "Chihuahua", "Fauve", "M", 1, 2),
                new Chien("Jack", "Jaja", "Chihuahua", "Fauve", "M", 1, 2),
                new Chien("Mojave", "Mojojo", "Chihuahua", "Fauve", "M", 1, 2),
                new Chien("Hercule", "Herc", "Chihuahua", "Beige", "M", 35, 2),
                new Chien("Médusa", "Med", "Terre-Neuve", "Noire", "F", 6, 40),
                new Chien("Mélusine", "Mel", "Terre-Neuve", "Noire", "F", 7, 41),
                new Chien("Venus", "Violette", "Terre-Neuve", "Noire", "F", 8, 38),
                new Chien("Letto", "Lele", "Berger Australien", "Bleu Merle", "M", 3, 30),
                new Chien("Cabron", "Dum dum", "Berger Australien", "Bleu Merle", "M", 9, 31),
                new Chien("Banzaï", "Zaïzaï", "Berger Australien", "Noir et blanc", "M", 1, 28 ),
                new Chien("Haricot", "Harry", "Berger Australien", "Noir et blanc", "M", 2, 27),
                new Chien("Gédéon", "Gégé", "Berger Allemand", "Noir et feu", "M", 9, 31),
                new Chien("Bella", "Belbel", "Berger Allemand", "Noir et feu", "F", 5, 28),
                new Chien("Oui-oui", "oui", "Berger Allemand", "Sable", "M", 7, 35),
                new Chien("Pataud", "Patoche", "Carlin", "Sable", "M", 16, 8),
                new Chien("Killer", "Kiki", "Carlin", "Sable", "M", 10, 8),
                new Chien("Frank", "Colonel", "Carlin", "Noir", "M", 9, 9)
            };

            Console.WriteLine("\r\n7.1: Faire un group by multiple sur la race et la couleur et trier par ordre croissant la race, puis la couleur");
            chiens7.GroupBy(c => new { c.Race, c.Couleur })
                .OrderBy(g => g.Key.Race)
                .ThenBy(g => g.Key.Couleur)
                .ToList().ForEach(g => Console.WriteLine($"{g.Key.Race}, {g.Key.Couleur}"));

            Console.WriteLine("\r\n\n7.2: Faire un group by multiple sur la couleur et le sexe et trier par ordre croissant sur le sexe");
            chiens7.GroupBy(c => new { c.Couleur, c.Genre })
                .OrderBy(g => g.Key.Genre)
                .ToList().ForEach(g => Console.WriteLine($"{g.Key.Couleur}, {g.Key.Genre}"));

            Console.WriteLine("\r\n\n7.3: Faire un group by par sexe, age, couleur"
                + "\r\n- Pour les chiens âgés entre 2 et 15 ans (inclus)"
                + "\r\n- Sélectionner les chiens dont le surnom n'est pas un nom composé (on cherche les surnoms sans espace)"
                + "\r\n- Trier par sexe (croissant), par âge (décroissant), par race (décroissant), par couleur (croissant)");
            chiens7.GroupBy(c => new { c.Genre, c.Age, c.Couleur })
                .Where(g => g.Key.Age >= 2 && g.Key.Age <= 15 && g.Any(c => !c.Surnom.Contains(" ")))
                .OrderBy(g => g.Key.Genre)
                .ThenByDescending(g => g.Key.Age)
                .ThenByDescending(g => g.Select(c => c.Race).FirstOrDefault())
                .ThenBy(g => g.Key.Couleur)
                .ToList().ForEach(g => 
                {
                    Console.WriteLine($"Genre: {g.Key.Genre}, Âge: {g.Key.Age}, Race: {g.Select(c => c.Race).FirstOrDefault()}, Couleur: {g.Key.Couleur}");
                    g.ToList().ForEach(c => Console.WriteLine($"  Nom: {c.Nom}, Surnom: {c.Surnom}"));
                });



            Console.WriteLine("\r\n__________________________________________________");

            Console.WriteLine("\r\nExercice 8 : Requêtes, group by et into");

            List<Chien> chiens8 = new List<Chien>()
            {
                new Chien("Gnocci", "Gnoc Gnoc", "Labrador", "Sable", "M", 1, 20),
                new Chien("Vagabond", "Gros Loup", "Labrador", "Noir", "M", 8, 25),
                new Chien("Milou", "Milos", "Labrador", "Sable", "M", 10, 24),
                new Chien("Sirène", "Sissy", "Labrador", "Sable","F", 4, 19),
                new Chien("Félicia", "Felicci", "Labrador", "Sable", "F", 6, 20),
                new Chien("Kratos", "Mon tueur", "Chihuahua", "Fauve", "M", 1, 2),
                new Chien("Jack", "Jaja", "Chihuahua", "Fauve", "M", 1, 2),
                new Chien("Mojave", "Mojojo", "Chihuahua", "Fauve", "M", 1, 2),
                new Chien("Hercule", "Herc", "Chihuahua", "Beige", "M", 35, 2),
                new Chien("Médusa", "Med", "Terre-Neuve", "Noire", "F", 6, 40),
                new Chien("Mélusine", "Mel", "Terre-Neuve", "Noire", "F", 7, 41),
                new Chien("Venus", "Violette", "Terre-Neuve", "Noire", "F", 8, 38),
                new Chien("Letto", "Lele", "Berger Australien", "Bleu Merle", "M", 3, 30),
                new Chien("Cabron", "Dum dum", "Berger Australien", "Bleu Merle", "M", 9, 31),
                new Chien("Banzaï", "Zaïzaï", "Berger Australien", "Noir et blanc", "M", 1, 28 ),
                new Chien("Haricot", "Harry", "Berger Australien", "Noir et blanc", "M", 2, 27),
                new Chien("Gédéon", "Gégé", "Berger Allemand", "Noir et feu", "M", 9, 31),
                new Chien("Bella", "Belbel", "Berger Allemand", "Noir et feu", "F", 5, 28),
                new Chien("Oui-oui", "oui", "Berger Allemand", "Sable", "M", 7, 35),
                new Chien("Pataud", "Patoche", "Carlin", "Sable", "M", 16, 8),
                new Chien("Killer", "Kiki", "Carlin", "Sable", "M", 10, 8),
                new Chien("Frank", "Colonel", "Carlin", "Noir", "M", 9, 9)
            };

            Console.WriteLine("\r\r\n8.1: Récupérer et afficher les chiens après avoir les avoir regrouper par leur race et les avoir trié par âge croissant sans utiliser orderby");
            chiens8.Sort((c1, c2) => c1.Age.CompareTo(c2.Age));
            chiens8.GroupBy(c => c.Race)
                .ToList().ForEach(g =>
                {
                    Console.WriteLine($"Race: {g.Key}");
                    g.ToList().ForEach(c => Console.WriteLine($"  Nom: {c.Nom}, Age: {c.Age}"));
                }); 
            
            Console.WriteLine("\r\r\n8.2: Regrouper les chiens par âge dans ageChiens"
                + "\r\n-Trier les chiens par âge croissant"
                + "\r\n-Regrouper depuis ageChiens les chiens ayant un âge pair et ceux impair"
                + "\r\n-Afficher le nom des chiens impairs, puis pairs classé par age");
            chiens8.GroupBy(c => c.Age).OrderBy(g => g.Key)
                .GroupBy(g => g.Key % 2 == 0 ? "Pairs" : "Impairs")
                .ToList().ForEach(g =>
                {
                    Console.WriteLine($"{g.Key}: {g.Count()}");
                    g.ToList().ForEach(g =>
                    {
                        Console.WriteLine($"  Age: {g.Select(c => c.Age).FirstOrDefault()}");
                        foreach (var chien in g)
                        {
                            if (chien.Age == g.Select(c => c.Age).FirstOrDefault())
                            {
                                Console.WriteLine($"    Nom: {chien.Nom}");
                            }
                        }
                    });
                });

            Console.WriteLine("\r\r\n8.3: Faire un group by sur la race et la couleur des chiens et créer chiensRaceCouleur avec into, Trier les chiens par race et par couleur (ordre croissant)");
            chiens8.GroupBy(c => new { c.Race, c.Couleur }) // PAS DE INTO EN METHODE :(
                .OrderBy(g => g.Key.Race)
                .ThenBy(g => g.Key.Couleur)
                .ToList().ForEach(g => Console.WriteLine($"Race: {g.Key.Race}, Couleur: {g.Key.Couleur}"));
        }
    }
}
