﻿using System;

namespace Zarwin.Core
{
    public class Wave
    {
        private List<Zombie> zombies;
        private City city;
        private Stack<Round> rounds;

        public Wave(List<Zombie> zombies, City city)
        {
            this.zombies = zombies;
            this.city = city;
            this.rounds = new List<Round>();

            //Add rounds
            this.rounds.Push(new WarnRound());

            this.AddSoldiersZombieRound();
        }

        public void Run()
        {
            //Beginning of the wave
            this.BeginningWave();

            int i = 1;
            Round currentRound;
            //Rounds
            while (this.zombies.size() > 0)
            {
                Console.WriteLine("Round {0}", i);

                currentRound = this.rounds.Pop();
                currentRound.Run();

                Console.ReadLine();
            }

            //End of the wave
            this.EndWave();
        }

    }

    private void BeginningWave()
    {
        Console.WriteLine("Beginning of the wave");
        Console.ReadLine();
    }

    private void EndWave()
    {
        Console.WriteLine("End of the wave");
        Console.ReadLine();
    }

    public void AddRound(List<Round> newRounds)
    {
        foreach (Round round in newRounds)
        {
            this.rounds.Push(round);
        }
    }

    public void AddSoldiersZombieRound()
    {
        foreach(Soldier sold in city.GetSoldierAlive()){
                this.rounds.Push(new SoldierRound(sold));
            }
            this.rounds.Push(new ZombieRound());
    }
}