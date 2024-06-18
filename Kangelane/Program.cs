using System;
using System.Collections.Generic;
using System.IO;

class Hero
{
    public string Name { get; set; }
    public string Location { get; set; }

    public Hero(string name, string location)
    {
        Name = name;
        Location = location;
    }

    public virtual int Save(int dangerPeople)
    {
        return (int)Math.Round(dangerPeople * (0.95));
    }

    public override string ToString()
    {
        return $"Name: {Name}, Location: {Location}";
    }
}

class SuperHero : Hero
{
    public double SkillLevel { get; }

    public SuperHero(string name, string location, double skillLevel) : base(name, location)
    {
        SkillLevel = skillLevel;
    }

    public override int Save(int dangerPeople)
    {
        return (int)Math.Round(dangerPeople * (0.95) + SkillLevel);
    }

    public override string ToString()
    {
        return base.ToString() + $", Skill Level: {SkillLevel}";
    }
}

class Program
{
    static List<Hero> heroesList = new List<Hero>();

    static void Main(string[] args)
    {
        ReadHeroesFromFile("../../../heroes.txt");

        foreach (var hero in heroesList)
        {
            Console.WriteLine(hero.ToString());
            if (hero is SuperHero)
            {
                SuperHero superHero = (SuperHero)hero;
                int inDanger = 666;
                Console.WriteLine($"People in Danger: {inDanger}");
                int savedPeople = superHero.Save(inDanger);
                Console.WriteLine($"Saved people: {savedPeople}");
            }
            else
            {
                int inDanger = 666;
                Console.WriteLine($"People in Danger: {inDanger}");
                int savedPeople = hero.Save(inDanger);
                Console.WriteLine($"Saved people: {savedPeople}");
            }
            Console.WriteLine();
        }
    }

    static void ReadHeroesFromFile(string fileName)
    {
        try
        {
            using (StreamReader herofile = new StreamReader(fileName))
            {
                string line;
                while ((line = herofile.ReadLine()) != null)
                {
                    string[] data = line.Split('/');
                    string name = data[0].Trim();
                    string location = data[1].Trim();

                    if (name.EndsWith("*"))
                    {
                        int skillLevel = new Random().Next(1, 6); 
                        heroesList.Add(new SuperHero(name.TrimEnd('*'), location, skillLevel));
                    }
                    else
                    {
                        heroesList.Add(new Hero(name, location));
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
    }
}
