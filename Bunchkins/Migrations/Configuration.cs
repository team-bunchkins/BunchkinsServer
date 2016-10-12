namespace Bunchkins.Migrations
{
    using Domain.Cards.Door.Monsters;
    using Domain.Cards.Door.Spells;
    using Domain.Cards.Door.Spells.Curses;
    using Domain.Cards.Treasure.Equipment;
    using Domain.Cards.Treasure.Spells;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Bunchkins.Infrastructure.BunchkinsDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Bunchkins.Infrastructure.BunchkinsDataContext context)
        {
            if(context.Cards.Count() == 0)
            {

                context.Cards.Add(new BuffMonsterSpellDoorCard { Description = "Play During Combat. +5 to Monster Level.", Name = "Enraged Monster", CombatPower = 5, Type = "Spell" });
                context.Cards.Add(new BuffMonsterSpellDoorCard { Description = "Play During Combat. +5 to Monster Level,", Name = "Intelligent Monster", CombatPower = 5, Type = "Spell" });
                context.Cards.Add(new BuffMonsterSpellDoorCard { Description = "Play During Combat. +10 to Monster Level", Name = "Humongous Monster", CombatPower = 5, Type = "Spell" });
                context.Cards.Add(new BuffMonsterSpellDoorCard { Description = "Play During Combat. +10 to Monster Level", Name = "Ancient Monster", CombatPower = 5, Type = "Spell" });
                context.Cards.Add(new BuffMonsterSpellDoorCard { Description = "Play During Combat. -5 to Monster Level", Name = "Baby Monster", CombatPower = -5, Type = "Spell" });

                context.Cards.Add(new AddMonsterSpellCard { Description = "This room looks fun. OH GOD WHATS THAT?!", Name = "Wandering Monster", Type = "Spell" });

                context.Cards.Add(new ArmorRemoveMonsterCard { Description = "The biggest and most hairy of feet.", Name = "Bigfoot", Level = 12, TreasureGain = 3, LevelGain = 1, Slot = "Headgear", Type = "Monster" });
                context.Cards.Add(new ArmorRemoveMonsterCard { Description = "I wont ask how you got them. But you do have them.", Name = "Crabs", Level = 1, TreasureGain = 1, LevelGain = 1, Slot = "Footgear", Type = "Monster" });
                context.Cards.Add(new ArmorRemoveMonsterCard { Description = "You should have ducked when the egg hatched.", Name = "Face Sucker", Level = 8, TreasureGain = 2, LevelGain = 1, Slot = "Headgear", Type = "Monster" });
                context.Cards.Add(new ArmorRemoveMonsterCard { Description = "Similar to the jello your aunt makes. Only less deadly.", Name = "Drooling Slime", Level = 1, TreasureGain = 1, LevelGain = 1, Slot = "Footgear", Type = "Monster" });
                context.Cards.Add(new ArmorRemoveMonsterCard { Description = "Never try to take away his internet. Unless you enjoy shattered eardrums.", Name = "Shrieking Geek", Level = 6, TreasureGain = 2, LevelGain = 1, Slot = "Headgear", Type = "Monster" });
                context.Cards.Add(new ArmorRemoveMonsterCard { Description = "Gotta go to work. Work all day. Won't stop until they have underpants, HEY.", Name = "Underpants Gnomes", Level = 4, TreasureGain = 2, LevelGain = 1, Slot = "Footgear", Type = "Monster" });
                context.Cards.Add(new ArmorRemoveMonsterCard { Description = "Oh god, it's turbo all over again.", Name = "Snails on speed", Level = 4, TreasureGain = 1, LevelGain = 1, Slot = "Footgear", Type = "Monster" });
                context.Cards.Add(new ArmorRemoveMonsterCard { Description = "She doesn't like being put inside the pukéball.", Name = "Pukachu", Level = 6, TreasureGain = 2, LevelGain = 1, Slot = "Armor", Type = "Monster" });

                context.Cards.Add(new LevelDecreaseMonsterCard { Description = "He wants to suck your blood per se.", Name = "Wannabe Vampire", Level = 12, LevelGain = 1, TreasureGain = 4, LevelsDecreased = 3, Type = "Monster" });
                context.Cards.Add(new LevelDecreaseMonsterCard { Description = "At least you'll die at the hands of a fantastic kisser.", Name = "Tongue Demon", Level = 12, LevelGain = 1, TreasureGain = 3, LevelsDecreased = 2, Type = "Monster" });
                context.Cards.Add(new LevelDecreaseMonsterCard { Description = "Beware falling snot.", Name = "Floating Nose", Level = 10, LevelGain = 1, TreasureGain = 3, LevelsDecreased = 3, Type = "Monster" });
                context.Cards.Add(new LevelDecreaseMonsterCard { Description = "Not your average wedding venue. OR IS IT?!?!", Name = "Gazebo", Level = 8, LevelGain = 1, TreasureGain = 2, LevelsDecreased = 3, Type = "Monster" });
                context.Cards.Add(new LevelDecreaseMonsterCard { Description = "It's a duck...wait it's a seal...either way it's coming straight for you.", Name = "Platycore", Level = 6, LevelGain = 1, TreasureGain = 2, LevelsDecreased = 2, Type = "Monster" });
                context.Cards.Add(new LevelDecreaseMonsterCard { Description = "Don't harp on the details too much, they just want your flesh.", Name = "Harpies", Level = 4, LevelGain = 1, TreasureGain = 2, LevelsDecreased = 2, Type = "Monster" });
                context.Cards.Add(new LevelDecreaseMonsterCard { Description = "Look at my horse. My horse is amaz......oh it's dead.", Name = "Undead Horse", Level = 4, LevelGain = 1, TreasureGain = 2, LevelsDecreased = 2, Type = "Monster" });
                context.Cards.Add(new LevelDecreaseMonsterCard { Description = "He just wants your love.", Name = "Pit Bull", Level = 2, LevelGain = 1, TreasureGain = 1, LevelsDecreased = 2, Type = "Monster" });
                context.Cards.Add(new LevelDecreaseMonsterCard { Description = "The Kernel couldn't catch this one for proccessing.", Name = "Large Angry Chicken", Level = 2, LevelGain = 1, TreasureGain = 1, LevelsDecreased = 1, Type = "Monster" });
                context.Cards.Add(new LevelDecreaseMonsterCard { Description = "Spooky scary skeletons send shivers down your spine.", Name = "Mr.Bones", Level = 2, LevelGain = 1, TreasureGain = 1, LevelsDecreased = 2, Type = "Monster" });
                context.Cards.Add(new LevelDecreaseMonsterCard { Description = "The French say they taste like chicken.", Name = "Flying Frogs", Level = 2, LevelGain = 1, TreasureGain = 1, LevelsDecreased = 2, Type = "Monster" });
                context.Cards.Add(new LevelDecreaseMonsterCard { Description = "He took his mom to prom.", Name = "Lame Goblin", Level = 1, LevelGain = 1, TreasureGain =1, LevelsDecreased = 1, Type = "Monster" });
                context.Cards.Add(new LevelDecreaseMonsterCard { Description = "Enjoys hanging out by The Gap and putting Gaps in shoppers' heads.", Name = "Maul Rat", Level = 1, LevelGain = 1, TreasureGain = 1, LevelsDecreased = 1, Type = "Monster" });
                context.Cards.Add(new LevelDecreaseMonsterCard { Description = "Beware her drones.", Name = "Amazon", Level = 8, LevelGain = 1, TreasureGain = 2, LevelsDecreased = 3, Type = "Monster" });
                context.Cards.Add(new LevelDecreaseMonsterCard { Description = "Come on, can't you photosypmathize with it?", Name = "Potted Plant", Level = 1, LevelGain = 1, TreasureGain = 1, LevelsDecreased = 1, Type = "Monster" });
                context.Cards.Add(new LevelDecreaseMonsterCard { Description = "Their dream of flying came true after their attempt failed.", Name = "The Wight Brothers", Level = 16, LevelGain = 2, TreasureGain = 4, LevelsDecreased = 10, Type = "Monster" });

                context.Cards.Add(new DeathMonsterCard { Description = "The biggest and most hairy of feet.", Name = "Plutonium Dragon", Level = 20, TreasureGain = 5, LevelGain = 2, MinPursuitLevel = 6, Type = "Monster" });
                context.Cards.Add(new DeathMonsterCard { Description = "The first one to walk like an Egyptian.", Name = "King Tut", Level = 16, TreasureGain = 4, LevelGain = 2, MinPursuitLevel = 4, Type = "Monster" });
                context.Cards.Add(new DeathMonsterCard { Description = "Beware low flying mammals.", Name = "Hippogriff", Level = 16, TreasureGain = 4, LevelGain = 2, MinPursuitLevel = 4, Type = "Monster" });
                context.Cards.Add(new DeathMonsterCard { Description = "You better roll a 20. Oh wait....", Name = "Gelatinous Octahedron", Level = 2, TreasureGain = 1, LevelGain = 2, Type = "Monster" });
                context.Cards.Add(new DeathMonsterCard { Description = "Your life is overruled.", Name = "Lawyers", Level = 6, TreasureGain = 2, LevelGain = 1, Type = "Monster" });
                context.Cards.Add(new DeathMonsterCard { Description = "THAT'S ALOT OF ORCS!!!", Name = "3,872 Orcs", Level = 10, TreasureGain = 3, LevelGain = 1, Type = "Monster" });
                context.Cards.Add(new DeathMonsterCard { Description = "There are no words. Only screams.", Name = "Unspeakably Awful Indescribable Horror", Level = 14, TreasureGain = 4, LevelGain = 1, Type = "Monster" });
                context.Cards.Add(new DeathMonsterCard { Description = "He too is a fan of Pineapple Express.", Name = "Stoned Golem", Level = 14, TreasureGain = 4, LevelGain = 1, Type = "Monster" });
                context.Cards.Add(new DeathMonsterCard { Description = "Poorly dubbed sushi awaits your demise.", Name = "Squidzilla", Level = 18, TreasureGain = 4, LevelGain = 2, Type = "Monster" });
                context.Cards.Add(new DeathMonsterCard { Description = "You should probably let him pass.", Name = "Bullrog", Level = 18, TreasureGain = 5, LevelGain = 2, MinPursuitLevel = 5, Type = "Monster" });

                context.Cards.Add(new LoseAllEquipCurseCard { Description = "Either way, your friends still think your pretty.", Name = "Change Sex", Type = "Curse"});
                context.Cards.Add(new LoseAllEquipCurseCard { Description = "The Man has come for what you owe.", Name = "Income Tax", Type = "Curse" });

                context.Cards.Add(new LoseEquipCurseCard { Description = "It's not the good kind of sticky.", Name = "Tar Trap", Slot = "Footgear", Type = "Curse" });
                context.Cards.Add(new LoseEquipCurseCard { Description = "Standing under it with a metal helmet wasn't a good idea.", Name = "Giant Magnet", Slot = "Headgear", Type = "Curse" });
                context.Cards.Add(new LoseEquipCurseCard { Description = "You should use better dandruff shampoo to keep these away.", Name = "Chicken on your head", Slot = "Headgear", Type = "Curse" });
                context.Cards.Add(new LoseEquipCurseCard { Description = "The quality was not worth the price.", Name = "Knockoff Armor Polish", Slot = "Armor", Type = "Curse" });
                context.Cards.Add(new LoseEquipCurseCard { Description = "Probably some wizards sick joke.", Name = "Flying Hat", Slot = "Headgear", Type = "Curse" });
                context.Cards.Add(new LoseEquipCurseCard { Description = "You really don't know how to maintain your weapon do you?", Name = "Bumby Grind Stone", Slot = "1Hand", Type = "Curse" });
                context.Cards.Add(new LoseEquipCurseCard { Description = "It REALLY wanted your two hander.", Name = "Very Accurate Black Hole", Slot = "2Hands", Type = "Curse" });

                context.Cards.Add(new LoseHandCardsCurseCard { Description = "On the bright side, you can use toast as papertowels.", Name = "Butter Fingers", NumCards = 2 , Type = "Curse" });

                context.Cards.Add(new LoseLevelCurseCard { Description = "And here you thought it was a cute duck.", Name = "Duck of Doom", Levels = 2, Type = "Curse" });
                context.Cards.Add(new LoseLevelCurseCard { Description = "You used to be awsome, but you can't remember how anymore.", Name = "Amnesia", Levels = 1, Type = "Curse" });




                context.Cards.Add(new LevelUpSpellCard { Description = "It did it's job.", Name = "Potion of General Usefullness", Level = 1, Type = "Spell" });
                context.Cards.Add(new LevelUpSpellCard { Description = "You get a level only because he thinks your mom is hot.", Name = "Whine at the GM", Level = 1, Type = "Spell" });
                context.Cards.Add(new LevelUpSpellCard { Description = "Your nerd knowledge is not to be trifled with.", Name = "Invoke Obscure Rules", Level = 1, Type = "Spell" });
                context.Cards.Add(new LevelUpSpellCard { Description = "He owed you money.", Name = "Kill That Guy There", Level = 1, Type = "Spell" });
                context.Cards.Add(new LevelUpSpellCard { Description = "Your lust for digging through the remains of your enemys is....outstanding.", Name = "Corpse Harvesting", Level = 1, Type = "Spell" });
                context.Cards.Add(new LevelUpSpellCard { Description = "They ruined your picknick. MAKE THEM PAY!", Name = "Boil an Anthill", Level = 1, Type = "Spell" });
                context.Cards.Add(new LevelUpSpellCard { Description = "You deserve EVERYTHING!!!", Name = "Check Your Privilege", Level = 1, Type = "Spell" });
                context.Cards.Add(new LevelUpSpellCard { Description = "The pizza was free, but he doesn't need to know that.", Name = "Bribe GM with food", Level = 1, Type = "Spell" });
                context.Cards.Add(new LevelUpSpellCard { Description = "2 + 2 = whatever you want bro.", Name = "Convenient Addition Error", Level = 1, Type = "Spell" });

                context.Cards.Add(new EscapeAllSpellCard { Description = "When in doubt, BAIL!!!", Name = "Invisibility Potion", Gold = 200, Type = "Spell" });
                context.Cards.Add(new EscapeAllSpellCard { Description = "Why can't we be friends? Oh, we can.", Name = "Friendship Potion", Gold = 200, Type = "Spell" });
                context.Cards.Add(new EscapeAllSpellCard { Description = "From the makers of Instant Coffee and Faster DMV Lines.", Name = "Instant Wall", Gold = 300, Type = "Spell" });

                context.Cards.Add(new VanquishMonsterSpellCard { Description = "That golem looks tough...aaaaannnd its a rooster.", Name = "Polymorph Potion", Gold = 1300, Type = "Spell" });
                context.Cards.Add(new VanquishMonsterSpellCard { Description = "You get one wish. Alladin was a liar", Name = "Magic Lamp", Gold = 500, Type = "Spell" });

                context.Cards.Add(new BuffPlayerSpellCard { Description = "Somewhere in between Harry Potter and that creepy guy in the park.", Name = "Cloak of Obscurity", CombatPower = 4, Gold = 600, Type = "Spell" });
                context.Cards.Add(new BuffPlayerSpellCard { Description = "The highground always wins. Why not make your own.", Name = "Stepladder", CombatPower = 3, Gold = 400, Type = "Spell" });
                context.Cards.Add(new BuffPlayerSpellCard { Description = "Well...a distraction is always good.", Name = "Singing & Dancing Sword", CombatPower = 2, Gold = 400, Type = "Spell" });
                context.Cards.Add(new BuffPlayerSpellCard { Description = "At least my friends think I'm cool.", Name = "Really Impressive Title", CombatPower = 3, Type = "Spell" });
                context.Cards.Add(new BuffPlayerSpellCard { Description = "Just throw it already. It stinks in here.", Name = "Limburger and Anchovy Sandwich", CombatPower = 3, Gold = 400, Type = "Spell" });
                context.Cards.Add(new BuffPlayerSpellCard { Description = "You'll never slow dance again, but you can really kill a gnome.", Name = "Spiky Knees", CombatPower = 1, Gold = 200, Type = "Spell" });
                context.Cards.Add(new BuffPlayerSpellCard { Description = "Look. Just put them on and lift that building.", Name = "Pantyhose of Giant Strength", CombatPower = 3, Gold = 600, Type = "Spell" });
                context.Cards.Add(new BuffPlayerSpellCard { Description = "DnD for life.", Name = "Magic Missle", CombatPower = 5, Gold = 300, Type = "Spell" });
                context.Cards.Add(new BuffPlayerSpellCard { Description = "You think your high class with your fancy water, and your right.", Name = "Yuppie Water", CombatPower = 2, Gold = 100, Type = "Spell" });
                context.Cards.Add(new BuffPlayerSpellCard { Description = "Don't drop it and get cold feet.", Name = "Freezing Explosive Potion", CombatPower = 3, Gold = 100, Type = "Spell" });
                context.Cards.Add(new BuffPlayerSpellCard { Description = "No one can resist......THE BALLOON.", Name = "Pretty Balloons", CombatPower = 5, Type = "Spell" });

                context.Cards.Add(new BuffMonsterSpellTreasureCard { Description = "Stay arms length from that monster. Mayber further.", Name = "Halitosis", CombatPower = 2, Gold = 100, Type = "Spell" });
                context.Cards.Add(new BuffMonsterSpellTreasureCard { Description = "Here. Try this.", Name = "Flaming Poison Potion", CombatPower = 3, Gold = 100, Type = "Spell" });
                context.Cards.Add(new BuffMonsterSpellTreasureCard { Description = "Swing until you hit somebody. Then swing some more.", Name = "Cotion of Ponfusion", CombatPower = 3, Gold = 100, Type = "Spell" });
                context.Cards.Add(new BuffMonsterSpellTreasureCard { Description = "Looks like your friend needs a nap. Help them out.", Name = "Sleep Potion", CombatPower = 2, Gold = 100, Type = "Spell" });
                context.Cards.Add(new BuffMonsterSpellTreasureCard { Description = "They uhh.....shouldn't try to fight that.", Name = "Potion of Idiotic Bravery", CombatPower = 2, Gold = 100, Type = "Spell" });
                context.Cards.Add(new BuffMonsterSpellTreasureCard { Description = "Why would you drink this.", Name = "Nasty-Tasting Sports Drink", CombatPower = 2, Gold = 200, Type = "Spell" });

                context.Cards.Add(new EquipmentCard { Description = "Slimy and effective.", Slot = "Armor", Gold = 200, Bonus = 1, Name = "Slimy Armor", Type = "Equipment" });
                context.Cards.Add(new EquipmentCard { Description = "Read the name.", Slot = "Headgear", Gold = 400, Bonus = 3, Name = "Bad-Ass Bandanna", Type = "Equipment" });
                context.Cards.Add(new EquipmentCard { Description = "You'll like the way you look. I guarantee it.", Slot = "Armor", Gold = 400, Bonus = 3, Name = "Short Wide Armor", Type = "Equipment" });
                context.Cards.Add(new EquipmentCard { Description = "Well.....it's pretty.", Slot = "2Hands", Gold = 800, Bonus = 4, Name = "Bow With Ribbons", Type = "Equipment" });

                context.SaveChanges();
            }
        }
    }
}
